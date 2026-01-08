import React, { useState, useEffect } from "react";
import { getAllProducts } from "../../services/product/product";
import { getAllStockRequest } from "../../services/stock/stockRequestService";
import { getAllStockRequestLine, newStockRequestLine } from "../../services/stock/stockRequestLineService";


import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import StockRequestLineForm from "../../core/modals/stocks/stockRequestLineFormModel";
import { useSelector } from 'react-redux';



const StockRequestLinePage = () => {
    const [listData, setListData] = useState([]);
    const [stockRequestList, setStockRequestList] = useState([]);
    const [productList, setProductList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const debtorId = useSelector((state) => state.selectedDebtorStore);
    const [selectedStockRequest, setSelectedStockRequest] = useState(0);
    useEffect(() => {
        fetchPurchaseOrder();
        fetchRecords();
    }, [debtorId]);

    const fetchPurchaseOrder = async () => {
        try {
            const data = await getAllStockRequest(debtorId == null ? 1 : debtorId);
            setStockRequestList(data);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    }

    const fetchRecords = async () => {
        try {
            const type = await getAllProducts();
            setProductList(type);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    };

    const handlePurchaseOrder = async (e) => {
        const selectedId = e.target.value;
        if (selectedId == "") {
            setListData([]);
        }
        else {
            setSelectedStockRequest(selectedId);
            const data = await getAllStockRequestLine(selectedId);
            setListData(data);
        }
    };

    const filteredData = listData.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);
    const handlePurchaseOrderLine = async (data) => {
        console.log("Data : ", data);
        try {
            await newStockRequestLine(data);
            setListData([]);
            const list = await getAllStockRequestLine(selectedStockRequest);
            setListData(list);
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    // const handleEditProduct = (record) => {
    //     setSelectedData(record);
    //     setModelShow(true);
    // };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Stock Request Line</h4>
                            <h6>Manage Your Stock Line</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Request Line
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
                                <select className="form-select" onChange={handlePurchaseOrder}>
                                    <option value="">Filter by Stock Request</option>
                                    {stockRequestList.map((item, index) => (
                                        <option key={index} value={item.POS_StockRequestID}>
                                            {item.RefNumber}
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
                                        <th>Quantity</th>
                                        <th>Notes</th>
                                        <th>IsDeclined</th>
                                        <th>Manager Notes</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ProductName || "N/A"}</td>
                                                <td>{item.Quantity || "N/A"}</td>
                                                <td>{item.Notes || "N/A"}</td>
                                                <td>{item.IsDeclined ? "Yes" : "No"}</td>
                                                <td>{item.ManagerNotes || "N/A"}</td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="5" className="text-center">
                                                No records found - Please select Stock Request
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <StockRequestLineForm
                onSubmit={handlePurchaseOrderLine}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
                productList={productList}
                stockRequestList={stockRequestList}
            />
        </div>
    );
};


export default StockRequestLinePage;
