import React, { useState, useEffect } from "react";
import { getMenuWithItem } from "../../services/menu/menuService";

const OrderPlacePage = () => {
    const [menus, setMenus] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [cart, setCart] = useState([]);

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const data = await getMenuWithItem();
            setMenus(data);
        } catch (err) {
            console.error("Failed to load:", err.message);
        }
    };

    // Add item to cart
    const addToCart = (item) => {
        setCart((prevCart) => {
            const existing = prevCart.find((i) => i.ItemID === item.ItemID);
            if (existing) {
                return prevCart.map((i) =>
                    i.ItemID === item.ItemID
                        ? { ...i, quantity: i.quantity + 1 }
                        : i
                );
            } else {
                return [...prevCart, { ...item, quantity: 1 }];
            }
        });
    };

    // Remove item from cart
    const removeFromCart = (itemId) => {
        setCart((prevCart) => prevCart.filter((i) => i.ItemID !== itemId));
    };

    // Change quantity
    const changeQuantity = (itemId, delta) => {
        setCart((prevCart) =>
            prevCart
                .map((i) =>
                    i.ItemID === itemId
                        ? { ...i, quantity: Math.max(1, i.quantity + delta) }
                        : i
                )
        );
    };

    // Filter menus by search term
    const filteredMenus = menus
        .filter((menu) =>
            menu.MenuName.toLowerCase().includes(searchTerm.toLowerCase()) ||
            (menu.MenuItems && menu.MenuItems.some(item =>
                item.Item && item.Item.toLowerCase().includes(searchTerm.toLowerCase())
            ))
        );

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Order Place</h4>
                            <h6>Select Menu Items and Add to Cart</h6>
                        </div>
                    </div>
                </div>
                <div className="row" style={{ height: 'calc(100vh - 200px)' }}>
                    {/* Menus - Left Side (col-8) */}
                    <div className="col-md-8" style={{ height: '100%' }}>
                        <div className="card h-100">
                            <div className="card-body d-flex flex-column" style={{ height: '100%' }}>
                                <div className="search-set mb-3">
                                    <input
                                        type="text"
                                        placeholder="Search menu or item..."
                                        className="form-control"
                                        value={searchTerm}
                                        onChange={(e) => setSearchTerm(e.target.value)}
                                    />
                                </div>
                                <div style={{ flex: 1, overflowY: 'auto', overflowX: 'hidden' }}>
                                    {filteredMenus.length > 0 ? (
                                        filteredMenus.map((menu) => (
                                            <div key={menu.MenuID} className="mb-4">
                                                <h5 className="mb-2">{menu.MenuName}</h5>
                                                <div className="row">
                                                    {menu.MenuItems && menu.MenuItems.length > 0 ? (
                                                        menu.MenuItems.map((item) => (
                                                            <div key={item.ItemID} className="col-sm-6 col-lg-3 mb-3">
                                                                <div className="card h-100 d-flex flex-column justify-content-between" style={{ minHeight: '100px', margin: '0' }}>
                                                                    <div className="card-body p-2 d-flex flex-column justify-content-between">
                                                                        <h6 className="card-title mb-2" style={{ fontSize: '1rem', fontWeight: '500' }}>{item.Item}</h6>
                                                                    </div>
                                                                    <div className="card-footer bg-transparent border-0 p-2">
                                                                        <button className="btn btn-success w-100 btn-sm" style={{ fontSize: '0.9rem' }} onClick={() => addToCart(item)}>
                                                                            Add to Cart
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        ))
                                                    ) : (
                                                        <div className="col-12"><div className="card"><div className="card-body">No items found.</div></div></div>
                                                    )}
                                                </div>
                                            </div>
                                        ))
                                    ) : (
                                        <div>No menus found.</div>
                                    )}
                                </div>
                            </div>
                        </div>
                    </div>
                    {/* Cart - Right Side (col-4) */}
                    <div className="col-md-4" style={{ position: 'sticky', top: 80, height: 'calc(100vh - 200px)' }}>
                        <div className="card h-100 d-flex flex-column justify-content-between">
                            <div>
                                <div className="card-header">
                                    <h5>Cart</h5>
                                </div>
                                <div className="card-body" style={{ height: cart.length > 7 ? '320px' : 'auto', overflowY: cart.length > 7 ? 'auto' : 'visible', paddingRight: '4px' }}>
                                    {cart.length > 0 ? (
                                        <table className="table mb-0">
                                            <thead>
                                                <tr>
                                                    <th>Item</th>
                                                    <th>Quantity</th>
                                                    <th>Actions</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {cart.map((item) => (
                                                    <tr key={item.ItemID}>
                                                        <td>{item.Item}</td>
                                                        <td>{item.quantity}</td>
                                                        <td>
                                                            <button className="btn btn-sm btn-secondary me-2" onClick={() => changeQuantity(item.ItemID, 1)}>+</button>
                                                            <button className="btn btn-sm btn-secondary me-2" onClick={() => changeQuantity(item.ItemID, -1)} disabled={item.quantity === 1}>-</button>
                                                            <button className="btn btn-sm btn-danger" onClick={() => removeFromCart(item.ItemID)}>Remove</button>
                                                        </td>
                                                    </tr>
                                                ))}
                                            </tbody>
                                        </table>
                                    ) : (
                                        <div>Cart is empty.</div>
                                    )}
                                </div>
                            </div>
                            <div className="card-footer bg-transparent border-0 mt-auto">
                                <button className="btn btn-primary w-100" disabled={cart.length === 0} onClick={() => alert('Order placed!')}>
                                    Place Order
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};


export default OrderPlacePage;
