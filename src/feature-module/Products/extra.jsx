import React, { useState, useEffect } from "react";
import { getAllProducts } from "../../services/product/product";
import { getAllExtraById, newExtra, updateExtra } from "../../services/product/extra";
import { getAllExtraCategory } from "../../services/product/extraCategory";

import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import ExtraForm from "../../core/modals/products/extraFormModel";
import { useParams } from "react-router-dom";



const ExtraPage = () => {
    const { id } = useParams();
    const [listData, setListData] = useState([]);
    const [productListData, setProductListData] = useState([]);
    const [categoryListData, setCategoryListData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            if (id) {
                const data = await getAllExtraById(id);
                setListData(data);
            }
            const data = await getAllProducts();
            setProductListData(data);
            const categorydata = await getAllExtraCategory();
            setCategoryListData(categorydata);
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

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);
    const handleAddExtra = async (data) => {
        try {
            if (data.ProductExtraID) {
                await updateExtra(data);
            }
            else {
                await newExtra(data);
            }
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
                            <h4>Extra</h4>
                            <h6>Manage Your Extra</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Extra
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
                                        <th>Product Name</th>
                                        <th>Category Name</th>
                                        <th>Extra Name</th>
                                        <th>Is Quantified</th>
                                        <th>Quantity</th>
                                        <th>Is ExtraCharge</th>
                                        <th>Display Order</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ProductName || "N/A"}</td>
                                                <td>{item.CategoryName || "N/A"}</td>
                                                <td>{item.ExtraName || "N/A"}</td>
                                                <td>{item.IsQuantified ? "Yes" : "No"}</td>
                                                <td>{item.Quantity}</td>
                                                <td>{item.IsExtraCharge ? "Yes" : "No"}</td>
                                                <td>{item.DisplayOrder || "N/A"}</td>
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
            {showModel &&
                <ExtraForm
                    onSubmit={handleAddExtra}
                    showModel={showModel}
                    handleClose={handleClose}
                    data={selectedData}
                    productList={productListData}
                    extraCategoryList={categoryListData}
                    id={id}
                />
            }
        </div>
    );
};


export default ExtraPage;
