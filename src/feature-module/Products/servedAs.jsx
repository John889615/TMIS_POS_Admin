import React, { useEffect, useMemo, useState } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";

import {
    getAllServedAs,
    newServedAs,
    updateServedAs,
} from "../../services/product/servedAs";

import ServedAsForm from "../../core/modals/products/servedAsFormModel";

const ServedAs = () => {
    const [listData, setListData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        setLoading(true);
        try {
            const data = await getAllServedAs();
            setListData(Array.isArray(data) ? data : []);
        } catch (err) {
            console.error("Failed to load Served As records:", err?.message || err);
            setListData([]);
        } finally {
            setLoading(false);
        }
    };

    const filteredData = useMemo(() => {
        const search = searchTerm.trim().toLowerCase();

        if (!search) return listData;

        return listData.filter((item) => {
            const servedAsType = String(item?.ServedAsType || "").toLowerCase();
            const name = String(item?.Name || "").toLowerCase();

            return servedAsType.includes(search) || name.includes(search);
        });
    }, [listData, searchTerm]);

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true);
    };

    const handleClose = () => {
        setModelShow(false);
        setSelectedData(null);
    };

    const handleAddOrUpdateServedAs = async (formData) => {
        try {
            const payload = {
                ServedAsID: Number(formData?.ServedAsID) || 0,
                ServedAsType: formData?.ServedAsType || "",
                Name: formData?.Name || "",
            };

            let response;

            if (payload.ServedAsID > 0) {
                response = await updateServedAs(payload);
            } else {
                response = await newServedAs(payload);
            }

            if (response?.Success === false) {
                console.error("Save failed:", response?.Errors || response?.Messages);
                return;
            }

            await fetchRecords();
            handleClose();
        } catch (err) {
            console.error("Error saving Served As record:", err?.message || err);
        }
    };

    const handleEditServedAs = (record) => {
        setSelectedData({
            ServedAsID: record?.ServedAsID || 0,
            ServedAsType: record?.ServedAsType || "",
            Name: record?.Name || "",
        });

        setModelShow(true);
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Served As</h4>
                            <h6>Manage served as options</h6>
                        </div>
                    </div>

                    <div className="page-btn">
                        <Button
                            variant="none"
                            className="btn btn-added"
                            onClick={handleShow}
                        >
                            <PlusCircle className="me-2" />
                            Add Served As
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
                                        placeholder="Search served as"
                                        className="form-control"
                                        value={searchTerm}
                                        onChange={(e) => setSearchTerm(e.target.value)}
                                    />
                                    <Link to="#" className="btn btn-searchset">
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
                                        <th style={{ width: "140px" }}>ID</th>
                                        <th style={{ width: "180px" }}>Type</th>
                                        <th>Name</th>
                                        <th style={{ width: "120px" }}>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {loading ? (
                                        <tr>
                                            <td colSpan="4" className="text-center">
                                                Loading...
                                            </td>
                                        </tr>
                                    ) : filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={item?.ServedAsID || index}>
                                                <td>{item?.ServedAsID || "N/A"}</td>
                                                <td>{item?.ServedAsType || "N/A"}</td>
                                                <td>{item?.Name || "N/A"}</td>
                                                <td>
                                                    <button
                                                        type="button"
                                                        onClick={() => handleEditServedAs(item)}
                                                        className="btn btn-sm btn-primary"
                                                    >
                                                        <i className="feather-edit" />
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="4" className="text-center">
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
                <ServedAsForm
                    onSubmit={handleAddOrUpdateServedAs}
                    showModel={showModel}
                    handleClose={handleClose}
                    data={selectedData}
                />
            )}
        </div>
    );
};

export default ServedAs;