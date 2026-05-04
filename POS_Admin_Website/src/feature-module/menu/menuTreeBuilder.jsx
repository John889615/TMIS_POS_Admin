import React, { useState, useEffect } from "react";
import Swal from "sweetalert2";
import {
  DndContext,
  useDraggable,
  useDroppable,
  DragOverlay,
} from "@dnd-kit/core";
import { getMenuTree } from "../../services/menu/menuService";
import { getAllProducts } from "../../services/product/product";
import { useParams } from "react-router-dom";
import {
  newMenuItemProduct,
  deleteMenuItemProduct,
  reorderMenuItemProducts,
} from "../../services/menu/menuItemProductService";
import {
  newMenuItem,
  updateMenuItem,
  deleteMenuItem,
} from "../../services/menu/menuItemService";

const MenuTreeBuilder = () => {
  const { id } = useParams();

  const [menuData, setMenuData] = useState(null);
  const [productList, setProductList] = useState([]);
  const [activeProduct, setActiveProduct] = useState(null);

  const [expandedItems, setExpandedItems] = useState({}); // { parentId: childId(s) }
  const [parentItemId, setParentItemId] = useState(null);

  const [showNewModal, setShowNewModal] = useState(false);
  const [newItemName, setNewItemName] = useState("");
  const [newItemDesc, setNewItemDesc] = useState("");
  const [newItemImage, setNewItemImage] = useState(null);

  // Edit modal state
  const [showEditModal, setShowEditModal] = useState(false);
  const [editItem, setEditItem] = useState(null);
  const [editItemName, setEditItemName] = useState("");
  const [editItemDesc, setEditItemDesc] = useState("");
  const [editItemImage, setEditItemImage] = useState(null);

  // Filter state
  const [productFilter, setProductFilter] = useState("");
  const [onlyUnassigned, setOnlyUnassigned] = useState(false);

  // -------------------------------------------------------
  // Display helper: always prefer Description over ProductName
  // -------------------------------------------------------
  const getProductLabel = (p) =>
    (p?.Description || p?.ProductDescription || p?.ProductName || p?.Product || "")
      ?.toString()
      .trim();

  // -----------------------------
  // SweetAlert helpers
  // -----------------------------
  const getApiMessage = (res, fallback) =>
    res?.Messages?.[0] || res?.Errors?.[0] || fallback || "Something went wrong.";

  const swalError = async (title, resOrMsg) => {
    const msg =
      typeof resOrMsg === "string"
        ? resOrMsg
        : getApiMessage(resOrMsg, "Something went wrong.");
    await Swal.fire({
      icon: "error",
      title: title || "Error",
      text: msg,
      confirmButtonText: "OK",
      allowOutsideClick: false,
    });
  };

  const swalConfirm = async (title, text, confirmText = "Yes") => {
    const result = await Swal.fire({
      icon: "warning",
      title: title,
      text: text,
      showCancelButton: true,
      confirmButtonText: confirmText,
      cancelButtonText: "Cancel",
      reverseButtons: true,
      allowOutsideClick: false,
    });
    return result.isConfirmed;
  };

  // -----------------------------
  // Data loading
  // -----------------------------
  useEffect(() => {
    if (id) {
      fetchData();
      fetchProduct();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  async function fetchData() {
    try {
      const tree = await getMenuTree(id);
      setMenuData(tree);
    } catch (err) {
      await swalError(
        "Failed to load menu tree",
        err?.message || "Could not load menu tree."
      );
    }
  }

  async function fetchProduct() {
    try {
      const products = await getAllProducts();
      setProductList(Array.isArray(products) ? products : []);
    } catch (err) {
      await swalError(
        "Failed to load products",
        err?.message || "Could not load products."
      );
    }
  }

  // Expand all (existing logic kept)
  useEffect(() => {
    if (menuData && menuData.MenuItems && menuData.MenuItems.length > 0) {
      const expanded = {};
      function expandAll(items, parentId = "root") {
        if (items && items.length > 0) {
          expanded[parentId] = items.map((child) => child.ItemID);
          items.forEach((item) => {
            if (item.ChildItem && item.ChildItem.length > 0) {
              expandAll(item.ChildItem, item.ItemID);
            }
          });
        }
      }
      expandAll(menuData.MenuItems, "root");
      setExpandedItems(expanded);
    }
  }, [menuData]);

  // -----------------------------
  // Assigned products helpers
  // -----------------------------
  function collectAssignedProductIds(menuItems) {
    const ids = new Set();
    function walk(items) {
      if (!items) return;
      items.forEach((it) => {
        if (it.Product && it.Product.length > 0) {
          it.Product.forEach((p) => {
            const pid = p.ProductID ?? p.POS_ProductID;
            if (pid != null) ids.add(Number(pid));
          });
        }
        if (it.ChildItem && it.ChildItem.length > 0) walk(it.ChildItem);
      });
    }
    walk(menuItems);
    return ids;
  }

  const assignedIds = menuData?.MenuItems
    ? collectAssignedProductIds(menuData.MenuItems)
    : new Set();

  const filteredProducts = (productList || [])
    .filter((p) => {
      const label = getProductLabel(p).toLowerCase(); // ✅ filter on Description first
      return label.includes(productFilter.trim().toLowerCase());
    })
    .filter((p) => {
      if (!onlyUnassigned) return true;
      const pid = Number(p.ProductID ?? p.POS_ProductID);
      return !assignedIds.has(pid);
    });

  // -----------------------------
  // Droppable + draggable
  // -----------------------------
  function DroppableProducts({ item, parentId, children }) {
    const { setNodeRef, isOver } = useDroppable({
      id: `menu-products-${item.ItemID}`,
      // beforeLinkId: null => drop on bare zone means "append at end".
      data: { item, parentId, targetMenuItemId: item.ItemID, beforeLinkId: null },
    });

    // Allow drop from anywhere (right pane, same item, different item).
    const canDrop = !!activeProduct;

    return (
      <div
        ref={setNodeRef}
        style={{ background: isOver && canDrop ? "#e3f2fd" : undefined }}
      >
        {children(isOver && canDrop)}
      </div>
    );
  }

  function DraggableLinkedProduct({ product, sourceMenuItemId, onDelete }) {
    const productId = product.ProductID ?? product.POS_ProductID;
    const menuItemProductId = product.POS_MenuItemProductID;
    const fallbackId = `${sourceMenuItemId}-${productId}`;

    const draggable = useDraggable({
      id: `menuitem-product-${menuItemProductId ?? fallbackId}`,
      data: { product, fromMenuItemId: sourceMenuItemId, menuItemProductId, productId },
    });

    // Each row is also a drop target: dropping on it means "insert ABOVE this row".
    const droppable = useDroppable({
      id: `mip-drop-${menuItemProductId ?? fallbackId}`,
      data: { targetMenuItemId: sourceMenuItemId, beforeLinkId: menuItemProductId },
      disabled: draggable.isDragging,
    });

    const setNodeRef = (el) => {
      draggable.setNodeRef(el);
      droppable.setNodeRef(el);
    };

    useEffect(() => {
      if (draggable.isDragging) {
        setActiveProduct({ ...product, _fromMenuItemId: sourceMenuItemId });
      } else if (
        activeProduct?._fromMenuItemId === sourceMenuItemId &&
        (activeProduct.ProductID ?? activeProduct.POS_ProductID) === productId
      ) {
        setActiveProduct(null);
      }
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [draggable.isDragging]);

    const showDropIndicator =
      droppable.isOver && activeProduct &&
      !(activeProduct._fromMenuItemId === sourceMenuItemId &&
        (activeProduct.ProductID ?? activeProduct.POS_ProductID) === productId);

    return (
      <li
        ref={setNodeRef}
        {...draggable.attributes}
        {...draggable.listeners}
        className="mb-1 d-flex align-items-center justify-content-between"
        style={{
          border: "1px solid #dee2e6",
          borderTop: showDropIndicator ? "3px solid #0d6efd" : "1px solid #dee2e6",
          borderRadius: "5px",
          padding: "6px 10px",
          background: draggable.isDragging ? "#e3f2fd" : "#f9f9f9",
          cursor: "grab",
          opacity: draggable.isDragging ? 0.5 : 1,
          marginTop: showDropIndicator ? 4 : 0,
        }}
      >
        <span>{getProductLabel(product)}</span>

        <button
          className="btn btn-sm btn-link text-danger p-0"
          onPointerDown={(e) => e.stopPropagation()}
          onClick={onDelete}
        >
          <i className="bi bi-trash"></i>
        </button>
      </li>
    );
  }

  function DraggableProduct({ product, fromMenuItemId }) {
    if (fromMenuItemId) {
      return (
        <div
          className="list-group-item"
          style={{
            cursor: "default",
            opacity: 1,
            backgroundColor: "rgb(249, 249, 249)",
          }}
        >
          {getProductLabel(product)}
        </div>
      );
    }

    const { attributes, listeners, setNodeRef, isDragging } = useDraggable({
      id: `product-${product.POS_ProductID}`,
      data: { product },
    });

    useEffect(() => {
      if (isDragging) setActiveProduct(product);
      else if (
        activeProduct &&
        activeProduct.POS_ProductID === product.POS_ProductID
      )
        setActiveProduct(null);
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [isDragging]);

    return (
      <div
        ref={setNodeRef}
        {...attributes}
        {...listeners}
        className={`list-group-item${isDragging ? " bg-info border border-primary" : ""
          }`}
        style={{ cursor: "grab", opacity: isDragging ? 0.5 : 1 }}
      >
        {getProductLabel(product)}
      </div>
    );
  }

  // -----------------------------
  // Render tree
  // -----------------------------
  const renderMenuTree = (items, level = 0, parentId = "root") => (
    <ul className="list-group" style={{ marginLeft: level * 10 }}>
      {items.map((item) => {
        const hasChildren = item.ChildItem && item.ChildItem.length > 0;
        const expandedForParent = expandedItems[parentId];
        const isExpanded = Array.isArray(expandedForParent)
          ? expandedForParent.includes(item.ItemID)
          : expandedForParent === item.ItemID;

        const isTopLevel = level === 0;

        return (
          <li
            key={item.ItemID}
            className={`list-group-item${isTopLevel ? " mb-3 border-1" : "shadow-sm"
              }`}
            style={
              isTopLevel
                ? {
                  borderRadius: "8px",
                  boxShadow: "0 0.125rem 0.25rem rgba(0,0,0,.075)",
                }
                : {}
            }
          >
            <div
              style={{
                display: "flex",
                alignItems: "center",
                fontWeight: 600,
                justifyContent: "space-between",
              }}
            >
              {/* LEFT SIDE */}
              <div style={{ display: "flex", alignItems: "center" }}>
                {(hasChildren || (item.Product && item.Product.length > 0)) && (
                  <button
                    type="button"
                    className="btn btn-sm btn-link px-1"
                    style={{ fontSize: "1.1em", marginRight: 4 }}
                    onClick={() => {
                      setExpandedItems((prev) => {
                        const prevExpanded = prev[parentId];
                        let newExpanded;
                        if (Array.isArray(prevExpanded)) {
                          if (prevExpanded.includes(item.ItemID))
                            newExpanded = prevExpanded.filter(
                              (id) => id !== item.ItemID
                            );
                          else newExpanded = [...prevExpanded, item.ItemID];
                        } else {
                          newExpanded = prevExpanded === item.ItemID ? [] : [item.ItemID];
                        }
                        return { ...prev, [parentId]: newExpanded };
                      });
                    }}
                  >
                    {isExpanded ? "-" : "+"}
                  </button>
                )}

                <span style={{ fontSize: "1.25rem" }}>{item.Item}</span>

                {/* EDIT */}
                <button
                  type="button"
                  className="btn btn-sm btn-link text-primary ms-2 p-0"
                  title="Edit Menu Item"
                  onClick={() => {
                    setEditItem(item);
                    setEditItemName(item.Item);
                    setEditItemDesc(item.Description || "");
                    setEditItemImage(null);
                    setShowEditModal(true);
                  }}
                >
                  <i className="bi bi-pencil-square"></i>
                </button>

                {/* DELETE */}
                <button
                  type="button"
                  className="btn btn-sm btn-link text-danger ms-2 p-0"
                  title="Delete Menu Item"
                  onClick={async () => {
                    const ok = await swalConfirm(
                      "Delete menu item?",
                      "This will remove ALL sub-items and linked products.",
                      "Delete"
                    );
                    if (!ok) return;

                    try {
                      const res = await deleteMenuItem(item.ItemID);
                      if (res?.Success === false) {
                        await swalError("Delete failed", res);
                        return;
                      }
                      await fetchData();
                    } catch (err) {
                      await swalError(
                        "Delete failed",
                        err?.message || "Error deleting menu item."
                      );
                    }
                  }}
                >
                  <i className="bi bi-trash"></i>
                </button>
              </div>

              {/* RIGHT SIDE */}
              <div className="d-flex align-items-center gap-2">
                <button
                  className="btn btn-sm btn-outline-success"
                  onClick={() => openNewItemModal(item.ItemID)}
                >
                  <i className="bi bi-plus-circle me-1"></i>
                  Add Sub Item
                </button>
                {level > 0 && <span className="badge bg-info">Sub Item</span>}
              </div>
            </div>

            {/* PRODUCTS */}
            {isExpanded && (
              <DroppableProducts item={item} parentId={parentId}>
                {(isOver) => (
                  <div>
                    <span style={{ fontWeight: 500 }}>Products:</span>
                    <ul
                      className="list-unstyled mb-2"
                      style={{
                        minHeight: 32,
                        background: isOver ? "#bbf7d0" : undefined,
                        border: isOver ? "2px dashed #007bff" : undefined,
                      }}
                    >
                      {isOver && activeProduct && (
                        <li className="list-group-item list-group-item-success">
                          Drop <b>{getProductLabel(activeProduct)}</b> here
                        </li>
                      )}

                      {item.Product && item.Product.length > 0 ? (
                        item.Product.map((prod) => (
                          <DraggableLinkedProduct
                            key={prod.ProductID || prod.POS_ProductID}
                            product={prod}
                            sourceMenuItemId={item.ItemID}
                            onDelete={async () => {
                              const ok = await swalConfirm(
                                "Delete product link?",
                                "Remove this product from the menu item?",
                                "Remove"
                              );
                              if (!ok) return;

                              try {
                                const res = await deleteMenuItemProduct(
                                  prod.POS_MenuItemProductID
                                );
                                if (res?.Success === false) {
                                  await swalError("Remove failed", res);
                                  return;
                                }
                                await fetchData();
                              } catch (err) {
                                await swalError(
                                  "Remove failed",
                                  err?.message || "Error removing product."
                                );
                              }
                            }}
                          />
                        ))
                      ) : (
                        <li className="text-muted">No products assigned</li>
                      )}
                    </ul>
                  </div>
                )}
              </DroppableProducts>
            )}

            {/* CHILDREN */}
            {hasChildren && isExpanded && (
              <div
                className="ms-3 p-2 shadow-sm mt-1"
                style={{
                  borderRadius: "8px",
                  border: "1px solid #dee2e6",
                }}
              >
                {renderMenuTree(item.ChildItem, level + 1, item.ItemID)}
              </div>
            )}
          </li>
        );
      })}
    </ul>
  );

  // -----------------------------
  // Drag end logic
  // -----------------------------
  const findMenuItemById = (items, menuItemId) => {
    for (const item of items || []) {
      if (item.ItemID === menuItemId) return item;
      if (item.ChildItem && item.ChildItem.length > 0) {
        const found = findMenuItemById(item.ChildItem, menuItemId);
        if (found) return found;
      }
    }
    return null;
  };

  // Build the new ordered list of POS_MenuItemProductIDs after dropping
  // `draggedLinkId` either above `beforeLinkId` (insert there) or at the
  // end (when beforeLinkId is null). Filters out the dragged ID first so
  // reorder works for both same-context reorder and post-cross-item move.
  const buildReorderedIds = (products, draggedLinkId, beforeLinkId) => {
    const ids = (products || [])
      .map((p) => p.POS_MenuItemProductID)
      .filter((linkId) => linkId != null);
    const filtered = ids.filter((linkId) => linkId !== draggedLinkId);
    if (beforeLinkId == null) {
      filtered.push(draggedLinkId);
      return filtered;
    }
    const idx = filtered.indexOf(beforeLinkId);
    if (idx === -1) {
      filtered.push(draggedLinkId);
    } else {
      filtered.splice(idx, 0, draggedLinkId);
    }
    return filtered;
  };

  // After an add/move into `targetMenuItemId`, if a target position was
  // requested, refetch the tree to discover the new link ID, then call
  // reorder so the row lands above `beforeLinkId`. If beforeLinkId is null
  // the new row is already at the bottom (MAX+1 from the API), so we just
  // refresh.
  const placeAtPositionAfterAdd = async (targetMenuItemId, addedProductId, beforeLinkId) => {
    if (beforeLinkId == null) {
      await fetchData();
      return;
    }
    const tree = await getMenuTree(id);
    setMenuData(tree);
    const refreshedTarget = findMenuItemById(tree?.MenuItems || [], targetMenuItemId);
    const newProduct = refreshedTarget?.Product?.find(
      (p) => (p.ProductID ?? p.POS_ProductID) === addedProductId
    );
    if (!newProduct) return;
    const newLinkId = newProduct.POS_MenuItemProductID;
    if (newLinkId == null) return;
    const newOrderIds = buildReorderedIds(refreshedTarget.Product, newLinkId, beforeLinkId);
    await reorderMenuItemProducts(targetMenuItemId, newOrderIds);
    await fetchData();
  };

  const handleDragEnd = async (event) => {
    const { active, over } = event;
    setActiveProduct(null);

    if (!active || !over) return;

    const targetMenuItemId = over.data?.current?.targetMenuItemId;
    const beforeLinkId = over.data?.current?.beforeLinkId ?? null;

    if (targetMenuItemId == null) return;

    const targetMenuItem = findMenuItemById(menuData?.MenuItems, targetMenuItemId);

    if (!targetMenuItem) {
      await swalError("Drag/drop failed", "Could not find target menu item.");
      return;
    }

    // Case 1: dragging a product from the right-side products list
    if (active.id.startsWith("product-")) {
      const match = active.id.match(/^product-(\d+)$/);
      if (!match) return;

      const productId = parseInt(match[1], 10);
      const draggedProduct = productList.find(
        (p) => p.POS_ProductID === productId
      );
      if (!draggedProduct) {
        await swalError("Drag/drop failed", "Could not find dragged product.");
        return;
      }

      const alreadyExists =
        targetMenuItem.Product &&
        targetMenuItem.Product.some(
          (p) => p.ProductID === productId || p.POS_ProductID === productId
        );

      if (alreadyExists) {
        await swalError(
          "Already linked",
          "This product is already linked to the menu item."
        );
        return;
      }

      try {
        const res = await newMenuItemProduct({
          FK_MenuItemID: targetMenuItemId,
          FK_ProductID: draggedProduct.ProductID || draggedProduct.POS_ProductID,
        });

        if (res?.Success === false) {
          await swalError("Could not add product", res);
          return;
        }

        await placeAtPositionAfterAdd(targetMenuItemId, productId, beforeLinkId);
      } catch (err) {
        await swalError(
          "Drag/drop failed",
          err?.message || "Could not add product to item."
        );
      }
      return;
    }

    // Case 2: dragging a product that already belongs to a menu item
    if (active.id.startsWith("menuitem-product-")) {
      const sourceMenuItemId = active.data.current?.fromMenuItemId;
      const productId = active.data.current?.productId;
      const sourceLinkId = active.data.current?.menuItemProductId;

      if (sourceMenuItemId == null || productId == null) {
        await swalError("Move failed", "Could not read source data.");
        return;
      }

      if (!sourceLinkId) {
        await swalError(
          "Move failed",
          "No menu-item-product link ID found on the source row."
        );
        return;
      }

      // 2a: same menu item — reorder only
      if (sourceMenuItemId === targetMenuItemId) {
        if (beforeLinkId === sourceLinkId) return;

        const newOrderIds = buildReorderedIds(
          targetMenuItem.Product,
          sourceLinkId,
          beforeLinkId
        );

        const existingIds = (targetMenuItem.Product || [])
          .map((p) => p.POS_MenuItemProductID)
          .filter((linkId) => linkId != null);
        if (existingIds.join(",") === newOrderIds.join(",")) return;

        try {
          const res = await reorderMenuItemProducts(targetMenuItemId, newOrderIds);
          if (res?.Success === false) {
            await swalError("Reorder failed", res);
            return;
          }
          await fetchData();
        } catch (err) {
          await swalError(
            "Reorder failed",
            err?.message || "Could not reorder products."
          );
        }
        return;
      }

      // 2b: different menu item — move (add then delete), then optional reorder
      const alreadyExists =
        targetMenuItem.Product &&
        targetMenuItem.Product.some(
          (p) => p.ProductID === productId || p.POS_ProductID === productId
        );

      if (alreadyExists) {
        await swalError(
          "Already linked",
          "This product is already linked to the target menu item."
        );
        return;
      }

      try {
        const addRes = await newMenuItemProduct({
          FK_MenuItemID: targetMenuItemId,
          FK_ProductID: productId,
        });

        if (addRes?.Success === false) {
          await swalError("Move failed", addRes);
          return;
        }

        const delRes = await deleteMenuItemProduct(sourceLinkId);

        if (delRes?.Success === false) {
          await swalError(
            "Move partially failed",
            "Added to the target item, but could not remove from the original. Please remove the duplicate manually."
          );
          await fetchData();
          return;
        }

        await placeAtPositionAfterAdd(targetMenuItemId, productId, beforeLinkId);
      } catch (err) {
        await swalError(
          "Move failed",
          err?.message || "Could not move product between menu items."
        );
      }
    }
  };

  const openNewItemModal = (parentId = null) => {
    setParentItemId(parentId);
    setShowNewModal(true);
  };

  // -----------------------------
  // UI
  // -----------------------------
  return (
    <div className="page-wrapper">
      <div className="content">
        <div className="page-header">
          <div className="add-item d-flex">
            <h4 className="mb-1">
              {menuData ? `${menuData.MenuName} Menu` : "Menu Tree"}
            </h4>
          </div>
          <div className="page-btn">
            <button className="btn btn-success mb-3" onClick={() => openNewItemModal()}>
              Add New Menu Item
            </button>
          </div>
        </div>

        {/* New Menu Item Modal */}
        {showNewModal && (
          <div
            className="modal fade show"
            style={{ display: "block", background: "rgba(0,0,0,0.3)" }}
          >
            <div className="modal-dialog">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title">
                    {parentItemId ? "Add Sub Menu Item" : "Add New Menu Item"}
                  </h5>
                  <button
                    type="button"
                    className="btn-close"
                    onClick={() => {
                      setShowNewModal(false);
                      setParentItemId(null);
                      setNewItemName("");
                      setNewItemDesc("");
                      setNewItemImage(null);
                    }}
                  ></button>
                </div>

                <form
                  onSubmit={async (e) => {
                    e.preventDefault();
                    if (!newItemName.trim()) return;

                    const payload = {
                      FK_MenuID: menuData?.MenuID || 0,
                      Item: newItemName,
                      Description: newItemDesc,
                      FK_POS_MenuItemID: parentItemId,
                    };

                    if (newItemImage) payload.ImageFile = newItemImage;

                    try {
                      const res = await newMenuItem(payload);
                      if (res?.Success) {
                        await fetchData();
                        setShowNewModal(false);
                        setNewItemName("");
                        setNewItemDesc("");
                        setNewItemImage(null);
                        setParentItemId(null);
                      } else {
                        await swalError("Could not add item", res);
                      }
                    } catch (err) {
                      await swalError(
                        "Could not add item",
                        err?.message || "Error adding menu item."
                      );
                    }
                  }}
                >
                  <div className="modal-body">
                    {parentItemId && (
                      <div className="alert alert-info mb-3">
                        <i className="bi bi-info-circle me-2"></i>
                        This will be added as a sub-item under the selected menu item.
                      </div>
                    )}

                    <input
                      type="text"
                      className="form-control mb-2"
                      placeholder="Menu item name"
                      value={newItemName}
                      onChange={(e) => setNewItemName(e.target.value)}
                    />
                    <input
                      type="text"
                      className="form-control mb-2"
                      placeholder="Description"
                      value={newItemDesc}
                      onChange={(e) => setNewItemDesc(e.target.value)}
                    />
                    <input
                      name="ImageFile"
                      type="file"
                      accept="image/*"
                      className="form-control"
                      onChange={(e) => setNewItemImage(e.target.files?.[0] || null)}
                    />
                  </div>

                  <div className="modal-footer">
                    <button
                      type="button"
                      className="btn btn-secondary"
                      onClick={() => {
                        setShowNewModal(false);
                        setParentItemId(null);
                        setNewItemName("");
                        setNewItemDesc("");
                        setNewItemImage(null);
                      }}
                    >
                      Cancel
                    </button>
                    <button type="submit" className="btn btn-primary">
                      Add
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        )}

        {/* Edit Menu Item Modal */}
        {showEditModal && editItem && (
          <div
            className="modal fade show"
            style={{ display: "block", background: "rgba(0,0,0,0.3)" }}
          >
            <div className="modal-dialog">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title">Edit Menu Item</h5>
                  <button
                    type="button"
                    className="btn-close"
                    onClick={() => {
                      setShowEditModal(false);
                      setEditItem(null);
                      setEditItemImage(null);
                    }}
                  ></button>
                </div>

                <form
                  onSubmit={async (e) => {
                    e.preventDefault();
                    if (!editItemName.trim()) return;

                    const payload = {
                      POS_MenuItemID: editItem.ItemID,
                      FK_MenuID: menuData?.MenuID || 0,
                      Item: editItemName,
                      Description: editItemDesc,
                      FK_POS_MenuItemID: editItem.FK_POS_MenuItemID || null,
                    };

                    if (editItemImage) payload.ImageFile = editItemImage;

                    try {
                      const res = await updateMenuItem(payload);
                      if (res?.Success) {
                        await fetchData();
                        setShowEditModal(false);
                        setEditItem(null);
                        setEditItemImage(null);
                      } else {
                        await swalError("Update failed", res);
                      }
                    } catch (err) {
                      await swalError(
                        "Update failed",
                        err?.message || "Error updating menu item."
                      );
                    }
                  }}
                >
                  <div className="modal-body">
                    <input
                      type="text"
                      className="form-control mb-2"
                      placeholder="Menu item name"
                      value={editItemName}
                      onChange={(e) => setEditItemName(e.target.value)}
                    />
                    <input
                      type="text"
                      className="form-control mb-2"
                      placeholder="Description"
                      value={editItemDesc}
                      onChange={(e) => setEditItemDesc(e.target.value)}
                    />
                    <input
                      name="ImageFile"
                      type="file"
                      accept="image/*"
                      className="form-control"
                      onChange={(e) => setEditItemImage(e.target.files?.[0] || null)}
                    />
                  </div>

                  <div className="modal-footer">
                    <button
                      type="button"
                      className="btn btn-secondary"
                      onClick={() => {
                        setShowEditModal(false);
                        setEditItem(null);
                        setEditItemImage(null);
                      }}
                    >
                      Cancel
                    </button>
                    <button type="submit" className="btn btn-primary">
                      Update
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        )}

        {/* Main DnD layout */}
        <DndContext onDragEnd={handleDragEnd}>
          <div className="row">
            {/* Left: Menu Tree */}
            <div className="col-md-7" style={{ maxHeight: "70vh", overflowY: "auto" }}>
              <div className="mb-3">
                <h5 className="mb-3">Menu Items</h5>
                {menuData && menuData.MenuItems ? (
                  renderMenuTree(menuData.MenuItems, 0, "root")
                ) : (
                  <p>Loading menu...</p>
                )}
              </div>
            </div>

            {/* Right: Products */}
            <div className="col-md-5">
              <div className="card shadow-sm">
                <div className="card-header bg-white">
                  <h5 className="mb-2">All Products</h5>

                  <input
                    type="text"
                    className="form-control form-control-sm"
                    placeholder="Search products..."
                    value={productFilter}
                    onChange={(e) => setProductFilter(e.target.value)}
                  />

                  <div className="form-check mt-2">
                    <input
                      className="form-check-input"
                      type="checkbox"
                      id="onlyUnassigned"
                      checked={onlyUnassigned}
                      onChange={(e) => setOnlyUnassigned(e.target.checked)}
                    />
                    <label className="form-check-label" htmlFor="onlyUnassigned">
                      Only show unassigned
                    </label>
                  </div>

                  <div className="text-muted mt-2" style={{ fontSize: 12 }}>
                    Showing {filteredProducts.length} of {productList.length}
                  </div>
                </div>

                <div style={{ maxHeight: "70vh", overflowY: "auto", padding: "16px" }}>
                  {filteredProducts && filteredProducts.length > 0 ? (
                    <ul className="list-group">
                      {filteredProducts.map((product) => (
                        <DraggableProduct product={product} key={product.POS_ProductID} />
                      ))}
                    </ul>
                  ) : (
                    <div className="text-muted">No matching products</div>
                  )}

                  <DragOverlay>
                    {activeProduct ? (
                      <div
                        className="list-group-item bg-primary text-white border border-primary"
                        style={{
                          fontWeight: 600,
                          fontSize: "1.1em",
                          boxShadow: "0 2px 8px rgba(0,0,0,0.15)",
                        }}
                      >
                        Dragging: {getProductLabel(activeProduct)}
                      </div>
                    ) : null}
                  </DragOverlay>
                </div>
              </div>
            </div>
          </div>
        </DndContext>
      </div>
    </div>
  );
};

export default MenuTreeBuilder;