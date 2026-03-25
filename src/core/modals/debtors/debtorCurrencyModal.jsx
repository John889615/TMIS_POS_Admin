import React, { useEffect, useState } from "react";
import PropTypes from "prop-types";
import { Button, Modal } from "react-bootstrap";
import Swal from "sweetalert2";
import { Link as LinkIcon, XCircle } from "react-feather";
import {
  listLocationCurrencies,
  newLocationCurrency,
  deleteLocationCurrency,
} from "../../../services/entityData/currency";

const DebtorCurrencyModal = ({
  showCurrencyModel,
  handleCurrencyClose,
  debtorId,
}) => {
  const [locationCurrencyList, setLocationCurrencyList] = useState([]);
  const [loading, setLoading] = useState(false);
  const [actionLoadingId, setActionLoadingId] = useState(null);

  const fetchLocationCurrencies = async () => {
    if (!debtorId) return;

    try {
      setLoading(true);

      const response = await listLocationCurrencies(debtorId);

      if (response?.Success) {
        setLocationCurrencyList(response.Data || []);
      } else {
        setLocationCurrencyList([]);
        Swal.fire({
          icon: "error",
          title: "Error",
          text: response?.Messages?.[0] || "Failed to load location currencies",
        });
      }
    } catch (err) {
      console.error("Failed to load debtor currencies:", err.message);
      setLocationCurrencyList([]);
      Swal.fire({
        icon: "error",
        title: "Error",
        text: err.message || "Failed to load debtor currencies",
      });
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (showCurrencyModel && debtorId) {
      fetchLocationCurrencies();
    } else {
      setLocationCurrencyList([]);
      setActionLoadingId(null);
    }
  }, [showCurrencyModel, debtorId]);

  const handleToggleCurrency = async (currency) => {
    try {
      setActionLoadingId(currency.CurrencyID);

      if (currency.IsActive) {
        await deleteLocationCurrency({
          LocationCurrencyID: currency.LocationCurrencyID,
          LocationID: debtorId,
          CurrencyID: currency.CurrencyID,
        });

      } else {
        await newLocationCurrency({
          LocationID: debtorId,
          CurrencyID: currency.CurrencyID,
          IsActive: true,
        });
      }

      await fetchLocationCurrencies();
    } catch (err) {
      console.error("Currency update failed:", err.message);
      Swal.fire({
        icon: "error",
        title: "Error",
        text: err.message || "Failed to update location currency",
      });
    } finally {
      setActionLoadingId(null);
    }
  };

  return (
    <Modal
      show={showCurrencyModel}
      onHide={handleCurrencyClose}
      centered
      size="lg"
    >
      <Modal.Header closeButton className="border-0 pb-0">
        <div className="w-100">
          <h4 className="mb-1">Currency Linking</h4>
          <h6 className="text-muted mb-0">Manage linked currencies for this debtor</h6>
        </div>
      </Modal.Header>

      <Modal.Body className="pt-3">
        <div className="card table-list-card mb-0">
          <div className="card-body">
            <div className="table-responsive">
              <table className="table table-bordered table-striped mb-0">
                <thead>
                  <tr>
                    <th>Currency</th>
                    <th>Symbol</th>
                    <th style={{ width: "160px" }}>Action</th>
                  </tr>
                </thead>
                <tbody>
                  {loading ? (
                    <tr>
                      <td colSpan="4" className="text-center">
                        Loading currencies...
                      </td>
                    </tr>
                  ) : locationCurrencyList.length > 0 ? (
                    locationCurrencyList.map((currency) => {
                      const isBusy = actionLoadingId === currency.CurrencyID;

                      return (
                        <tr key={currency.CurrencyID}>
                          <td>{currency.Currency || "N/A"}</td>
                          <td>{currency.Symbol || "N/A"}</td>
                          <td>
                            <button
                              type="button"
                              className={`btn btn-sm d-inline-flex align-items-center justify-content-center ${
                                currency.IsActive
                                  ? "btn-outline-danger"
                                  : "btn-outline-success"
                              }`}
                              onClick={() => handleToggleCurrency(currency)}
                              title={
                                currency.IsActive
                                  ? "Unlink Currency"
                                  : "Link Currency"
                              }
                              style={{ minWidth: "42px", height: "32px" }}
                              disabled={isBusy}
                            >
                              {currency.IsActive ? (
                                <XCircle size={14} />
                              ) : (
                                <LinkIcon size={14} />
                              )}
                            </button>
                          </td>
                        </tr>
                      );
                    })
                  ) : (
                    <tr>
                      <td colSpan="4" className="text-center">
                        No currencies found
                      </td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </Modal.Body>

      <Modal.Footer className="border-0 pt-2">
        <Button variant="light" onClick={handleCurrencyClose}>
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

DebtorCurrencyModal.propTypes = {
  showCurrencyModel: PropTypes.bool.isRequired,
  handleCurrencyClose: PropTypes.func.isRequired,
  debtorId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
};

export default DebtorCurrencyModal;