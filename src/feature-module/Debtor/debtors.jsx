import React, { useState, useEffect } from "react";
import {
  getAllDebtors,
  getAllBranches,
  getAllDepartments,
  getAllDebtorTypes,
  newDebtor,
  updateDebtor,
} from "../../services/debtors/debtors";
import { getAllStatus } from "../../services/entityData/status";
import { getAllCurrency } from "../../services/entityData/currency";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";
import DebtorForm from "../../core/modals/debtors/debtorFormModel";
import DebtorAddress from "./debtorAddress";
import DebtorContact from "./debtorContact";
import DebtorCurrencyModal from "../../core/modals/debtors/debtorCurrencyModal";

const Debtors = () => {
  const [listData, setListData] = useState([]);
  const [branchList, setBranchList] = useState([]);
  const [departmentList, setDepartmentList] = useState([]);
  const [debtorTypeList, setDebtorType] = useState([]);
  const [statusList, setStatusList] = useState([]);
  const [currencyList, setCurrencyList] = useState([]);

  const [searchTerm, setSearchTerm] = useState("");
  const [showModel, setModelShow] = useState(false);
  const [showAddressModel, setAddressModelShow] = useState(false);
  const [showContactModel, setContactModelShow] = useState(false);
  const [showCurrencyModel, setCurrencyModelShow] = useState(false);

  const [selectedDebtor, setSelectedDebtor] = useState(null);
  const [selectedDebtorId, setSelectedDebtorId] = useState(null);

  const [currentPage, setCurrentPage] = useState(1);
  const recordsPerPage = 10;
  const maxPageButtons = 5;

  useEffect(() => {
    fetchRecords();
  }, []);

  const fetchRecords = async () => {
    try {
      const data = await getAllDebtors();
      setListData(data || []);

      const currency = await getAllCurrency();
      setCurrencyList(currency || []);

      // const branches = await getAllBranches(); setBranchList(branches || []);
      // const departments = await getAllDepartments(); setDepartmentList(departments || []);
      // const debtorTypes = await getAllDebtorTypes(); setDebtorType(debtorTypes || []);
      // const statuses = await getAllStatus(); setStatusList(statuses || []);
    } catch (err) {
      console.error("Failed to load debtors:", err.message);
    }
  };

  const filteredData = (listData || []).filter((item) =>
    Object.values(item).some(
      (value) =>
        typeof value === "string" &&
        value.toLowerCase().includes(searchTerm.toLowerCase())
    )
  );

  const totalPages = Math.ceil(filteredData.length / recordsPerPage);

  useEffect(() => {
    if (totalPages > 0 && currentPage > totalPages) setCurrentPage(totalPages);
    if (totalPages === 0) setCurrentPage(1);
  }, [totalPages]); // eslint-disable-line react-hooks/exhaustive-deps

  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentData = filteredData.slice(indexOfFirstRecord, indexOfLastRecord);

  const goToPage = (page) => {
    if (page >= 1 && page <= totalPages) {
      setCurrentPage(page);
    }
  };

  let startPage = Math.max(1, currentPage - Math.floor(maxPageButtons / 2));
  let endPage = Math.min(totalPages, startPage + maxPageButtons - 1);
  if (endPage - startPage + 1 < maxPageButtons) {
    startPage = Math.max(1, endPage - maxPageButtons + 1);
  }

  const handleShow = () => {
    setSelectedDebtor(null);
    setModelShow(true);
  };

  const handleClose = () => setModelShow(false);

  const handleAddDebtor = async (data) => {
    try {
      if (data.DebtorID) {
        await updateDebtor(data);
      } else {
        await newDebtor(data);
      }

      await fetchRecords();
      setModelShow(false);
    } catch (err) {
      console.error("Error saving debtor:", err.message);
    }
  };

  const handleEditDebtor = (record) => {
    setSelectedDebtor(record);
    setModelShow(true);
  };

  const handleAddAddress = (debtorId) => {
    setSelectedDebtorId(debtorId);
    setAddressModelShow(true);
  };

  const handleViewContacts = (debtorId) => {
    setSelectedDebtorId(debtorId);
    setContactModelShow(true);
  };

  const handleManageCurrencies = (debtorId) => {
    setSelectedDebtorId(debtorId);
    setCurrencyModelShow(true);
  };

  const handleAddressClose = () => setAddressModelShow(false);
  const handleContactClose = () => setContactModelShow(false);
  const handleCurrencyClose = () => setCurrencyModelShow(false);

  return (
    <div className="page-wrapper">
      <div className="content">
        <div className="page-header">
          <div className="add-item d-flex">
            <div className="page-title">
              <h4>Debtors</h4>
              <h6>Manage Your Debtors</h6>
            </div>
          </div>
          <div className="page-btn">
            <Button variant="none" className="btn btn-added" onClick={handleShow}>
              <PlusCircle className="me-2" />
              Add New Debtor
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
                      setCurrentPage(1);
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
                    <th>Short Code</th>
                    <th>Name</th>
                    <th>Is Active</th>
                    <th>Action</th>
                  </tr>
                </thead>
                <tbody>
                  {currentData.length > 0 ? (
                    currentData.map((item, index) => (
                      <tr key={index}>
                        <td>{item.ShortCode || "N/A"}</td>
                        <td>{item.Name || "N/A"}</td>
                        <td>{item.IsActive ? "Yes" : "No"}</td>
                        <td>
                          <button
                            type="button"
                            onClick={() => handleEditDebtor(item)}
                            className="btn btn-sm btn-primary me-2"
                            title="Edit Debtor"
                          >
                            <i className="feather-edit"></i>
                          </button>

                          <button
                            type="button"
                            onClick={() => handleAddAddress(item.DebtorID)}
                            className="btn btn-sm btn-info me-2"
                            title="Add Address"
                          >
                            <i className="feather-map-pin"></i>
                          </button>

                          <button
                            type="button"
                            onClick={() => handleViewContacts(item.DebtorID)}
                            className="btn btn-sm btn-warning me-2"
                            title="View Contacts"
                          >
                            <i className="feather-users"></i>
                          </button>

                          <button
                            type="button"
                            onClick={() => handleManageCurrencies(item.DebtorID)}
                            className="btn btn-sm btn-secondary"
                            title="Manage Currencies"
                          >
                            <i className="feather-dollar-sign"></i>
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

              {totalPages > 1 && (
                <div className="d-flex justify-content-between align-items-center mt-3">
                  <span>
                    Page {currentPage} of {totalPages}
                  </span>

                  <div>
                    {Array.from(
                      { length: endPage - startPage + 1 },
                      (_, i) => startPage + i
                    ).map((page) => (
                      <Button
                        key={page}
                        variant={currentPage === page ? "primary" : "light"}
                        size="sm"
                        className="mx-1"
                        onClick={() => goToPage(page)}
                      >
                        {page}
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

      {showModel && (
        <DebtorForm
          currencyList={currencyList}
          branchList={branchList}
          onSubmitDebtor={handleAddDebtor}
          showModel={showModel}
          handleClose={handleClose}
          debtorData={selectedDebtor}
          debtorTypeList={debtorTypeList}
          departmentList={departmentList}
          statusList={statusList}
          debtorList={listData}
        />
      )}

      {showAddressModel && (
        <DebtorAddress
          showAddressModel={showAddressModel}
          debtorId={selectedDebtorId}
          handleAddressClose={handleAddressClose}
        />
      )}

      {showContactModel && (
        <DebtorContact
          showContactModel={showContactModel}
          debtorId={selectedDebtorId}
          handleContactClose={handleContactClose}
        />
      )}

      {showCurrencyModel && (
        <DebtorCurrencyModal
          showCurrencyModel={showCurrencyModel}
          debtorId={selectedDebtorId}
          handleCurrencyClose={handleCurrencyClose}
        />
      )}
    </div>
  );
};

export default Debtors;