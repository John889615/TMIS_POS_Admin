import React, { useState, useEffect } from "react";
import { getAllMenu } from "../../services/menu/menuService";
import { getAllMenuItem } from "../../services/menu/menuItemService";
import { getAllMenuItemProducts, newMenuItemProduct, deleteMenuItemProduct } from "../../services/menu/menuItemProductService";
import { getAllProducts } from "../../services/product/product";


import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import MenuItemProductForm from "../../core/modals/menu/menuItemProductFormModel";

const MenuItemProductPage = () => {
    const [listData, setListData] = useState([]);
    const [menulistData, setMenuListData] = useState([]);
    const [menItemulistData, setMenuItemListData] = useState([]);
    const [productList, setProductList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const [selectedMenu, setSelectedMenu] = useState(0);


    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const data = await getAllMenu();
            setMenuListData(data);
            const prod = await getAllProducts();
            setProductList(prod);
        } catch (err) {
            console.error("Failed to load:", err.message);
        }
    }

    const filteredData = listData.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    const handleShow = () => {
        if (selectedMenu == 0) {
            alert("Please first select any menu item");
            return;
        }
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);

    const handleAddProduct = async (data) => {
        try {
            const response = await newMenuItemProduct(data);
            if (!response.Success) {
                console.warn("API Error:", response.Messages?.[0] || "Unknown error");
                alert(response.Messages?.[0] || "Something went wrong.");
                return;
            }
            setListData([]);
            const list = await getAllMenuItemProducts(selectedMenu);
            setListData(list);
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleDeleteProduct = async (record) => {
        const confirmed = window.confirm("Are you sure you want to delete this product?");
        if (!confirmed) return;
        await deleteMenuItemProduct(record.POS_MenuItemProductID);
        setListData([]);
        const list = await getAllMenuItemProducts(selectedMenu);
        setListData(list);
    };

    const HandleMenuList = async (e) => {
        const selectedId = e.target.value;
        if (selectedId == "") {
            setListData([]);
        }
        else {
            const data = await getAllMenuItem(selectedId);
            setMenuItemListData(data);
        }
    };

    const HandleMenuItemList = async (e) => {
        const selectedId = e.target.value;
        if (selectedId == "") {
            setListData([]);
        }
        else {
            setSelectedMenu(selectedId);
            const data = await getAllMenuItemProducts(selectedId);
            setListData(data);
        }
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>All Menu Item Products</h4>
                            <h6>Manage Your Menu Product</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Menu Item Product
                        </Button>
                    </div>
                </div>
                <div className="card table-list-card">
                    <div className="card-body">
                        <div className="table-top d-flex justify-content-between align-items-center">
                            {/* Search Input - Left Side */}
                            <div className="search-set">
                                <div className="search-input">
                                    <input
                                        type="text"
                                        placeholder="Search"
                                        className="form-control"
                                        value={searchTerm}
                                        onChange={(e) => setSearchTerm(e.target.value)}
                                    />
                                    <Link to="#" className="btn btn-searchset">
                                        <i data-feather="search" className="feather-search" />
                                    </Link>
                                </div>
                            </div>

                            {/* Select Dropdown - Right Side */}
                            <div className="form-group mb-0">
                                <select className="form-select" onChange={HandleMenuList}>
                                    <option value="">Filter by Menus</option>
                                    {menulistData.map((item, index) => (
                                        <option key={index} value={item.POS_MenuID}>
                                            {item.MenuName}
                                        </option>
                                    ))}
                                </select>
                            </div>
                            <div className="form-group mb-0">
                                <select className="form-select" onChange={HandleMenuItemList}>
                                    <option value="">Filter by Menu Items</option>
                                    {menItemulistData.map((item, index) => (
                                        <option key={index} value={item.POS_MenuItemID}>
                                            {item.Item}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Item</th>
                                        <th>Product</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.Item || "N/A"}</td>
                                                <td>{item.ProductName || "N/A"}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleDeleteProduct(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-trash"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="5" className="text-center">
                                                No records found - Please select Menu/Menu items option
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <MenuItemProductForm
                onSubmit={handleAddProduct}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
                menuItemList={menItemulistData}
                productList={productList}
            />
        </div>
    );
};


export default MenuItemProductPage;
