import React, { useState, useEffect } from "react";
import { getAllPurchaseOrder } from "../../services/stock/purchaseOrderService";
import { getAllPurchaseOrderSubmittedLine, updatePurchaseOrderSubmittedline } from "../../services/stock/purchaseOrderSubmittedLineService";


import { Link } from "react-router-dom";
import PurchaseOrderSubmittedLineForm from "../../core/modals/stocks/purchaseOrderSubmittedLineFormModel";
import { useSelector } from 'react-redux';



const PurchaseOrderSubmittedLinePage = () => {
    const [listData, setListData] = useState([]);
    const [purchaseOrderList, setPurchaseOrderList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const debtorId = useSelector((state) => state.selectedDebtorStore);
    const [selectedPurchaseOrder, setSelectedPurchaseOrder] = useState(0);
    useEffect(() => {
        fetchPurchaseOrder();
    }, [debtorId]);

    const fetchPurchaseOrder = async () => {
        try {
            const data = await getAllPurchaseOrder(debtorId == null ? 1 : debtorId);
            setPurchaseOrderList(data);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    }

    const handlePurchaseOrder = async (e) => {
        const selectedId = e.target.value;
        if (selectedId == "") {
            setListData([]);
        }
        else {
            setSelectedPurchaseOrder(selectedId);
            const data = await getAllPurchaseOrderSubmittedLine(selectedId);
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


    const handleClose = () => setModelShow(false);
    const handlePurchaseOrderLine = async (data) => {
        console.log("Data : ", data);
        try {
            await updatePurchaseOrderSubmittedline(data);
            setListData([]);
            const list = await getAllPurchaseOrderSubmittedLine(selectedPurchaseOrder);
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

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Submitted Purchase Order Line</h4>
                            <h6>Manage Your Order Line</h6>
                        </div>
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
                                    <option value="">Filter by Purchase Order</option>
                                    {purchaseOrderList.map((item, index) => (
                                        <option key={index} value={item.POS_PurchaseOrderID}>
                                            {item.OrderNumber}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Purchase Order Id</th>
                                        <th>ProductName</th>
                                        <th>Quantity</th>
                                        <th>Unit Cost Excl</th>
                                        <th>Unit Cost Incl</th>
                                        <th>Tax Type ID</th>
                                        <th>Tax Rate</th>
                                        <th>Total Cost Excl</th>
                                        <th>Total Cost Incl</th>
                                        <th>Notes</th>
                                        <th>IsDeclined</th>
                                        <th>Manager Notes</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.PurchaseOrderID || "N/A"}</td>
                                                <td>{item.ProductName || "N/A"}</td>
                                                <td>{item.Quantity || "N/A"}</td>
                                                <td>{item.UnitCostExcl || "N/A"}</td>
                                                <td>{item.UnitCostIncl || "N/A"}</td>
                                                <td>{item.TaxTypeID || "N/A"}</td>
                                                <td>{item.TaxRate || "N/A"}</td>
                                                <td>{item.TotalCostExcl || "N/A"}</td>
                                                <td>{item.TotalCostIncl || "N/A"}</td>
                                                <td>{item.Notes || "N/A"}</td>
                                                <td>{item.IsDeclined ? "Yes" : "No"}</td>
                                                <td>{item.MangerNotes || "N/A"}</td>
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
                                            <td colSpan="13" className="text-center">
                                                No records found - Please select Purchase Order
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <PurchaseOrderSubmittedLineForm
                onSubmit={handlePurchaseOrderLine}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
            />
        </div>
    );
};


export default PurchaseOrderSubmittedLinePage;
