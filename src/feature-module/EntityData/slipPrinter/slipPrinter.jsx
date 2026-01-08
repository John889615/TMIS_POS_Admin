import React, { useState, useEffect } from "react";
import { getAllSlipPrinter, newSlipPrinter, updateSlipPrinter } from "../../../services/entityData/slipPrinter";
import { getAllDebtors } from "../../../services/debtors/debtors";
import { getAllCostCenter } from "../../../services/debtors/costCenter";
import SlipPrinterForm from "../../../core/modals/entityData/slipPrinter/slipPrinterModel";

import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";

const EntityDataSlipPrinter = () => {
    const [slipPrinterList, setSlipPrinterList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModal, setShowModal] = useState(false);
    const [selectedPrinter, setSelectedPrinter] = useState(null);
    const [debtorList, setDebtorList] = useState([]);
    const [costCenterList, setCostCenterList] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const recordsPerPage = 10;
    const maxPageButtons = 5;

    useEffect(() => {
        fetchRecord();
    }, []);

    const fetchRecord = async () => {
        try {
            const data = await getAllSlipPrinter();
            setSlipPrinterList(data);
            const debtors = await getAllDebtors();
            setDebtorList(debtors);
            const costCenters = await getAllCostCenter();
            setCostCenterList(costCenters);
        } catch (err) {
            console.error("Failed to load slip printer:", err.message);
        }
    };

    const filteredData = slipPrinterList.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    const handleShow = () => {
        setSelectedPrinter(null);
        setShowModal(true);
    };
    const handleClose = () => setShowModal(false);
    const handleAddSlipPrinter = async (data) => {
        console.log("Slip Printer Data", data);
        debugger;
        try {
            if (data.SlipPrinterID) {
                await updateSlipPrinter(data);
            }
            else {
                await newSlipPrinter(data);
            }
            await fetchRecord();
            setShowModal(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditPrinter = (record) => {
        setSelectedPrinter(record);
        setShowModal(true);
    };


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
                            <h4>Slip Printers List</h4>
                            <h6>Manage Your Slip Printers</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Slip Printer
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
                                        <th>Debtor</th>
                                        <th>Cost Center</th>
                                        <th>Name</th>
                                        <th>Model</th>
                                        <th>IP Address</th>
                                        <th>Port</th>
                                        <th>Is Default</th>
                                        <th>Is Active</th>
                                        <th>Created By</th>
                                        <th>Updated By</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {currentRecords.length > 0 ? (
                                        currentRecords.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.Debtor}</td>
                                                <td>{item.CostCenter}</td>
                                                <td>{item.Name}</td>
                                                <td>{item.Model}</td>
                                                <td>{item.IpAddress}</td>
                                                <td>{item.Port}</td>
                                                <td>{item.IsDefault ? "Yes" : "No"}</td>
                                                <td>{item.IsActive ? "Yes" : "No"}</td>
                                                <td>{item.CreatedBy}</td>
                                                <td>{item.UpdatedBy}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditPrinter(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="11" className="text-center">
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
                    <SlipPrinterForm
                        onSubmit={handleAddSlipPrinter}
                        showModel={showModal}
                        handleClose={handleClose}
                        data={selectedPrinter}
                        debtorList={debtorList}
                        costCenterList={costCenterList}
                    />
                }
            </div>
        </div>
    );
};


export default EntityDataSlipPrinter;
