import React, { useState, useEffect } from "react";
import { getAllProducts } from "../../services/product/product";
import { getAllCostCenterProduct, newCostCenterProduct, updateCostCenterProduct } from "../../services/stock/costCenterProductService";
import { getAllUnits, getAllTaxType } from "../../services/syncList/syncService";
import { getAllCostCenter } from "../../services/debtors/costCenter";


import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import CostCenterProductForm from "../../core/modals/stocks/costCenterProductModel";
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';


const CostCenterProduct = () => {
    const [listData, setListData] = useState([]);
    const [unitListData, setUnitListData] = useState([]);
    const [productList, setProductList] = useState([]);
    const [taxList, setTaxList] = useState([]);
    const [costCenterList, setCostCenterList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const recordsPerPage = 10;

    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const [selectedCostCenterId, setSelectedCostCenterId] = useState(null);
    const navigate = useNavigate();


    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const type = await getAllCostCenter();
            setCostCenterList(type);
            const prod = await getAllProducts();
            setProductList(prod);
            const unit = await getAllUnits();
            setUnitListData(unit);
            const tax = await getAllTaxType();
            setTaxList(tax);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    };

    const filteredData = listData.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    // ✅ pagination logic
    const totalPages = Math.ceil(filteredData.length / recordsPerPage);
    const startIndex = (currentPage - 1) * recordsPerPage;
    const currentData = filteredData.slice(startIndex, startIndex + recordsPerPage);

    const goToPage = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
        }
    };
    const handleShow = () => {
        if (selectedCostCenterId == null) {
            alert("Please first select any cost center");
            return;
        }
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);
    const handleAddProduct = async (data) => {
        debugger;
        try {
            if (data.POS_CostCenterProductID) {
                await updateCostCenterProduct(data);
            }
            else {
                await newCostCenterProduct(data);
            }
            setListData([]);
            const rec = await getAllCostCenterProduct(selectedCostCenterId);
            setListData(rec);
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditProduct = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };

    const handleCostCenter = async (e) => {
        const selectedId = e.target.value;
        if (selectedId == "") {
            setListData([]);
        }
        else {
            setSelectedCostCenterId(selectedId);
            const data = await getAllCostCenterProduct(selectedId);
            setListData(data);
        }
    };


    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Cost Center Products</h4>
                            <h6>Manage Your product</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Product
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
                                <select className="form-select" onChange={handleCostCenter}>
                                    <option value="">Filter by Cost Center</option>
                                    {costCenterList.map((item, index) => (
                                        <option key={index} value={item.CostCenterID}>
                                            {item.Name}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Product Name</th>
                                        <th>Location </th>
                                        <th>Rate</th>
                                        <th>Value</th>
                                        <th>Vat</th>
                                        <th>Item Price</th>
                                        <th>Symbol</th>
                                        <th>Unit</th>
                                        <th>Quantity On Hand</th>
                                        <th>Is Available</th>
                                        <th>Is Active</th>
                                        <th>Created By</th>
                                        <th>Updated By</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {currentData.length > 0 ? (
                                        currentData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ProductName || "N/A"}</td>
                                                <td>{item.Debtor || "N/A"}</td>
                                                <td>{item.Rate}</td>
                                                <td>{item.Value}</td>
                                                <td>{item.Vat}</td>
                                                <td>{item.ItemPrice}</td>
                                                <td>{item.Symbol || "N/A"}</td>
                                                <td>{item.Unit || "N/A"}</td>
                                                <td>{item.QuantityOnHand}</td>
                                                <td>{item.IsAvailable ? "Yes" : "No"}</td>
                                                <td>{item.IsActive ? "Yes" : "No"}</td>
                                                <td>{item.CreatedBy || "N/A"}</td>
                                                <td>{item.UpdatedBy || "N/A"}</td>
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
                                            <td colSpan="14" className="text-center">
                                                No records found - Please select cost center
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
            {showModel && (
                <CostCenterProductForm
                    onSubmit={handleAddProduct}
                    showModel={showModel}
                    handleClose={handleClose}
                    data={selectedData}
                    productList={productList}
                    taxList={taxList}
                    sellUnitList={unitListData}
                    costCenterId={selectedCostCenterId}
                />
            )}
        </div>
    );
};


export default CostCenterProduct;
