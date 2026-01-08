import React, { useState, useEffect } from "react";
import { getAllMenu } from "../../services/menu/menuService";
import { getAllMenuItem, newMenuItem, updateMenuItem } from "../../services/menu/menuItemService";


import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import MenuItemForm from "../../core/modals/menu/menuItemFormModel";
import { useSelector } from 'react-redux';

const MenuItemPage = () => {
    const [listData, setListData] = useState([]);
    const [menulistData, setMenuListData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const [selectedMenu, setSelectedMenu] = useState(0);
    const debtorId = useSelector((state) => state.selectedDebtorStore);

    useEffect(() => {
        fetchRecords();
    }, [debtorId]);

    const fetchRecords = async () => {
        try {
            const data = await getAllMenu(debtorId == null ? 1 : debtorId);
            setMenuListData(data);
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
            alert("Please first select any menu");
            return;
        }
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);

    const handleAddProduct = async (data) => {
        data.FK_MenuID = parseInt(selectedMenu);
        console.log("Data : ", data);
        try {
            if (data.POS_MenuItemID) {
                await updateMenuItem(data);
            }
            else {
                const response = await newMenuItem(data);
                if (!response.Success) {
                    console.warn("API Error:", response.Messages?.[0] || "Unknown error");
                    alert(response.Messages?.[0] || "Something went wrong.");
                    return;
                }
            }
            setListData([]);
            const list = await getAllMenuItem(selectedMenu);
            setListData(list);
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditProduct = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };

    const HandleMenuItem = async (e) => {
        debugger;
        const selectedId = e.target.value;
        if (selectedId == "") {
            setListData([]);
        }
        else {
            setSelectedMenu(selectedId);
            const data = await getAllMenuItem(selectedId);
            setListData(data);
        }
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>All Menu Item</h4>
                            <h6>Manage Your Item</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Menu Item
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
                                <select className="form-select" onChange={HandleMenuItem}>
                                    <option value="">Filter by Menus</option>
                                    {menulistData.map((item, index) => (
                                        <option key={index} value={item.MenuID}>
                                            {item.MenuName}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Menu Name</th>
                                        <th>Item</th>
                                        <th>Description</th>
                                        <th>Parent Item</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.MenuName || "N/A"}</td>
                                                <td>{item.Item || "N/A"}</td>
                                                <td>{item.Description || "N/A"}</td>
                                                <td>{item.ParentItem || "N/A"}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditProduct(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="5" className="text-center">
                                                No records found - Please select Menu option
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <MenuItemForm
                onSubmit={handleAddProduct}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
                menuItemList={listData}
            />
        </div>
    );
};


export default MenuItemPage;
