import React, { useState, useEffect } from "react";
import { getAllProductPriceById, newProductPrice, updateProductPrice } from "../../services/stock/debtorProductPrice";
import { getAllPriceCode } from "../../services/stock/priceCode";
import { getAllTaxType } from "../../services/syncList/syncService";
import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { PlusCircle } from "react-feather";
import DebtorProductForm from "../../core/modals/stocks/debtorProductPriceModel";

import { useParams } from "react-router-dom";



const DebtorProductPagePage = () => {
    const { id } = useParams();
    const [listData, setListData] = useState([]);
    const [priceCodeListData, setPriceCodeListData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [taxList, setTaxList] = useState([]);
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            if (id) {
                const data = await getAllProductPriceById(id);
                setListData(data);
            }
            const data = await getAllPriceCode();
            setPriceCodeListData(data);
            const tax = await getAllTaxType();
            setTaxList(tax);
        } catch (err) {
            console.error("Failed to load:", err.message);
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
    const handleAddProduct = async (data) => {
        try {
            if (data.POS_DebtorProductPriceID) {
                await updateProductPrice(data);
            }
            else {
                await newProductPrice(data);
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
                            <h4>Product Prices</h4>
                            <h6>Manage Your Price</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Price
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
                                        <th>Product Item Name</th>
                                        <th>Is Quantified</th>
                                        <th>Quantity</th>
                                        <th>Is Optional</th>
                                        <th>Is ExtraCharge</th>
                                        <th>Display Order</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ProductItemName || "N/A"}</td>
                                                <td>{item.IsQuantified ? "Yes" : "No"}</td>
                                                <td>{item.Quantity}</td>
                                                <td>{item.IsOptional ? "Yes" : "No"}</td>
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
                                            <td colSpan="7" className="text-center">
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
                <DebtorProductForm
                    onSubmit={handleAddProduct}
                    showModel={showModel}
                    handleClose={handleClose}
                    data={selectedData}
                    priceCodeList={priceCodeListData}
                    taxList={taxList}
                    id={id}
                />
            }
        </div>
    );
};


export default DebtorProductPagePage;
