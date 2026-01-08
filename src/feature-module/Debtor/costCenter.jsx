import React, { useState, useEffect } from "react";
import { getAllCostCenter, getAllCostCenterTypes, newCostCenter, updateCostCenter } from "../../services/debtors/costCenter";
import { getAllDebtors } from "../../services/debtors/debtors";
import { getAllStatus } from "../../services/entityData/status";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
} from "react-feather";
import CostCenterForm from "../../core/modals/debtors/costCenterFormModel";



const CostCenter = () => {
    const [listData, setListData] = useState([]);
    const [costTypeList, setCostTypeList] = useState([]);
    const [statusList, setStatusList] = useState([]);
    const [debtorList, setDebtorList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const data = await getAllCostCenter();
            setListData(data);
            const type = await getAllCostCenterTypes();
            setCostTypeList(type);
            const status = await getAllStatus();
            setStatusList(status);
            const debtor = await getAllDebtors();
            setDebtorList(debtor);
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

    const handleShow = () => setModelShow(true);
    const handleClose = () => setModelShow(false);
    const handleAddCostCenter = async (data) => {
        try {
            if (data.POS_CostCenterID) {
                await updateCostCenter(data);
            }
            else {
                await newCostCenter(data);
            }
            await fetchRecords();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditCostCenter = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Cost Centers</h4>
                            <h6>Manage Your Cost Center</h6>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Cost Center
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
                                        <th>Debtor</th>
                                        <th>Status</th>
                                        <th>Type</th>
                                        <th>Billing Reference</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.Name || "N/A"}</td>
                                                <td>{item.Debtor || "N/A"}</td>
                                                <td>{item.Status || "N/A"}</td>
                                                <td>{item.Type ? "Yes" : "No"}</td>
                                                <td>{item.BillingReference || "N/A"}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditCostCenter(item)}
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
            <CostCenterForm costTypeList={costTypeList}
                debtorList={debtorList}
                onSubmitCostCenter={handleAddCostCenter}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
                statusList={statusList}
            />
        </div>
    );
};


export default CostCenter;
