import React, { useState, useEffect } from "react";
import { getAllDebtors, getAllBranches, getAllDepartments, getAllDebtorTypes, newDebtor, updateDebtor } from "../../services/debtors/debtors";
import { getAllStatus } from "../../services/entityData/status";
import { getAllCurrency } from "../../services/entityData/currency";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import DebtorForm from "../../core/modals/debtors/debtorFormModel";
import DebtorAddress from "./debtorAddress";
import DebtorContact from "./debtorContact";



const Debtors = () => {
    const [listData, setListData] = useState([]);
    const [branchList, setBranchList] = useState([]);
    const [departmentList, setDepartmentList] = useState([]);
    const [debtorTypeList, setDebtorType] = useState([]);
    const [statusList, setStatusList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [showAddressModel, setAddressModelShow] = useState(false);
    const [showContactModel, setContactModelShow] = useState(false);
    const [selectedDebtor, setSelectedDebtor] = useState(null);
    const [selectedDebtorId, setSelectedDebtorId] = useState(null);
   
        const [currencyList, setCurrencyList] = useState([]);

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const data = await getAllDebtors();
            setListData(data);
            const currencyList = await getAllCurrency();
                        setCurrencyList(currencyList);
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
    const handleAddDebtor = async (data) => {
        try {
            if (data.DebtorID) {
                await updateDebtor(data);
            }
            else {
                await newDebtor(data);
            }
            await fetchRecords();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditDebtor = (record) => {
        setSelectedDebtor(record);
        setModelShow(true);
    };

    const handleAddAddress = (debtorId) => {
        setSelectedDebtorId(debtorId);
        setAddressModelShow(true);
    };

    const handleViewContacts = (debtorId) => {
        setSelectedDebtorId(debtorId);
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
                            <h4>Locations</h4>
                            <h6>Manage Your Location</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Location
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
                                        <th>Is Active</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ShortCode || "N/A"}</td>
                                                <td>{item.Name || "N/A"}</td>
                                                <td>{item.IsActive ? 'Yes' : 'No'}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditDebtor(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                    <button
                                                        type='button'
                                                        onClick={() => handleAddAddress(item.DebtorID)}
                                                        className="btn btn-sm btn-info me-2"
                                                        title="Add Address"
                                                    >
                                                        <i className="feather-map-pin"></i>
                                                    </button>
                                                    <button
                                                        type="button"
                                                        onClick={() => handleViewContacts(item.DebtorID)}
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
            {showModel &&
                <DebtorForm 
                currencyList={currencyList}
                branchList={branchList}
                    onSubmitDebtor={handleAddDebtor}
                    showModel={showModel}
                    handleClose={handleClose}
                    debtorData={selectedDebtor}
                    debtorTypeList={debtorTypeList}
                    departmentList={departmentList}
                    statusList={statusList}
                    debtorList={listData}
                />
            }
            {showAddressModel &&
                <DebtorAddress
                    showAddressModel={showAddressModel}
                    debtorId={selectedDebtorId}
                    handleAddressClose={handleAddressClose}
                />}

            {showContactModel &&
                <DebtorContact
                    showContactModel={showContactModel}
                    debtorId={selectedDebtorId}
                    handleContactClose={handleContactClose}
                />}
        </div>
    );
};


export default Debtors;
