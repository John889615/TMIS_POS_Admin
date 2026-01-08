import React, { useState, useEffect } from "react";
import { getAllCostCenter } from "../../services/debtors/costCenter";
import { getAllPurchaseOrder, newPurchaseOrder, updatePurchaseOrder } from "../../services/stock/purchaseOrderService";


import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import PurchaseOrderForm from "../../core/modals/stocks/purchaseOrderFormModel";
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';


const PurchaseOrderPage = () => {
    const [listData, setListData] = useState([]);
    const [costCenterList, setCostCenterList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
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
            const data = await getAllPurchaseOrder(debtorId == null ? 1 : debtorId);
            setListData(data);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    }

    const fetchRecords = async () => {
        try {
            const type = await getAllCostCenter();
            setCostCenterList(type);
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

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);
    const handleAddProduct = async (data) => {
        console.log("Data : ", data);
        try {
            if (data.POS_PurchaseOrderID) {
                await updatePurchaseOrder(data);
            }
            else {
                await newPurchaseOrder(data);
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

    const handlePurchseOrderClick = (menuId) => {
        navigate(`/purchase-order-tree/${menuId}`);
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Purchase Orders</h4>
                            <h6>Manage Your Order</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Order
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
                                        <th>Order Number</th>
                                        <th>Supplier Name</th>
                                        <th>Debtor Name</th>
                                        <th>Cost Center Name</th>
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
                                                <td style={{ cursor: 'pointer', textDecoration: 'underline' }}
                                                    onClick={() => handlePurchseOrderClick(item.POS_PurchaseOrderID)}
                                                >
                                                    {item.OrderNumber || "N/A"}
                                                </td>
                                                <td>{item.SupplierName || "N/A"}</td>
                                                <td>{item.DebtorName || "N/A"}</td>
                                                <td>{item.CostCenterName || "N/A"}</td>
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
                                            <td colSpan="9" className="text-center">
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
            <PurchaseOrderForm
                onSubmit={handleAddProduct}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
                costCenterList={costCenterList}
            />
        </div>
    );
};


export default PurchaseOrderPage;
