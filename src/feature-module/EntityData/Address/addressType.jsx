import React, { useState, useEffect } from "react";
import {
    getAllAddressType,
    newAddressType,
    updateAddressType,
} from "../../../services/entityData/addressType";
import { getAllEntities } from "../../../services/entityData/entities";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";

import AddressTypeForm from "../../../core/modals/entityData/address/addressTypeFormModel";

const EntityDataAddressType = () => {
    const [addressList, setAddressList] = useState([]);
    const [entityList, setEntityList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModal, setShowModal] = useState(false);
    const [selectedAddress, setSelectedAddress] = useState(null);

    // Pagination states
    const [currentPage, setCurrentPage] = useState(1);
    const recordsPerPage = 10;

    useEffect(() => {
        fetchEntities();
        fetchAddresses();
    }, []);

    const fetchAddresses = async () => {
        try {
            const data = await getAllAddressType();
            setAddressList(data);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    };

    const fetchEntities = async () => {
        try {
            const data = await getAllEntities();
            setEntityList(data);
        } catch (err) {
            console.error("Failed to load entities:", err.message);
        }
    };

    const filteredData = addressList.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    // Pagination logic
    const indexOfLastRecord = currentPage * recordsPerPage;
    const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
    const currentRecords = filteredData.slice(indexOfFirstRecord, indexOfLastRecord);
    const totalPages = Math.ceil(filteredData.length / recordsPerPage);
    const goToPage = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
        }
    };
    const handleShow = () => {
        setSelectedAddress(null); // reset for new
        setShowModal(true);
    };
    const handleClose = () => setShowModal(false);

    const handleAddAddressType = async (data) => {
        try {
            if (data.AddressTypeID) {
                await updateAddressType(data);
            } else {
                await newAddressType(data);
            }
            await fetchAddresses();
            setShowModal(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditAddressType = (record) => {
        setSelectedAddress(record);
        setShowModal(true);
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Address Types List</h4>
                            <h6>Manage Your Address Type</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button
                            variant="none"
                            className="btn btn-added"
                            onClick={handleShow}
                        >
                            <PlusCircle className="me-2" />
                            Add New Address Type
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
                                        <i
                                            data-feather="search"
                                            className="feather-search"
                                        />
                                    </Link>
                                </div>
                            </div>
                        </div>
                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Type</th>
                                        <th>Is Required</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {currentRecords.length > 0 ? (
                                        currentRecords.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.Type}</td>
                                                <td>{item.IsRequired ? "Yes" : "No"}</td>
                                                <td>
                                                    <button
                                                        className="btn btn-sm btn-primary me-2"
                                                        onClick={() =>
                                                            handleEditAddressType(item)
                                                        }
                                                    >
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="3" className="text-center">
                                                No records found
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                            {/* Pagination Controls */}
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

                {showModal && (
                    <AddressTypeForm
                        showModel={showModal}
                        handleClose={handleClose}
                        onSubmit={handleAddAddressType}
                        data={selectedAddress}
                        entitiesList={entityList}
                    />
                )}
            </div>
        </div>
    );
};

export default EntityDataAddressType;
