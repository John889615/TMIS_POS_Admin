import React, { useState, useEffect } from "react";
import { DndContext, useDraggable, useDroppable, DragOverlay } from "@dnd-kit/core";
import { getMenuTree } from "../../services/menu/menuService";
import { getAllProducts } from "../../services/product/product";
import { useParams } from "react-router-dom";
import { newMenuItemProduct, deleteMenuItemProduct } from "../../services/menu/menuItemProductService";
import { newMenuItem, updateMenuItem } from "../../services/menu/menuItemService";

const MenuTreeBuilder = () => {
    const { id } = useParams();
    const [menuData, setMenuData] = useState(null);
    const [productList, setProductList] = useState([]);
    const [activeProduct, setActiveProduct] = useState(null);
    const [expandedItems, setExpandedItems] = useState({}); // { parentId: childId }
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
    useEffect(() => {
        if (menuData && menuData.MenuItems && menuData.MenuItems.length > 0) {
            const expanded = {};
            function expandAll(items, parentId = 'root') {
                if (items && items.length > 0) {
                    // If multiple children, open all (store as array)
                    expanded[parentId] = items.map(child => child.ItemID);
                    items.forEach(item => {
                        if (item.ChildItem && item.ChildItem.length > 0) {
                            expandAll(item.ChildItem, item.ItemID);
                        }
                    });
                }
            }
            expandAll(menuData.MenuItems, 'root');
            setExpandedItems(expanded);
        }
    }, [menuData]);

    useEffect(() => {

        if (id) {
            fetchData();
            fetchProduct();
        }
    }, [id]);

    async function fetchData() {
        const tree = await getMenuTree(id);
        setMenuData(tree);
    }
    async function fetchProduct() {
        const products = await getAllProducts();
        setProductList(products);
    }

    // dnd-kit droppable for menu item products
    // Helper to find root parent for a menu item
    function getRootParentId(menuItems, itemId, parentId = 'root') {
        for (const item of menuItems) {
            if (item.ItemID === itemId) return parentId;
            if (item.ChildItem && item.ChildItem.length > 0) {
                const found = getRootParentId(item.ChildItem, itemId, item.ItemID);
                if (found) return found;
            }
        }
        return null;
    }

    // Only allow drop from right panel (not from menu item to menu item)
    function DroppableProducts({ item, parentId, children }) {
        const { setNodeRef, isOver } = useDroppable({
            id: `menu-products-${item.ItemID}`,
            data: { item, parentId }
        });
        // Only allow drop if dragging from right panel (no fromMenuItemId)
        const canDrop = activeProduct && !activeProduct.fromMenuItemId;
        return (
            <div ref={setNodeRef} style={{ background: isOver && canDrop ? '#e3f2fd' : undefined }}>
                {children(isOver && canDrop)}
            </div>
        );
    }

    // dnd-kit draggable for product (only allow drag from right panel, not from menu item)
    function DraggableProduct({ product, fromMenuItemId }) {
        // Only allow drag if not from a menu item (i.e., from right panel)
        if (fromMenuItemId) {
            // Render as static, not draggable
            return (
                <div className="list-group-item" style={{ cursor: 'default', opacity: 1, backgroundColor: 'rgb(249, 249, 249)' }}>
                    {product.ProductName}
                </div>
            );
        }
        // Draggable for right panel
        const { attributes, listeners, setNodeRef, isDragging } = useDraggable({
            id: `product-${product.POS_ProductID}`,
            data: { product }
        });
        React.useEffect(() => {
            if (isDragging) setActiveProduct(product);
            else if (activeProduct && activeProduct.POS_ProductID === product.POS_ProductID) setActiveProduct(null);
        }, [isDragging]);
        return (
            <div
                ref={setNodeRef}
                {...attributes}
                {...listeners}
                className={`list-group-item${isDragging ? ' bg-info border border-primary' : ''}`}
                style={{ cursor: 'grab', opacity: isDragging ? 0.5 : 1 }}
            >
                {product.ProductName}
            </div>
        );
    }

    // Accordion: only one expanded per parent, single toggle per item
    const renderMenuTree = (items, level = 0, parentId = 'root') => (
        <ul className="list-group" style={{ marginLeft: level * 10 }}>
            {items.map(item => {
                const hasChildren = item.ChildItem && item.ChildItem.length > 0;
                // Support expandedItems[parentId] as array (all open)
                const expandedForParent = expandedItems[parentId];
                const isExpanded = Array.isArray(expandedForParent)
                    ? expandedForParent.includes(item.ItemID)
                    : expandedForParent === item.ItemID;
                // Add extra space below top-level parent menu items
                const isTopLevel = level === 0;
                return (
                    <li key={item.ItemID} className={`list-group-item${isTopLevel ? ' mb-3 border-1' : 'shadow-sm'}`} style={isTopLevel ? { borderRadius: '8px', boxShadow: '0 0.125rem 0.25rem rgba(0,0,0,.075)' } : {}}>
                        <div style={{ display: 'flex', alignItems: 'center', fontWeight: 600, justifyContent: 'space-between' }}>
                            <div style={{ display: 'flex', alignItems: 'center' }}>
                                {/* Single toggle for both children and products */}
                                {(hasChildren || (item.Product && item.Product.length > 0)) && (
                                    <button
                                        type="button"
                                        className="btn btn-sm btn-link px-1"
                                        style={{ fontSize: '1.1em', marginRight: 4 }}
                                        onClick={() => {
                                            setExpandedItems(prev => {
                                                const prevExpanded = prev[parentId];
                                                let newExpanded;
                                                if (Array.isArray(prevExpanded)) {
                                                    if (prevExpanded.includes(item.ItemID)) {
                                                        newExpanded = prevExpanded.filter(id => id !== item.ItemID);
                                                    } else {
                                                        newExpanded = [...prevExpanded, item.ItemID];
                                                    }
                                                } else {
                                                    newExpanded = prevExpanded === item.ItemID ? [] : [item.ItemID];
                                                }
                                                return { ...prev, [parentId]: newExpanded };
                                            });
                                        }}
                                        aria-label={isExpanded ? 'Collapse' : 'Expand'}
                                    >
                                        {isExpanded ? <span>-</span> : <span>+</span>}
                                    </button>
                                )}
                                <span style={{ fontSize: '1.25rem' }}>{item.Item}</span>
                                {/* Edit icon button */}
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
                                {level > 0 && (
                                    <span className="badge bg-info">Sub Item</span>
                                )}
                            </div>
                        </div>
                        {/* Products for this item - droppable, collapsible */}
                        {isExpanded && (
                            <DroppableProducts item={item} parentId={parentId}>
                                {(isOver) => (
                                    <div>
                                        <span style={{ fontWeight: 500 }}>Products:</span>
                                        <ul className="list-unstyled mb-2" style={{ minHeight: 32, background: isOver ? '#bbf7d0' : undefined, border: isOver ? '2px dashed #007bff' : undefined }}>
                                            {isOver && activeProduct && (
                                                <li className="list-group-item list-group-item-success">
                                                    <span style={{ fontWeight: 500 }}>Drop <span className="text-primary">{activeProduct.ProductName}</span> here</span>
                                                </li>
                                            )}
                                            {item.Product && item.Product.length > 0 ? (
                                                item.Product.map((prod) => (
                                                    <li key={prod.ProductID || prod.POS_ProductID} className="mb-1 d-flex align-items-center justify-content-between" style={{ border: "1px solid #dee2e6", borderRadius: "5px", padding: "6px 10px", background: "#f9f9f9" }}>
                                                        <span style={{ fontWeight: 400 }}>{prod.ProductName || prod.Product}</span>
                                                        <button type="button" className="btn btn-sm btn-link text-danger ms-2 p-0" title="Delete" onClick={async () => {
                                                            if (window.confirm('Are you sure you want to delete this product?')) {
                                                                // Call API to delete product from menu item
                                                                await deleteMenuItemProduct(prod.POS_MenuItemProductID);
                                                                // Update local state after successful delete
                                                                setMenuData(prev => {
                                                                    function removeProduct(items) {
                                                                        return items.map(menuItem => {
                                                                            if (menuItem.ItemID === item.ItemID && menuItem.Product) {
                                                                                menuItem.Product = menuItem.Product.filter(p => (p.POS_MenuItemProductID !== prod.POS_MenuItemProductID));
                                                                            }
                                                                            if (menuItem.ChildItem && menuItem.ChildItem.length > 0) {
                                                                                menuItem.ChildItem = removeProduct(menuItem.ChildItem);
                                                                            }
                                                                            return menuItem;
                                                                        });
                                                                    }
                                                                    return { ...prev, MenuItems: removeProduct(prev.MenuItems) };
                                                                });
                                                            }
                                                        }}>
                                                            <i className="bi bi-trash"></i>
                                                        </button>
                                                    </li>
                                                ))
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
                        {/* Child items - collapsible */}
                        {hasChildren && isExpanded && (
                            <div className="ms-3 p-2 shadow-sm mt-1" style={{ borderRadius: '8px', border: '1px solid #dee2e6' }}>
                                {renderMenuTree(item.ChildItem, level + 1, item.ItemID)}
                            </div>
                        )}
                    </li>
                );
            })}
        </ul>
    );

    // Only allow drag from right panel to menu item (not menu item to menu item)
    const handleDragEnd = (event) => {
        const { active, over } = event;
        if (!active || !over) return;
        // Only allow dropping product into menu-products-* from right panel
        if (active.id.startsWith('product-') && over.id.startsWith('menu-products-')) {
            // Only allow if not dragging from a menu item
            const match = active.id.match(/^product-(\d+)(?:-from-(\d+))?$/);
            if (!match) return;
            const productId = parseInt(match[1]);
            const fromMenuItemId = match[2] ? parseInt(match[2]) : null;
            if (fromMenuItemId) return; // Prevent drop from menu item to menu item
            const menuItemId = parseInt(over.id.replace('menu-products-', ''));
            // Find product in right panel
            const draggedProduct = productList.find(p => p.POS_ProductID === productId);
            if (!draggedProduct) return;
            // Add to target menu and call API
            async function addProduct(items) {
                return Promise.all(items.map(async item => {
                    if (item.ItemID === menuItemId) {
                        const alreadyExists = item.Product && item.Product.some(p => (p.ProductID === productId || p.POS_ProductID === productId));
                        if (!alreadyExists) {
                            // Call API to add product to menu item
                            await newMenuItemProduct({ FK_MenuItemID: menuItemId, FK_ProductID: draggedProduct.ProductID || draggedProduct.POS_ProductID });
                            const newProd = {
                                POS_MenuItemProductID: Math.random(), // temp ID
                                ProductID: draggedProduct.ProductID || draggedProduct.POS_ProductID,
                                Product: draggedProduct.ProductName || draggedProduct.Product
                            };
                            item.Product = item.Product ? [...item.Product, newProd] : [newProd];
                        }
                    }
                    if (item.ChildItem && item.ChildItem.length > 0) {
                        item.ChildItem = await addProduct(item.ChildItem);
                    }
                    return item;
                }));
            }
            (async () => {
                const updatedMenuItems = await addProduct(menuData.MenuItems);
                setMenuData(prev => ({ ...prev, MenuItems: updatedMenuItems }));
            })();
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
                        <h4 className="mb-1">{menuData ? `${menuData.MenuName} Menu` : "Menu Tree"}</h4>
                    </div>
                    <div className="page-btn">
                        <button className="btn btn-success mb-3" onClick={() => openNewItemModal()}>Add New Menu Item</button>
                    </div>
                </div>
                {showNewModal && (
                    <div className="modal fade show" style={{ display: 'block', background: 'rgba(0,0,0,0.3)' }}>
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">
                                        {parentItemId ? 'Add Sub Menu Item' : 'Add New Menu Item'}
                                    </h5>
                                    <button type="button" className="btn-close" onClick={() => {
                                        setShowNewModal(false);
                                        setParentItemId(null);
                                    }}></button>
                                </div>
                                <form onSubmit={async e => {
                                    e.preventDefault();
                                    if (!newItemName.trim()) return;
                                    // Call newMenuItem service
                                    const payload = {
                                        FK_MenuID: menuData?.MenuID || 0,
                                        Item: newItemName,
                                        Description: newItemDesc,
                                        FK_POS_MenuItemID: parentItemId // Use the parent item ID
                                    };
                                    if (newItemImage) {
                                        payload.ImageFile = newItemImage;
                                    }

                                    try {
                                        const res = await newMenuItem(payload);
                                        debugger;
                                        if (res.Success) {
                                            await fetchData();
                                            setShowNewModal(false);
                                            setNewItemName("");
                                            setNewItemDesc("");
                                            setNewItemImage(null);
                                            setParentItemId(null);
                                        } else {
                                            alert(res.Messages?.[0] || "Something went wrong.");
                                        }
                                    } catch (err) {
                                        alert("Error adding menu item.");
                                    }
                                }}>
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
                                            onChange={e => setNewItemName(e.target.value)}
                                        />
                                        <input
                                            type="text"
                                            className="form-control mb-2"
                                            placeholder="Description"
                                            value={newItemDesc}
                                            onChange={e => setNewItemDesc(e.target.value)}
                                        />
                                        <input
                                            name="ImageFile"
                                            type="file"
                                            accept="image/*"
                                            className="form-control"
                                            onChange={e => setNewItemImage(e.target.files[0])}
                                        />
                                    </div>
                                    <div className="modal-footer">
                                        <button type="button" className="btn btn-secondary" onClick={() => {
                                            setShowNewModal(false);
                                            setParentItemId(null);
                                        }}>Cancel</button>
                                        <button type="submit" className="btn btn-primary">Add</button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                )}

                {/* Edit Menu Item Modal */}
                {showEditModal && editItem && (
                    <div className="modal fade show" style={{ display: 'block', background: 'rgba(0,0,0,0.3)' }}>
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">Edit Menu Item</h5>
                                    <button type="button" className="btn-close" onClick={() => {
                                        setShowEditModal(false);
                                        setEditItem(null);
                                    }}></button>
                                </div>
                                <form onSubmit={async e => {
                                    e.preventDefault();
                                    if (!editItemName.trim()) return;
                                    // Prepare payload for updateMenuItem
                                    const payload = {
                                        POS_MenuItemID: editItem.ItemID,
                                        FK_MenuID: menuData?.MenuID || 0,
                                        Item: editItemName,
                                        Description: editItemDesc,
                                        FK_POS_MenuItemID: editItem.FK_POS_MenuItemID || null
                                    };
                                    debugger;
                                    if (editItemImage) {
                                        payload.ImageFile = editItemImage;
                                    }
                                    try {
                                        const res = await updateMenuItem(payload);
                                        debugger;
                                        if (res.Success) {
                                            await fetchData();
                                            setShowEditModal(false);
                                            setEditItem(null);
                                            setEditItemImage(null);
                                        } else {
                                            alert(res.Messages?.[0] || "Something went wrong.");
                                        }
                                    } catch (err) {
                                        alert("Error updating menu item.");
                                    }
                                }}>
                                    <div className="modal-body">
                                        <input
                                            type="text"
                                            className="form-control mb-2"
                                            placeholder="Menu item name"
                                            value={editItemName}
                                            onChange={e => setEditItemName(e.target.value)}
                                        />
                                        <input
                                            type="text"
                                            className="form-control"
                                            placeholder="Description"
                                            value={editItemDesc}
                                            onChange={e => setEditItemDesc(e.target.value)}
                                        />
                                        <input
                                            name="ImageFile"
                                            type="file"
                                            accept="image/*"
                                            className="form-control"
                                            onChange={e => setEditItemImage(e.target.files[0])}
                                        />
                                    </div>
                                    <div className="modal-footer">
                                        <button type="button" className="btn btn-secondary" onClick={() => {
                                            setShowEditModal(false);
                                            setEditItem(null);
                                        }}>Cancel</button>
                                        <button type="submit" className="btn btn-primary">Update</button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                )}

                <DndContext onDragEnd={handleDragEnd}>
                    <div className="row">
                        {/* Left: Menu Tree */}
                        <div className="col-md-7" style={{ maxHeight: '70vh', overflowY: 'auto' }}>
                            <div className="mb-3">
                                <h5 className="mb-3">Menu Items</h5>
                                {menuData && menuData.MenuItems ? (
                                    renderMenuTree(menuData.MenuItems, 0, 'root')
                                ) : (
                                    <p>Loading menu...</p>
                                )}
                            </div>
                        </div>

                        {/* Right: Products */}
                        <div className="col-md-5">
                            <div className="card shadow-sm">
                                <div className="card-header bg-white">
                                    <h5 className="mb-0">All Products</h5>
                                </div>
                                <div style={{ maxHeight: '70vh', overflowY: 'auto', padding: '16px' }}>
                                    {(productList && productList.length > 0) ? (
                                        <ul className="list-group">
                                            {productList.map(product => (
                                                <DraggableProduct product={product} key={product.POS_ProductID} />
                                            ))}
                                        </ul>
                                    ) : (
                                        <div className="text-muted">No products available</div>
                                    )}
                                    <DragOverlay>
                                        {activeProduct ? (
                                            <div className="list-group-item bg-primary text-white border border-primary" style={{ fontWeight: 600, fontSize: '1.1em', boxShadow: '0 2px 8px rgba(0,0,0,0.15)' }}>
                                                Dragging: {activeProduct.ProductName}
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