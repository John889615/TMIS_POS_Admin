import React, { useState, useEffect, useMemo } from "react";
import { getAllStockRequest } from "../../services/stock/stockRequestService";
import { getAllStockRequestLine } from "../../services/stock/stockRequestLineService";


import { Link } from "react-router-dom";
import Select from "react-select";
import { useSelector } from 'react-redux';



const StockRequestLinePage = () => {
    const [listData, setListData] = useState([]);
    const [stockRequestList, setStockRequestList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const debtorId = useSelector((state) => state.selectedDebtorStore);

    useEffect(() => {
        fetchStockRequest();
    }, [debtorId]);

    const fetchStockRequest = async () => {
        try {
            const data = await getAllStockRequest(debtorId == null ? 1 : debtorId);
            setStockRequestList(data);
        } catch (err) {
            console.error("Failed to load stock requests:", err.message);
        }
    }

    const stockRequestOptions = useMemo(
        () => (stockRequestList || []).map(item => ({
            value: item.POS_StockRequestID,
            label: item.RefNumber,
        })),
        [stockRequestList]
    );

    const handleStockRequestChange = async (option) => {
        if (!option) {
            setListData([]);
            return;
        }
        const data = await getAllStockRequestLine(option.value);
        setListData(data);
    };

    const filteredData = listData.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Stock Request Line</h4>
                            <h6>View Stock Request Lines</h6>
                        </div>
                    </div>
                </div>
                <div className="card table-list-card">
                    <div className="card-body">
                        <div className="table-top d-flex justify-content-between align-items-center">
                            {/* Search Input - Left Side */}
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

                            {/* Select Dropdown - Right Side */}
                            <div className="form-group mb-0" style={{ minWidth: 260 }}>
                                <Select
                                    classNamePrefix="react-select"
                                    options={stockRequestOptions}
                                    onChange={handleStockRequestChange}
                                    placeholder="Filter by Stock Request"
                                    isClearable
                                    isSearchable
                                    noOptionsMessage={() => "No requests found"}
                                />
                            </div>
                        </div>

                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Product Name</th>
                                        <th>Quantity</th>
                                        <th>Approved Quantity</th>
                                        <th>Notes</th>
                                        <th>IsDeclined</th>
                                        <th>Manager Notes</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ProductName || "N/A"}</td>
                                                <td>{item.Quantity ?? "N/A"}</td>
                                                <td>{item.ApprovedQuantity ?? "N/A"}</td>
                                                <td>{item.Notes || "N/A"}</td>
                                                <td>{item.IsDeclined ? "Yes" : "No"}</td>
                                                <td>{item.ManagerNotes || "N/A"}</td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="6" className="text-center">
                                                No records found - Please select Stock Request
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};


export default StockRequestLinePage;
