import React, { useState, useEffect } from "react";
import { getAllMenu, newMenu, updateMenu, copyMenu } from "../../services/menu/menuService";
import { getAllDebtors } from "../../services/debtors/debtors";
import { getAllCostCenter } from "../../services/debtors/costCenter";


import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";
import MenuForm from "../../core/modals/menu/menuFormModel";
import { useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import CopyMenuForm from "../../core/modals/menu/copyMenuFormModel";


const MenuPage = () => {
    const [listData, setListData] = useState([]);
    const [debtorList, setDebtorList] = useState([]);
    const [costCenterList, setCostCenterList] = useState([]);
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const [selectedDebtor, setSelectedDebtor] = useState(0);
    const debtorId = useSelector((state) => state.selectedDebtorStore);
    const navigate = useNavigate();
    const [showCopyModel, setCopyModelShow] = useState(false);
    const [searchTerm, setSearchTerm] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const recordsPerPage = 10;

    useEffect(() => {
        fetchRecords();
        fetchDebtorCostCenter();
    }, [debtorId]);

    const fetchRecords = async () => {
        try {
            setSelectedDebtor(debtorId == null ? 1 : debtorId);
            const data = await getAllMenu(parseInt(debtorId == null ? 5 : debtorId));
            console.log("debtorId ", debtorId);
console.log(data);

            setListData(data);
        } catch (err) {
            console.error("Failed to load:", err.message);
        }
    }

    const fetchDebtorCostCenter = async () => {
        try {
            const data = await getAllDebtors();
            setDebtorList(data);
            const cost = await getAllCostCenter();
            setCostCenterList(cost);
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

    const totalPages = Math.ceil(filteredData.length / recordsPerPage);
    const startIndex = (currentPage - 1) * recordsPerPage;
    const currentData = filteredData.slice(startIndex, startIndex + recordsPerPage);

    const goToPage = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
        }
    };

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);
    const handleAddProduct = async (data) => {
        try {
            if (data.POS_MenuID) {
                await updateMenu(data);
            }
            else {
                await newMenu(data);
            }
            await fetchRecords();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditProduct = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };


    const handleMenuClick = (menuId, type) => {
        if (type == "Global") {
            navigate(`/menu-tree/${menuId}`);
        }
        else {
            navigate(`/menu-tree-camp/${menuId}`);
        }
    };

    const HandleCopyMenu = (record) => {
        setSelectedData(record);
        setCopyModelShow(true);

        fetchRecords();
    };

    const handleCopyClose = () => setCopyModelShow(false);

    const handleAddCopyMenu = async (data) => {
        console.log("Data : ", data);
        debugger;
        try {
            var response = await copyMenu(data);
            debugger;
            if (response.Success) {
                setCopyModelShow(false);
            }
            else {
                alert("something wrong!.")
            }
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };


    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>All Menu</h4>
                            <h6>Manage Your Menu</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Menu
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
                        </div>
                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Menu Name</th>
                                        <th>Location</th>
                                        <th>Is Active</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {currentData.length > 0 ? (
                                        currentData.map((item, index) => (
                                            <tr key={index}>
                                                <td
                                                    style={{ cursor: 'pointer', textDecoration: 'underline' }}
                                                    onClick={() => handleMenuClick(item.MenuID, item.SourceType)}
                                                >
                                                    {item.MenuName || "N/A"}
                                                </td>
                                                <td>{item.SourceType}</td>
                                                <td>{item.IsActive ? "Yes" : "No"}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditProduct(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                    {item.SourceType === "Global" && (
                                                        <button
                                                            type="button"
                                                            onClick={() => HandleCopyMenu(item)}
                                                            className="btn btn-sm btn-primary me-2"
                                                        >
                                                            Copy Menu
                                                        </button>
                                                    )}
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="4" className="text-center">
                                                No records found
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                            {totalPages > 1 && (<div className="d-flex justify-content-between align-items-center mt-3">
                                <span>
                                    Page {currentPage} of {totalPages}
                                </span>
                                <div>
                                    {Array.from({ length: totalPages }, (_, i) => (
                                        <Button
                                            key={i}
                                            variant={currentPage === i + 1 ? "primary" : "light"}
                                            size="sm"
                                            className="mx-1"
                                            onClick={() => goToPage(i + 1)}
                                        >
                                            {i + 1}
                                        </Button>
                                    ))}
                                </div>
                                <div>
                                    <Button
                                        variant="secondary"
                                        size="sm"
                                        disabled={currentPage === 1}
                                        onClick={() => setCurrentPage(currentPage - 1)}
                                    >
                                        Previous
                                    </Button>
                                    <Button
                                        variant="secondary"
                                        size="sm"
                                        className="ms-2"
                                        disabled={currentPage === totalPages}
                                        onClick={() => setCurrentPage(currentPage + 1)}
                                    >
                                        Next
                                    </Button>
                                </div>
                            </div>
                            )}
                        </div>
                    </div>
                </div>
            </div>
            {showModel &&
                <MenuForm
                    onSubmit={handleAddProduct}
                    showModel={showModel}
                    handleClose={handleClose}
                    data={selectedData}
                />
            }

            {showCopyModel &&
                <CopyMenuForm
                    onSubmit={handleAddCopyMenu}
                    showModel={showCopyModel}
                    handleClose={handleCopyClose}
                    data={selectedData}
                    debtorList={debtorList}
                    costCenterList={costCenterList}
                />
            }
        </div>
    );
};


export default MenuPage;
