import React, { useState, useEffect } from "react";
import Swal from "sweetalert2";
import {
  DndContext,
  useDraggable,
  useDroppable,
  DragOverlay,
} from "@dnd-kit/core";
import { getCampMenuTree } from "../../services/menu/menuService";
import { getAllProducts } from "../../services/product/product";
import { useParams } from "react-router-dom";
import {
  newDebtorMenuItemProduct,
  deleteDebtorMenuItemProduct,
} from "../../services/menu/menuItemProductService";
import {
  newDebtorMenuItem,
  updateDebtorMenuItem,
  deleteDebtorMenuItem,
} from "../../services/menu/menuItemService";

const MenuTreeCampBuilder = () => {
  const { id } = useParams();

  const [menuData, setMenuData] = useState(null);
  const [productList, setProductList] = useState([]);
  const [activeProduct, setActiveProduct] = useState(null);

  const [expandedItems, setExpandedItems] = useState({});
  const [parentItemId, setParentItemId] = useState(null);

  const [showNewModal, setShowNewModal] = useState(false);
  const [newItemName, setNewItemName] = useState("");
  const [newItemDesc, setNewItemDesc] = useState("");

  const [showEditModal, setShowEditModal] = useState(false);
  const [editItem, setEditItem] = useState(null);
  const [editItemName, setEditItemName] = useState("");
  const [editItemDesc, setEditItemDesc] = useState("");

  const [productFilter, setProductFilter] = useState("");
  const [onlyUnassigned, setOnlyUnassigned] = useState(false);

  const getProductLabel = (p) =>
    (
      p?.Description ||
      p?.ProductDescription ||
      p?.ProductName ||
      p?.Product ||
      ""
    )
      ?.toString()
      .trim();

  const getProductId = (p) => {
    const value = p?.ProductID ?? p?.POS_ProductID ?? p?.FK_ProductID;
    return value !== null && value !== undefined ? Number(value) : null;
  };

  // THIS is the important fix
  const getMenuItemProductId = (p) => {
    const value =
      p?.DebtorMenuItemProductID ??
      p?.POS_MenuItemProductID ??
      p?.MenuItemProductID ??
      p?.FK_MenuItemProductID;

    return value !== null && value !== undefined && value !== ""
      ? Number(value)
      : null;
  };

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

  const swalSuccess = async (title, msg) => {
    await Swal.fire({
      icon: "success",
      title: title || "Success",
      text: msg || "Done.",
      confirmButtonText: "OK",
      allowOutsideClick: false,
    });
  };

  const swalConfirm = async (title, text, confirmText = "Yes") => {
    const result = await Swal.fire({
      icon: "warning",
      title,
      text,
      showCancelButton: true,
      confirmButtonText: confirmText,
      cancelButtonText: "Cancel",
      reverseButtons: true,
      allowOutsideClick: false,
    });

    return result.isConfirmed;
  };

  useEffect(() => {
    if (id) {
      fetchData();
      fetchProduct();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  async function fetchData() {
    try {
      const tree = await getCampMenuTree(id);
      console.log("getCampMenuTree response:", tree);
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

  function collectAssignedProductIds(menuItems) {
    const ids = new Set();

    function walk(items) {
      if (!items) return;

      items.forEach((it) => {
        if (it.Product && it.Product.length > 0) {
          it.Product.forEach((p) => {
            const pid = getProductId(p);
            if (pid != null) ids.add(pid);
          });
        }

        if (it.ChildItem && it.ChildItem.length > 0) {
          walk(it.ChildItem);
        }
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
      const label = getProductLabel(p).toLowerCase();
      return label.includes(productFilter.trim().toLowerCase());
    })
    .filter((p) => {
      if (!onlyUnassigned) return true;
      const pid = getProductId(p);
      return pid == null ? true : !assignedIds.has(pid);
    });

  function DroppableProducts({ item, parentId, children }) {
    const { setNodeRef, isOver } = useDroppable({
      id: `menu-products-${item.ItemID}`,
      data: { item, parentId },
    });

    const canDrop = activeProduct && !activeProduct.fromMenuItemId;

    return (
      <div
        ref={setNodeRef}
        style={{ background: isOver && canDrop ? "#e3f2fd" : undefined }}
      >
        {children(isOver && canDrop)}
      </div>
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

    const draggableProductId = getProductId(product);

    const { attributes, listeners, setNodeRef, isDragging } = useDraggable({
      id: `product-${draggableProductId}`,
      data: { product },
    });

    useEffect(() => {
      if (isDragging) setActiveProduct(product);
      else if (activeProduct && getProductId(activeProduct) === getProductId(product)) {
        setActiveProduct(null);
      }
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [isDragging]);

    return (
      <div
        ref={setNodeRef}
        {...attributes}
        {...listeners}
        className={`list-group-item${
          isDragging ? " bg-info border border-primary" : ""
        }`}
        style={{ cursor: "grab", opacity: isDragging ? 0.5 : 1 }}
      >
        {getProductLabel(product)}
      </div>
    );
  }

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
            className={`list-group-item${
              isTopLevel ? " mb-3 border-1" : "shadow-sm"
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
                          if (prevExpanded.includes(item.ItemID)) {
                            newExpanded = prevExpanded.filter(
                              (x) => x !== item.ItemID
                            );
                          } else {
                            newExpanded = [...prevExpanded, item.ItemID];
                          }
                        } else {
                          newExpanded =
                            prevExpanded === item.ItemID ? [] : [item.ItemID];
                        }

                        return { ...prev, [parentId]: newExpanded };
                      });
                    }}
                    aria-label={isExpanded ? "Collapse" : "Expand"}
                  >
                    {isExpanded ? <span>-</span> : <span>+</span>}
                  </button>
                )}

                <span style={{ fontSize: "1.25rem" }}>{item.Item}</span>

                <button
                  type="button"
                  className="btn btn-sm btn-link text-primary ms-2 p-0"
                  title="Edit Menu Item"
                  onClick={() => {
                    setEditItem(item);
                    setEditItemName(item.Item);
                    setEditItemDesc(item.Description || "");
                    setShowEditModal(true);
                  }}
                >
                  <i className="bi bi-pencil-square"></i>
                </button>

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
                      const res = await deleteDebtorMenuItem(item.ItemID);

                      if (res?.Success === false) {
                        await swalError("Delete failed", res);
                        return;
                      }

                      await fetchData();
                      await swalSuccess("Deleted", "Menu item removed.");
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

              <div className="d-flex align-items-center gap-2">
                <button
                  className="btn btn-sm btn-outline-success"
                  onClick={() => openNewItemModal(item.ItemID)}
                  title="Add child menu item"
                >
                  <i className="bi bi-plus-circle me-1"></i>
                  Add Sub Item
                </button>
                {level > 0 && <span className="badge bg-info">Sub Item</span>}
              </div>
            </div>

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
                          <span style={{ fontWeight: 500 }}>
                            Drop{" "}
                            <span className="text-primary">
                              {getProductLabel(activeProduct)}
                            </span>{" "}
                            here
                          </span>
                        </li>
                      )}

                      {item.Product && item.Product.length > 0 ? (
                        item.Product.map((prod, index) => {
                          const menuItemProductId = getMenuItemProductId(prod);
                          const productId = getProductId(prod);

                          return (
                            <li
                              key={
                                menuItemProductId ||
                                productId ||
                                `${item.ItemID}-${index}`
                              }
                              className="mb-1 d-flex align-items-center justify-content-between"
                              style={{
                                border: "1px solid #dee2e6",
                                borderRadius: "5px",
                                padding: "6px 10px",
                                background: "#f9f9f9",
                              }}
                            >
                              <span>{getProductLabel(prod)}</span>

                              <button
                                type="button"
                                className="btn btn-sm btn-link text-danger ms-2 p-0"
                                title="Delete Product"
                                onClick={async () => {
                                  console.log("Delete clicked product:", prod);

                                  const deleteId = getMenuItemProductId(prod);

                                  if (!deleteId) {
                                    await swalError(
                                      "Remove failed",
                                      "No menu-item-product link ID found on this row. Check the console log for the product object field names."
                                    );
                                    return;
                                  }

                                  const ok = await swalConfirm(
                                    "Delete product link?",
                                    "Remove this product from the menu item?",
                                    "Remove"
                                  );
                                  if (!ok) return;

                                  try {
                                    const res =
                                      await deleteDebtorMenuItemProduct(deleteId);

                                    if (res?.Success === false) {
                                      await swalError("Remove failed", res);
                                      return;
                                    }

                                    await fetchData();
                                    await swalSuccess(
                                      "Removed",
                                      "Product removed from menu item."
                                    );
                                  } catch (err) {
                                    await swalError(
                                      "Remove failed",
                                      err?.message || "Error removing product."
                                    );
                                  }
                                }}
                              >
                                <i className="bi bi-trash"></i>
                              </button>
                            </li>
                          );
                        })
                      ) : (
                        <li className="text-muted">
                          {isOver ? "Drop product here..." : "No products assigned"}
                        </li>
                      )}
                    </ul>
                  </div>
                )}
              </DroppableProducts>
            )}

            {hasChildren && isExpanded && (
              <div
                className="ms-3 p-2 shadow-sm mt-1"
                style={{ borderRadius: "8px", border: "1px solid #dee2e6" }}
              >
                {renderMenuTree(item.ChildItem, level + 1, item.ItemID)}
              </div>
            )}
          </li>
        );
      })}
    </ul>
  );

  const findMenuItemById = (items, menuItemId) => {
    for (const item of items) {
      if (item.ItemID === menuItemId) return item;
      if (item.ChildItem && item.ChildItem.length > 0) {
        const found = findMenuItemById(item.ChildItem, menuItemId);
        if (found) return found;
      }
    }
    return null;
  };

  const handleDragEnd = async (event) => {
    const { active, over } = event;
    setActiveProduct(null);

    if (!active || !over) return;

    if (
      active.id.startsWith("product-") &&
      over.id.startsWith("menu-products-")
    ) {
      const match = active.id.match(/^product-(\d+)$/);
      if (!match) return;

      const productId = parseInt(match[1], 10);
      const menuItemId = parseInt(over.id.replace("menu-products-", ""), 10);

      const draggedProduct = productList.find(
        (p) => getProductId(p) === productId
      );

      if (!draggedProduct) {
        await swalError("Drag/drop failed", "Could not find dragged product.");
        return;
      }

      const targetMenuItem = findMenuItemById(menuData?.MenuItems || [], menuItemId);

      if (!targetMenuItem) {
        await swalError("Drag/drop failed", "Could not find target menu item.");
        return;
      }

      const alreadyExists =
        targetMenuItem.Product &&
        targetMenuItem.Product.some((p) => getProductId(p) === productId);

      if (alreadyExists) {
        await swalError("Already linked", "This product is already linked to the menu item.");
        return;
      }

      try {
        const res = await newDebtorMenuItemProduct({
          FK_MenuItemID: menuItemId,
          FK_ProductID: getProductId(draggedProduct),
        });

        if (res?.Success === false) {
          await swalError("Could not add product", res);
          return;
        }

        await fetchData();
      } catch (err) {
        await swalError(
          "Drag/drop failed",
          err?.message || "Could not add product to item."
        );
      }
    }
  };

  const openNewItemModal = (parentId = null) => {
    setParentItemId(parentId);
    setShowNewModal(true);
  };

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
            <button
              className="btn btn-success mb-3"
              onClick={() => openNewItemModal()}
            >
              Add New Menu Item
            </button>
          </div>
        </div>

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
                    }}
                  ></button>
                </div>

                <form
                  onSubmit={async (e) => {
                    e.preventDefault();
                    if (!newItemName.trim()) return;

                    const payload = {
                      FK_MenuID: menuData?.DebtorMenuID || 0,
                      Item: newItemName,
                      Description: newItemDesc,
                      FK_POS_MenuItemID: parentItemId,
                    };

                    try {
                      const res = await newDebtorMenuItem(payload);

                      if (res?.Success === false) {
                        await swalError("Could not add menu item", res);
                        return;
                      }

                      await fetchData();
                      setShowNewModal(false);
                      setNewItemName("");
                      setNewItemDesc("");
                      setParentItemId(null);
                      await swalSuccess("Added", "Menu item created.");
                    } catch (err) {
                      await swalError(
                        "Add failed",
                        err?.message || "Error adding menu item."
                      );
                    }
                  }}
                >
                  <div className="modal-body">
                    <input
                      type="text"
                      className="form-control mb-2"
                      placeholder="Menu item name"
                      value={newItemName}
                      onChange={(e) => setNewItemName(e.target.value)}
                    />
                    <input
                      type="text"
                      className="form-control"
                      placeholder="Description"
                      value={newItemDesc}
                      onChange={(e) => setNewItemDesc(e.target.value)}
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
                      }}
                    >
                      Cancel
                    </button>
                    <button type="submit" className="btn btn-primary">
                      Save
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        )}

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
                    }}
                  ></button>
                </div>

                <form
                  onSubmit={async (e) => {
                    e.preventDefault();
                    if (!editItemName.trim()) return;

                    const payload = {
                      POS_MenuItemID: editItem.ItemID,
                      Item: editItemName,
                      Description: editItemDesc,
                      FK_POS_MenuItemID: editItem.FK_POS_MenuItemID || null,
                    };

                    try {
                      const res = await updateDebtorMenuItem(payload);

                      if (res?.Success === false) {
                        await swalError("Could not update menu item", res);
                        return;
                      }

                      await fetchData();
                      setShowEditModal(false);
                      setEditItem(null);
                      await swalSuccess("Updated", "Menu item updated.");
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
                      className="form-control"
                      placeholder="Description"
                      value={editItemDesc}
                      onChange={(e) => setEditItemDesc(e.target.value)}
                    />
                  </div>

                  <div className="modal-footer">
                    <button
                      type="button"
                      className="btn btn-secondary"
                      onClick={() => {
                        setShowEditModal(false);
                        setEditItem(null);
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

        <DndContext onDragEnd={handleDragEnd}>
          <div className="row">
            <div
              className="col-md-7"
              style={{ maxHeight: "70vh", overflowY: "auto" }}
            >
              <div className="mb-3">
                <h5 className="mb-3">Menu Items</h5>
                {menuData && menuData.MenuItems ? (
                  renderMenuTree(menuData.MenuItems, 0, "root")
                ) : (
                  <p>Loading menu...</p>
                )}
              </div>
            </div>

            <div className="col-md-5">
              <div className="card shadow-sm">
                <div className="card-header bg-white">
                  <h5 className="mb-0">All Products</h5>
                </div>

                <div style={{ padding: "16px", borderBottom: "1px solid #eee" }}>
                  <input
                    type="text"
                    className="form-control mb-2"
                    placeholder="Search products..."
                    value={productFilter}
                    onChange={(e) => setProductFilter(e.target.value)}
                  />

                  <div className="form-check">
                    <input
                      className="form-check-input"
                      type="checkbox"
                      id="onlyUnassignedCamp"
                      checked={onlyUnassigned}
                      onChange={(e) => setOnlyUnassigned(e.target.checked)}
                    />
                    <label
                      className="form-check-label"
                      htmlFor="onlyUnassignedCamp"
                    >
                      Show only unassigned products
                    </label>
                  </div>
                </div>

                <div
                  style={{ maxHeight: "70vh", overflowY: "auto", padding: "16px" }}
                >
                  {filteredProducts && filteredProducts.length > 0 ? (
                    <ul className="list-group">
                      {filteredProducts.map((product, index) => (
                        <DraggableProduct
                          product={product}
                          key={getProductId(product) || index}
                        />
                      ))}
                    </ul>
                  ) : (
                    <div className="text-muted">No products available</div>
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

export default MenuTreeCampBuilder;