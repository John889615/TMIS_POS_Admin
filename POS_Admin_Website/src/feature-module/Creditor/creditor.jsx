import React, { useState, useEffect } from "react";
import { getAllCreditor, getAllCreditorTypes, newCreditor } from "../../services/creditor/creditor";
import { getAllStatus } from "../../services/entityData/status";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import CreditorForm from "../../core/modals/creditor/creditorFormModel";

import CreditorAddress from "./creditorAddress";
import CreditorContact from "./creditorContact";


const Creditor = () => {
    const [listData, setListData] = useState([]);
    const [CreditorTypeList, setCreditorTypeList] = useState([]);
    const [statusList, setStatusList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const [showAddressModel, setAddressModelShow] = useState(false);
    const [showContactModel, setContactModelShow] = useState(false);
    const [selectedCreditorId, setSelectedCreditorId] = useState(null);
    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const data = await getAllCreditor();
            setListData(data);
            const type = await getAllCreditorTypes();
            setCreditorTypeList(type);
            const status = await getAllStatus();
            setStatusList(status);
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

    const handleShow = () => setModelShow(true);
    const handleClose = () => setModelShow(false);
    const handleAddCreditor = async (data) => {
        try {
            if (data.POS_CostCenterID) {
                await updateCostCenter(data);
            }
            else {
                await newCreditor(data);
            }
            await newCreditor(data);
            await fetchRecords();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditCreditor = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };

    const handleAddAddress = (creditorId) => {
        setSelectedCreditorId(creditorId);
        setAddressModelShow(true);
    };

    const handleViewContacts = (creditorId) => {
        setSelectedCreditorId(creditorId);
        setContactModelShow(true);
    };

    const handleAddressClose = () => setAddressModelShow(false);
    const handleContactClose = () => setContactModelShow(false);

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Creditors</h4>
                            <h6>Manage Your Creditor</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Creditor
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
                                        <th>Short Code</th>
                                        <th>Name</th>
                                        <th>Master Creditor</th>
                                        <th>Is Master Creditor</th>
                                        <th>Type</th>
                                        <th>Status</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ShortCode || "N/A"}</td>
                                                <td>{item.Name || "N/A"}</td>
                                                <td>{item.MasterCreditor || "N/A"}</td>
                                                <td>{item.IsMasterCreditor ? "Yes" : "No"}</td>
                                                <td>{item.CreditorType || "N/A"}</td>
                                                <td>{item.Status || "N/A"}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditCreditor(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                    <button
                                                        type='button'
                                                        onClick={() => handleAddAddress(item.CreditorID)}
                                                        className="btn btn-sm btn-info me-2"
                                                        title="Add Address"
                                                    >
                                                        <i className="feather-map-pin"></i>
                                                    </button>
                                                    <button
                                                        type="button"
                                                        onClick={() => handleViewContacts(item.CreditorID)}
                                                        className="btn btn-sm btn-warning"
                                                        title="View Contacts"
                                                    >
                                                        <i className="feather-users"></i>
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
            {showModel && (
                <CreditorForm creditorTypeList={CreditorTypeList}
                    onSubmitCreditor={handleAddCreditor}
                    showModel={showModel}
                    handleClose={handleClose}
                    data={selectedData}
                    statusList={statusList}
                    creditorList={listData}
                />
            )}

            {showAddressModel &&
                <CreditorAddress
                    showAddressModel={showAddressModel}
                    creditorId={selectedCreditorId}
                    handleAddressClose={handleAddressClose}
                />}

            {showContactModel &&
                <CreditorContact
                    showContactModel={showContactModel}
                    creditorId={selectedCreditorId}
                    handleContactClose={handleContactClose}
                />}
        </div>
    );
};


export default Creditor;
