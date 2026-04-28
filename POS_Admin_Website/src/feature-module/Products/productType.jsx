import React, { useState, useEffect } from "react";
import { getAllProductTypes, newProductType, updateProductType } from "../../services/product/productType";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import ProductTypeForm from "../../core/modals/products/productTypeFormModel";



const ProductType = () => {
    const [listData, setListData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const data = await getAllProductTypes();
            setListData(data);
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
    const handleAddType = async (data) => {
        console.log("Data : ", data);
        try {
            if (data.POS_ProductTypeID) {
                await updateProductType(data);
            }
            else {
                await newProductType(data);
            }
            await fetchRecords();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditType = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Product Types</h4>
                            <h6>Manage Your Type</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Type
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
                                        <th>Is Inventory</th>
                                        <th>Is Manufactured</th>
                                        <th>Is Service</th>
                                        <th>Is Composite</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ProductType}</td>
                                                <td>{item.IsInventory ? "Yes" : "No"}</td>
                                                <td>{item.IsManufactured ? "Yes" : "No"}</td>
                                                <td>{item.IsService ? "Yes" : "No"}</td>
                                                <td>{item.IsComposite ? "Yes" : "No"}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditType(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="6" className="text-center">
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
            <ProductTypeForm
                onSubmit={handleAddType}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
            />
        </div>
    );
};


export default ProductType;
