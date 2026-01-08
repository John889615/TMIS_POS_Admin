import React, { useState, useEffect } from "react";
import { getAllSubmittedPurchaseOrder, newPurchaseOrderSubmitted } from "../../services/stock/purchaseOrderSubmittedService";
import { Link } from "react-router-dom";
import PurchaseOrderSubmittedForm from "../../core/modals/stocks/purchaseOrderSubmittedFormModel";



const PurchaseOrderSubmittedPage = () => {
    const [listData, setListData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const type = await getAllSubmittedPurchaseOrder();
            setListData(type);
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

    // const handleShow = () => {
    //     setSelectedData(null);
    //     setModelShow(true)
    // };

    const handleClose = () => setModelShow(false);
    const handleAddProduct = async (data) => {
        console.log("Data : ", data);
        try {
            await newPurchaseOrderSubmitted(data);
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

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Submitted Purchase Orders</h4>
                            <h6>Manage Your Submitted Order</h6>
                        </div>
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
                                        <th>Order Id</th>
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
                                                <td>{item.POS_PurchaseOrderID || "N/A"}</td>
                                                <td>{item.OrderNumber || "N/A"}</td>
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
                                            <td colSpan="10" className="text-center">
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
            <PurchaseOrderSubmittedForm
                onSubmit={handleAddProduct}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
            />
        </div>
    );
};


export default PurchaseOrderSubmittedPage;
