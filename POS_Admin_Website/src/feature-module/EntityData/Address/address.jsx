import React, { useState, useEffect } from "react";
import { getAllAddress } from "../../../services/entityData/address";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";

const EntityDataAddress = () => {
    const [addressList, setAddressList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const recordsPerPage = 10; // ✅ how many rows per page

    useEffect(() => {
        fetchAddresses();
    }, []);

    const fetchAddresses = async () => {
        try {
            const data = await getAllAddress();
            setAddressList(data);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    };

    // ✅ filter data by search
    const filteredData = addressList.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    // ✅ pagination logic
    const totalPages = Math.ceil(filteredData.length / recordsPerPage);
    const startIndex = (currentPage - 1) * recordsPerPage;
    const currentData = filteredData.slice(startIndex, startIndex + recordsPerPage);

    const goToPage = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
        }
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Address List</h4>
                            <h6>Manage Your Addresses</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added">
                            <PlusCircle className="me-2" />
                            Add New Address
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
                                        onChange={(e) => {
                                            setSearchTerm(e.target.value);
                                            setCurrentPage(1); // reset to page 1 on search
                                        }}
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
                                        <th>Street Address</th>
                                        <th>Locality</th>
                                        <th>Postal Code</th>
                                        <th>Landmark</th>
                                        <th>Latitude</th>
                                        <th>Longitude</th>
                                        <th>Notes</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {currentData.length > 0 ? (
                                        currentData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.StreetAddress}</td>
                                                <td>{item.Locality}</td>
                                                <td>{item.PostalCode}</td>
                                                <td>{item.Landmark}</td>
                                                <td>{item.Latitude}</td>
                                                <td>{item.Longitude}</td>
                                                <td>{item.Notes}</td>
                                                <td>
                                                    <button className="btn btn-sm btn-primary me-2">
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
            </div>
        </div>
    );
};

export default EntityDataAddress;
