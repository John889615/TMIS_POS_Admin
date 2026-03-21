import React, { useEffect, useState } from "react";
import Swal from "sweetalert2";
import { Button } from "react-bootstrap";
import { Link, useParams } from "react-router-dom";
import { PlusCircle, Trash2 } from "react-feather";

import { getAllServedAs } from "../../services/product/servedAs";
import {
    getAllServedAsProductsById,
    newServedAsProduct,
    updateServedAsProduct,
    removeServedAsProduct,
} from "../../services/product/servedAs";

import ServedAsProductForm from "../../core/modals/products/servedAsProductFormModel";

const ServedAsProduct = () => {
    const { id } = useParams();
    const [listData, setListData] = useState([]);
    const [servedAsListData, setServedAsListData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);

    useEffect(() => {
        fetchRecords();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [id]);

    const fetchRecords = async () => {
        try {
            const [servedAsProducts, servedAsList] = await Promise.all([
                id ? getAllServedAsProductsById(id) : Promise.resolve([]),
                getAllServedAs(),
            ]);

            setListData(Array.isArray(servedAsProducts) ? servedAsProducts : []);
            setServedAsListData(Array.isArray(servedAsList) ? servedAsList : []);
        } catch (err) {
            console.error("Failed to load served as products:", err?.message || err);
            setListData([]);
            setServedAsListData([]);
        }
    };

    const filteredData = (listData || []).filter((item) =>
        Object.values(item || {}).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    const getServedAsIdFromRow = (row) =>
        Number(row?.ServedAsID) ||
        Number(row?.FK_ServedAsID) ||
        Number(row?.ServedAs?.ServedAsID) ||
        0;

    const getServedAsName = (servedAsId) => {
        const match = (servedAsListData || []).find(
            (row) => Number(row?.ServedAsID) === Number(servedAsId)
        );

        return match?.Name || "N/A";
    };

    const getServedAsType = (servedAsId) => {
        const match = (servedAsListData || []).find(
            (row) => Number(row?.ServedAsID) === Number(servedAsId)
        );

        return match?.ServedAsType || "N/A";
    };

    const getServedAsProductIdFromRow = (row) =>
        Number(row?.ServedAsProductID) ||
        Number(row?.FK_ServedAsProductID) ||
        0;

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true);
    };

    const handleClose = () => {
        setModelShow(false);
        setSelectedData(null);
    };

    const showApiError = async (title, response, fallback) => {
        const msg =
            response?.Messages?.[0] ||
            response?.Errors?.[0] ||
            response?.Message ||
            fallback ||
            "Something went wrong.";

        await Swal.fire({
            icon: "error",
            title: title || "Error",
            text: msg,
            confirmButtonText: "OK",
            allowOutsideClick: false,
        });
    };

    const handleSave = async (data) => {
        try {
            let response;

            if (data?.ServedAsProductID) {
                response = await updateServedAsProduct(data);
            } else {
                response = await newServedAsProduct(data);
            }

            if (response?.Success === false) {
                await showApiError(
                    "Could not save served as",
                    response,
                    "Served as could not be saved."
                );
                return;
            }

            await fetchRecords();
            setModelShow(false);
            setSelectedData(null);
        } catch (err) {
            console.error("Error saving served as product:", err?.message || err);

            await Swal.fire({
                icon: "error",
                title: "Unexpected error",
                text: err?.message || "Something went wrong.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
            });
        }
    };

    const handleEdit = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };

    const handleDelete = async (row) => {
        const servedAsProductId = getServedAsProductIdFromRow(row);

        if (!servedAsProductId) {
            await Swal.fire({
                icon: "error",
                title: "Delete failed",
                text: "Missing ServedAsProductID.",
                confirmButtonText: "OK",
            });
            return;
        }

        const confirm = await Swal.fire({
            icon: "warning",
            title: "Delete this served as item?",
            text: "This action cannot be undone.",
            showCancelButton: true,
            confirmButtonText: "Yes, delete",
            cancelButtonText: "Cancel",
            allowOutsideClick: false,
        });

        if (!confirm.isConfirmed) return;

        try {
            const response = await removeServedAsProduct(servedAsProductId);

            if (response?.Success === false) {
                await showApiError("Delete failed", response, "Failed to delete served as item.");
                return;
            }

            await fetchRecords();
        } catch (err) {
            await Swal.fire({
                icon: "error",
                title: "Delete failed",
                text: err?.response?.data?.Message || err?.message || "Failed to delete served as item.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
            });
        }
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Product Served As</h4>
                            <h6>Manage served as for this product</h6>
                        </div>
                    </div>

                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
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
                        </div>

                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Served As</th>
                                        <th>Type</th>
                                        <th>Is Quantified</th>
                                        <th>Quantity</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => {
                                            const servedAsId = getServedAsIdFromRow(item);

                                            return (
                                                <tr key={getServedAsProductIdFromRow(item) || index}>
                                                    <td>{getServedAsName(servedAsId)}</td>
                                                    <td>{getServedAsType(servedAsId)}</td>
                                                    <td>{item?.IsQuantified ? "Yes" : "No"}</td>
                                                    <td>{item?.Quantity ?? 0}</td>
                                                    <td className="text-nowrap">
                                                        <button
                                                            type="button"
                                                            onClick={() => handleEdit(item)}
                                                            className="btn btn-sm btn-primary me-2"
                                                            title="Edit"
                                                        >
                                                            <i className="feather-edit"></i>
                                                        </button>

                                                        <button
                                                            type="button"
                                                            onClick={() => handleDelete(item)}
                                                            className="btn btn-sm btn-light me-2"
                                                            title="Delete"
                                                            style={{ border: "1px solid #f1c0c0" }}
                                                        >
                                                            <Trash2 size={16} color="red" />
                                                        </button>
                                                    </td>
                                                </tr>
                                            );
                                        })
                                    ) : (
                                        <tr>
                                            <td colSpan="5" className="text-center">
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
                <ServedAsProductForm
                    onSubmit={handleSave}
                    showModel={showModel}
                    handleClose={handleClose}
                    data={selectedData}
                    servedAsList={servedAsListData}
                    id={id}
                />
            )}
        </div>
    );
};

export default ServedAsProduct;