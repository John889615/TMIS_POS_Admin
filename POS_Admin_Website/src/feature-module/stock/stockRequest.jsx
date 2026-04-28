import React, { useState, useEffect } from "react";
import { getAllStockRequest, newStockRequest, updateStockRequest } from "../../services/stock/stockRequestService";


import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import StockRequestForm from "../../core/modals/stocks/stockRequestFormModel";
import { useSelector } from 'react-redux';



const StockRequestPage = () => {
    const [listData, setListData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const debtorId = useSelector((state) => state.selectedDebtorStore);

    useEffect(() => {
        fetchStockRequest();
    }, [debtorId]);

    const fetchStockRequest = async () => {
        try {
            const data = await getAllStockRequest(debtorId == null ? 1 : debtorId);
            setListData(data);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
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
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);
    const handleAddProduct = async (data) => {
        console.log("Data : ", data);
        try {
            if (data.POS_StockRequestID) {
                await updateStockRequest(data);
            }
            else {
                const response = await newStockRequest(data);

                // Check if API responded with success = false
                if (!response.Success) {
                    // Optional: Show toast, alert, or set error state
                    console.warn("API Error:", response.Messages?.[0] || "Unknown error");
                    alert(response.Messages?.[0] || "Something went wrong.");
                    return; // Stop further execution
                }
            }
            await fetchStockRequest();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditProduct = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Stock Request</h4>
                            <h6>Manage Your request</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Request
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
                                        <th>Ref. Number</th>
                                        <th>From Debtor</th>
                                        <th>To Debtor</th>
                                        <th>Order Status</th>
                                        <th>Created By</th>
                                        <th>Notes</th>
                                        <th>Manager Notes</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.RefNumber || "N/A"}</td>
                                                <td>{item.FromDebtorName || "N/A"}</td>
                                                <td>{item.ToDebtorName || "N/A"}</td>
                                                <td>{item.OrderStatus || "N/A"}</td>
                                                <td>{item.CreatedBy || "N/A"}</td>
                                                <td>{item.Notes || "N/A"}</td>
                                                <td>{item.ManagerNotes || "N/A"}</td>
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
                                            <td colSpan="8" className="text-center">
                                                No records found
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <StockRequestForm
                onSubmit={handleAddProduct}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
            />
        </div>
    );
};


export default StockRequestPage;
