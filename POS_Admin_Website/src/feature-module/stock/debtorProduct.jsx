import React, { useState, useEffect } from "react";
import { getAllProducts } from "../../services/product/product";
import { getAllDebtorProduct, newDebtorProduct, updateDebtorProduct } from "../../services/stock/locationProductService";
import { getAllUnits, getAllTaxType } from "../../services/syncList/syncService";


import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";
import DebtorProductForm from "../../core/modals/stocks/debtorProductModel";
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';


const DebtorProduct = () => {
    const [listData, setListData] = useState([]);
    const [unitListData, setUnitListData] = useState([]);
    const [productList, setProductList] = useState([]);
    const [taxList, setTaxList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const recordsPerPage = 10;

    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const debtorId = useSelector((state) => state.selectedDebtorStore);
    const navigate = useNavigate();


    useEffect(() => {
        fetchPurchaseOrder();
        fetchRecords();
    }, [debtorId]);

    const fetchPurchaseOrder = async () => {
        try {
            const data = await getAllDebtorProduct(debtorId == null ? 1 : debtorId);
            setListData(data);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    }

    const fetchRecords = async () => {
        try {
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
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);
    const handleAddProduct = async (data) => {
        try {
            if (data.POS_DebtorProductID) {
                await updateDebtorProduct(data);
            }
            else {
                await newDebtorProduct(data);
            }
            await fetchPurchaseOrder();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditProduct = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };

    const handleRedirect = (e, item) => {
        const value = e.target.value;
        if (!value) return;
        navigate(`/${value}/${item.POS_DebtorProductID}`);
    };


    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Location Products</h4>
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
                        <div className="table-top">
                            <div className="search-set">
                                <div className="search-input">
                                    <input
                                        type="text"
                                        placeholder="Search"
                                        className="form-control"
                                        value={searchTerm}
                                        onChange={(e) => setSearchTerm(e.target.value)}
                                    />
                                    <Link to className="btn btn-searchset">
                                        <i data-feather="search" className="feather-search" />
                                    </Link>
                                </div>
                            </div>
                        </div>
                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Product Name</th>
                                        <th>Location</th>
                                        <th>Cost Price</th>
                                        <th>Symbol</th>
                                        <th>Unit</th>
                                        <th>Quantity On Hand</th>
                                        <th>Is Available</th>
                                        <th>Is Active</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {currentData.length > 0 ? (
                                        currentData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ProductName || "N/A"}</td>
                                                <td>{item.Debtor || "N/A"}</td>
                                                <td>{item.CostPrice || "N/A"}</td>
                                                <td>{item.Symbol || "N/A"}</td>
                                                <td>{item.Unit || "N/A"}</td>
                                                <td>{item.QuantityOnHand}</td>
                                                <td>{item.IsAvailable ? "Yes" : "No"}</td>
                                                <td>{item.IsActive ? "Yes" : "No"}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditProduct(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                    <select
                                                        className="form-select form-select-sm d-inline-block"
                                                        style={{ width: "140px" }}
                                                        onChange={(e) => handleRedirect(e, item)}
                                                        defaultValue=""
                                                    >
                                                        <option value="" disabled>
                                                            Select Action
                                                        </option>
                                                        <option value="debtor-product-price">Prices</option>
                                                    </select>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="9" className="text-center">
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
            {showModel && (
                <DebtorProductForm
                    onSubmit={handleAddProduct}
                    showModel={showModel}
                    handleClose={handleClose}
                    data={selectedData}
                    productList={productList}
                    taxList={taxList}
                    sellUnitList={unitListData}
                />
            )}
        </div>
    );
};


export default DebtorProduct;
