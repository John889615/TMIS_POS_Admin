import React, { useState, useEffect } from "react";
import {
    getAllStockRequest,
    newStockRequest,
    updateStockRequest,
    submitStockRequest,
    approveStockRequest,
} from "../../services/stock/stockRequestService";
import { getAllStockRequestLine } from "../../services/stock/stockRequestLineService";
import { getAllProducts } from "../../services/product/product";
import { getAllUnits } from "../../services/product/units";


import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
    Eye,
    Edit2,
} from "react-feather";
import StockRequestForm from "../../core/modals/stocks/stockRequestFormModel";
import StockRequestApprovalForm from "../../core/modals/stocks/stockRequestApprovalFormModel";
import { useSelector } from 'react-redux';



const StockRequestPage = () => {
    const [listData, setListData] = useState([]);
    const [productList, setProductList] = useState([]);
    const [unitList, setUnitList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const [selectedLines, setSelectedLines] = useState([]);
    const [showApprovalModel, setApprovalModelShow] = useState(false);
    const [approvalData, setApprovalData] = useState(null);
    const [approvalLines, setApprovalLines] = useState([]);
    const [approvalReadOnly, setApprovalReadOnly] = useState(false);
    const debtorId = useSelector((state) => state.selectedDebtorStore);

    useEffect(() => {
        fetchStockRequest();
        fetchProducts();
        fetchUnits();
    }, [debtorId]);

    const fetchStockRequest = async () => {
        try {
            const data = await getAllStockRequest(debtorId == null ? 1 : debtorId);
            setListData(data);
        } catch (err) {
            console.error("Failed to load stock requests:", err.message);
        }
    }

    const fetchProducts = async () => {
        try {
            const data = await getAllProducts();
            setProductList(data);
        } catch (err) {
            console.error("Failed to load products:", err.message);
        }
    }

    const fetchUnits = async () => {
        try {
            const data = await getAllUnits();
            setUnitList(data);
        } catch (err) {
            console.error("Failed to load units:", err.message);
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
        setSelectedLines([]);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);

    const handleSaveStockRequest = async (data, shouldSubmit) => {
        try {
            let response;
            let stockRequestId = data.POS_StockRequestID;

            if (stockRequestId) {
                response = await updateStockRequest(data);
            } else {
                response = await newStockRequest(data);
            }

            if (response && response.Success === false) {
                alert(response.Messages?.[0] || "Something went wrong.");
                return;
            }

            if (!stockRequestId) {
                stockRequestId = response?.Data?.POS_StockRequestID;
            }

            if (shouldSubmit && stockRequestId) {
                const submitResponse = await submitStockRequest({ POS_StockRequestID: stockRequestId });
                if (submitResponse && submitResponse.Success === false) {
                    alert(submitResponse.Messages?.[0] || "Saved, but submit for approval failed.");
                    await fetchStockRequest();
                    return;
                }
            }

            await fetchStockRequest();
            setModelShow(false);
        } catch (err) {
            console.error("Error saving stock request:", err.message);
        }
    };

    const handleEditRecord = async (record) => {
        try {
            const lines = await getAllStockRequestLine(record.POS_StockRequestID);
            setSelectedData(record);
            setSelectedLines(lines || []);
            setModelShow(true);
        } catch (err) {
            console.error("Failed to load lines for edit:", err.message);
        }
    };

    const handleView = async (record) => {
        try {
            const lines = await getAllStockRequestLine(record.POS_StockRequestID);
            setApprovalData(record);
            setApprovalLines(lines || []);
            setApprovalReadOnly(!isPending(record));
            setApprovalModelShow(true);
        } catch (err) {
            console.error("Failed to load lines for view:", err.message);
        }
    };

    const handleApprovalSubmit = async (data) => {
        try {
            const response = await approveStockRequest(data);
            if (response && response.Success === false) {
                alert(response.Messages?.[0] || "Something went wrong.");
                return;
            }
            setApprovalModelShow(false);
            await fetchStockRequest();
        } catch (err) {
            console.error("Error approving stock request:", err.message);
        }
    };

    const isDraft = (item) => (item.OrderStatus || '').toLowerCase() === 'draft';
    const isPending = (item) => (item.OrderStatus || '').toLowerCase() === 'pending';

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
                                                        onClick={() => handleView(item)}
                                                        title={isPending(item) ? 'Review for approval' : 'View'}
                                                        className="btn btn-sm btn-outline-secondary me-2">
                                                        <Eye size={14} />
                                                    </button>
                                                    {isDraft(item) && (
                                                        <button type='button'
                                                            onClick={() => handleEditRecord(item)}
                                                            title="Update"
                                                            className="btn btn-sm btn-primary me-2">
                                                            <Edit2 size={14} />
                                                        </button>
                                                    )}
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
                onSubmit={handleSaveStockRequest}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
                existingLines={selectedLines}
                productList={productList}
                unitList={unitList}
            />
            <StockRequestApprovalForm
                onSubmit={handleApprovalSubmit}
                showModel={showApprovalModel}
                handleClose={() => setApprovalModelShow(false)}
                data={approvalData}
                lines={approvalLines}
                readOnly={approvalReadOnly}
            />
        </div>
    );
};


export default StockRequestPage;
