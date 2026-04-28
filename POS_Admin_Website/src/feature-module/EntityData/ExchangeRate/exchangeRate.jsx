import React, { useEffect, useMemo, useState } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";
import Swal from "sweetalert2";

import ExchangeRateForm from "../../../core/modals/entityData/exchangeRate/exchangeRateModal";
import {
  listExchangeRates,
  newExchangeRate,
  updateExchangeRate,
} from "../../../services/entityData/exchangeRate";

const ExchangeRatePage = () => {
  const [showModal, setShowModal] = useState(false);
  const [selectedRecord, setSelectedRecord] = useState(null);
  const [listData, setListData] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [loading, setLoading] = useState(false);

  const [currentPage, setCurrentPage] = useState(1);
  const recordsPerPage = 10;
  const maxPageButtons = 5;

  useEffect(() => {
    fetchRecord();
  }, []);

  const fetchRecord = async () => {
    try {
      setLoading(true);

      const response = await listExchangeRates();

      if (response?.Success) {
        setListData(Array.isArray(response.Data) ? response.Data : []);
      } else {
        setListData([]);

        if (response?.Messages?.length) {
          console.warn(response.Messages[0]);
        }
      }
    } catch (err) {
      console.error("Failed to load exchange rates:", err);
      setListData([]);
      Swal.fire({
        icon: "error",
        title: "Error",
        text: "Failed to load exchange rates.",
      });
    } finally {
      setLoading(false);
    }
  };

  const handleShow = () => {
    setSelectedRecord(null);
    setShowModal(true);
  };

  const handleClose = () => {
    setShowModal(false);
    setSelectedRecord(null);
  };

  const handleSave = async (record) => {
    try {
      const response = record.ExchangeRateID
        ? await updateExchangeRate(record)
        : await newExchangeRate(record);

      if (response?.Success) {
        Swal.fire({
          icon: "success",
          title: "Success",
          text: response?.Messages?.[0] || "Saved successfully.",
        });

        await fetchRecord();
        handleClose();
      } else {
        Swal.fire({
          icon: "error",
          title: "Error",
          text:
            response?.Messages?.[0] ||
            response?.Errors?.[0] ||
            "Failed to save exchange rate.",
        });
      }
    } catch (err) {
      console.error("Save failed:", err);
      Swal.fire({
        icon: "error",
        title: "Error",
        text: "Failed to save exchange rate.",
      });
    }
  };

  const handleEdit = (record) => {
    setSelectedRecord(record);
    setShowModal(true);
  };

  const filteredData = useMemo(() => {
    if (!searchTerm.trim()) return listData;

    return listData.filter((item) =>
      Object.values(item).some((value) =>
        String(value ?? "")
          .toLowerCase()
          .includes(searchTerm.toLowerCase())
      )
    );
  }, [listData, searchTerm]);

  useEffect(() => {
    setCurrentPage(1);
  }, [searchTerm]);

  const totalPages = Math.ceil(filteredData.length / recordsPerPage);
  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentRecords = filteredData.slice(indexOfFirstRecord, indexOfLastRecord);

  let startPage = Math.max(1, currentPage - Math.floor(maxPageButtons / 2));
  let endPage = Math.min(totalPages, startPage + maxPageButtons - 1);

  if (endPage - startPage + 1 < maxPageButtons) {
    startPage = Math.max(1, endPage - maxPageButtons + 1);
  }

  return (
    <div className="page-wrapper">
      <div className="content">
        <div className="page-header">
          <div className="add-item d-flex">
            <div className="page-title">
              <h4>Exchange Rates</h4>
              <h6>Manage exchange rates</h6>
            </div>
          </div>

          <div className="page-btn">
            <Button variant="none" className="btn btn-added" onClick={handleShow}>
              <PlusCircle className="me-2" />
              Add Exchange Rate
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
                    <th>Currency</th>
                    <th>Exchange Rate</th>
                    <th style={{ width: "120px" }}>Action</th>
                  </tr>
                </thead>
                <tbody>
                  {loading ? (
                    <tr>
                      <td colSpan="3" className="text-center">
                        Loading...
                      </td>
                    </tr>
                  ) : currentRecords.length > 0 ? (
                    currentRecords.map((item, index) => (
                      <tr key={item.ExchangeRateID || index}>
                        <td>{item.Currency || "N/A"}</td>
                        <td>{item.ExchangeRate ?? "N/A"}</td>
                        <td>
                          <button
                            onClick={() => handleEdit(item)}
                            className="btn btn-sm btn-primary me-2"
                          >
                            <i className="feather-edit"></i>
                          </button>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan="3" className="text-center">
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
                    ).map((pageNumber) => (
                      <Button
                        key={pageNumber}
                        variant={currentPage === pageNumber ? "primary" : "light"}
                        size="sm"
                        className="mx-1"
                        onClick={() => setCurrentPage(pageNumber)}
                      >
                        {pageNumber}
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

        {showModal && (
          <ExchangeRateForm
            onSubmit={handleSave}
            showModel={showModal}
            handleClose={handleClose}
            data={selectedRecord}
          />
        )}
      </div>
    </div>
  );
};

export default ExchangeRatePage;