import React, { useState, useEffect } from "react";
import { getAllContact } from "../../../services/entityData/contact";
import { Button, Pagination } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";

const EntityDataContact = () => {
    const [addressList, setAddressList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 10; // 👈 adjust per your needs

    useEffect(() => {
        fetchAddresses();
    }, []);

    const fetchAddresses = async () => {
        try {
            const data = await getAllContact();
            setAddressList(data);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    };

    // ✅ Filter Data
    const filteredData = addressList.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    // ✅ Pagination Logic
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = filteredData.slice(indexOfFirstItem, indexOfLastItem);
    const totalPages = Math.ceil(filteredData.length / itemsPerPage);

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header d-flex justify-content-between align-items-center">
                    <div className="page-title">
                        <h4>Contacts List</h4>
                        <h6>Manage Your Contact</h6>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added">
                            <PlusCircle className="me-2" />
                            Add New Contact
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
                                            setCurrentPage(1); // reset to first page on search
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
                                        <th>Value</th>
                                        <th>Is Verified</th>
                                        <th>Notes</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {currentItems.length > 0 ? (
                                        currentItems.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ContactValue}</td>
                                                <td>{item.IsVerified ? "Yes" : "No"}</td>
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
                        </div>

                        {/* ✅ Pagination Component */}
                        {totalPages > 1 && (
                            <Pagination className="justify-content-end mt-3">
                                {[...Array(totalPages)].map((_, index) => (
                                    <Pagination.Item
                                        key={index + 1}
                                        active={index + 1 === currentPage}
                                        onClick={() => setCurrentPage(index + 1)}
                                    >
                                        {index + 1}
                                    </Pagination.Item>
                                ))}
                            </Pagination>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default EntityDataContact;
