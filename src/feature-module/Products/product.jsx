import React, { useState, useEffect } from "react";
import { getAllProducts, newProduct, updateProduct } from "../../services/product/product";
import { getAllProductCategory } from "../../services/product/productCategory";
import { getAllProductTypes } from "../../services/product/productType";
import { getAllUnits } from "../../services/product/units";
import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { PlusCircle } from "react-feather";
import ProductForm from "../../core/modals/products/productFormModel";


const ProductPage = () => {
    const [listData, setListData] = useState([]);
    const [CategoryListData, setCategoryListData] = useState([]);
    const [typeListData, setTypeListData] = useState([]);
    const [unitListData, setUnitListData] = useState([]);
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const [searchTerm, setSearchTerm] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const recordsPerPage = 10;
    const navigate = useNavigate();

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const data = await getAllProducts();
            setListData(data);
            const type = await getAllProductCategory();
            setCategoryListData(type);
            const category = await getAllProductTypes();
            setTypeListData(category);
            const unit = await getAllUnits();
            setUnitListData(unit);
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

    const totalPages = Math.ceil(filteredData.length / recordsPerPage);
    const startIndex = (currentPage - 1) * recordsPerPage;
    const currentData = filteredData.slice(startIndex, startIndex + recordsPerPage);

    const goToPage = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
        }
    };

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true)
    };

    const handleClose = () => setModelShow(false);
    const handleAddProduct = async (data) => {
        console.log("Data : ", data);
        debugger;
        try {
            if (data.POS_ProductID) {
                await updateProduct(data);
            }
            else {
               var response =  await newProduct(data);
               debugger;
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

    const handleRedirect = (e, item) => {
        const value = e.target.value;
        if (!value) return;
        navigate(`/${value}/${item.POS_ProductID}`);
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Products</h4>
                            <h6>Manage Your Product</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Product
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
                                        <th>Description</th>
                                        <th>Product Type</th>
                                        <th>Unit</th>
                                        <th>Default Unit</th>
                                        <th>Category</th>
                                        <th>Is Stock Tracked</th>
                                        <th>SKU</th>
                                        <th>Barcode</th>
                                        <th>QrCode</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {currentData.length > 0 ? (
                                        currentData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ProductName || "N/A"}</td>
                                                <td>{item.Description || "N/A"}</td>
                                                <td>{item.ProductType || "N/A"}</td>
                                                <td>{item.Unit || "N/A"}</td>
                                                <td>{item.DefaultUnit || "N/A"}</td>
                                                <td>{item.ProductCategory || "N/A"}</td>
                                                <td>{item.IsStockTracked ? "Yes" : "No"}</td>
                                                <td>{item.SKU || "N/A"}</td>
                                                <td>{item.Barcode || "N/A"}</td>
                                                <td>{item.QrCode || "N/A"}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditProduct(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                    <select
                                                        className="form-select form-select-sm d-inline-block"
                                                        style={{ width: "140px" }}
                                                        onChange={(e) => handleRedirect(e, item)}
                                                        defaultValue=""
                                                    >
                                                        <option value="" disabled>
                                                            Select Action
                                                        </option>
                                                        <option value="product-combination">Combination</option>
                                                        <option value="product-extra">Extra</option>
                                                        <option value="product-preparation">Preparation</option>
                                                        <option value="product-substitution">Substitution</option>
                                                    </select>
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
            {showModel &&
                <ProductForm
                    onSubmit={handleAddProduct}
                    showModel={showModel}
                    handleClose={handleClose}
                    data={selectedData}
                    categoryList={CategoryListData}
                    typeList={typeListData}
                    unitList={unitListData}
                />
            }
        </div>
    );
};


export default ProductPage;
