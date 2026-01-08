import React, { useState, useEffect } from "react";
import { getAllPaymentType, newPaymentType, updatePaymentType, getAllPaymentTypeIcon } from "../../../services/entityData/paymentType";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import PaymentTypeForm from "../../../core/modals/entityData/payment/paymentTypeModel";


const PaymentTypes = () => {
    const [showModal, setShowModal] = useState(false);
    const [selectedType, setSelectedType] = useState(null);
    const [ListData, setListData] = useState([]);
    const [iconListData, setIconListData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const recordsPerPage = 10;
    const maxPageButtons = 5;

    useEffect(() => {
        fetchRecord();
    }, []);

    const fetchRecord = async () => {
        try {
            const data = await getAllPaymentType();
            setListData(data);
            const dataIcon = await getAllPaymentTypeIcon();
            setIconListData(dataIcon);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    };

    const handleShow = () => {
        setSelectedType(null);
        setShowModal(true);
    };
    const handleClose = () => setShowModal(false);
    const handleAddType = async (data) => {
        try {
            if (data.PaymentTypeID) {
                await updatePaymentType(data);
            }
            else {
                await newPaymentType(data);
            }
            await fetchRecord();
            setShowModal(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditType = (record) => {
        setSelectedType(record);
        setShowModal(true);
    };

    const filteredData = ListData.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );


    const indexOfLastRecord = currentPage * recordsPerPage;
    const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
    const currentRecords = filteredData.slice(indexOfFirstRecord, indexOfLastRecord);
    const totalPages = Math.ceil(filteredData.length / recordsPerPage);
    const goToPage = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
        }
    };

    let startPage = Math.max(1, currentPage - Math.floor(maxPageButtons / 2));
    let endPage = Math.min(totalPages, startPage + maxPageButtons - 1);
    if (endPage - startPage + 1 < maxPageButtons) {
        startPage = Math.max(1, endPage - maxPageButtons + 1);
    }
    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Payment Types</h4>
                            <h6>Manage Your Type</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Payment Type
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
                                        <th>Name</th>
                                        <th>Is Active</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {currentRecords.length > 0 ? (
                                        currentRecords.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.Name}</td>
                                                <td>{item.IsActive ? "Yes" : "No"}</td>
                                                <td>
                                                    <button
                                                        onClick={() => handleEditType(item)}
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
                            {totalPages > 1 && (
                                <div className="d-flex justify-content-between align-items-center mt-3">
                                    <span>
                                        Page {currentPage} of {totalPages}
                                    </span>
                                    <div>
                                        {Array.from({ length: endPage - startPage + 1 }, (_, i) => {
                                            const pageNumber = startPage + i;
                                            return (
                                                <Button
                                                    key={pageNumber}
                                                    variant={currentPage === pageNumber ? "primary" : "light"}
                                                    size="sm"
                                                    className="mx-1"
                                                    onClick={() => setCurrentPage(pageNumber)}
                                                >
                                                    {pageNumber}
                                                </Button>
                                            );
                                        })}
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
                {showModal &&
                    <PaymentTypeForm
                        onSubmit={handleAddType}
                        showModel={showModal}
                        handleClose={handleClose}
                        data={selectedType}
                        iconList={iconListData}
                    />
                }
            </div>
        </div>
    );
};

export default PaymentTypes;
