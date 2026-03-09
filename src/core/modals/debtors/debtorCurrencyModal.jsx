import React, { useEffect, useState } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";
import Swal from "sweetalert2";
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
    }
  }, [showCurrencyModel, debtorId]);

  const handleToggleCurrency = async (currency) => {
    try {
      if (currency.IsActive) {
        await deleteLocationCurrency({
          LocationCurrencyID: currency.LocationCurrencyID,
          LocationID: debtorId,
          CurrencyID: currency.CurrencyID,
        });

        Swal.fire({
          icon: "success",
          title: "Removed",
          text: `${currency.Currency} removed from location`,
          timer: 1400,
          showConfirmButton: false,
        });
      } else {
        await newLocationCurrency({
          LocationID: debtorId,
          CurrencyID: currency.CurrencyID,
          IsActive: true,
        });

        Swal.fire({
          icon: "success",
          title: "Added",
          text: `${currency.Currency} added to location`,
          timer: 1400,
          showConfirmButton: false,
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
    }
  };

  return (
    <Modal
      show={showCurrencyModel}
      onHide={handleCurrencyClose}
      centered
      size="lg"
      dialogClassName="custom-modal-two"
    >
      <Modal.Header closeButton className="custom-modal-header border-0">
        <Modal.Title>Manage Location Currencies</Modal.Title>
      </Modal.Header>

      <Modal.Body className="custom-modal-body">
        {loading ? (
          <div className="text-center py-4">Loading currencies...</div>
        ) : (
          <div className="table-responsive">
            <table className="table table-bordered table-striped align-middle">
              <thead>
                <tr>
                  <th style={{ width: "90px" }}>Action</th>
                  <th>Currency</th>
                  <th>Symbol</th>
                  <th>Status</th>
                </tr>
              </thead>
              <tbody>
                {locationCurrencyList.length > 0 ? (
                  locationCurrencyList.map((currency) => (
                    <tr key={currency.CurrencyID}>
                      <td>
                        <button
                          type="button"
                          className={`btn btn-sm ${currency.IsActive ? "btn-success" : "btn-danger"}`}
                          onClick={() => handleToggleCurrency(currency)}
                          title={
                            currency.IsActive
                              ? "Currency linked - click to remove"
                              : "Currency not linked - click to add"
                          }
                        >
                          {currency.IsActive ? "+" : "-"}
                        </button>
                      </td>
                      <td>{currency.Currency || "N/A"}</td>
                      <td>{currency.Symbol || "N/A"}</td>
                      <td>
                        {currency.IsActive ? (
                          <span className="badge bg-success">Active</span>
                        ) : (
                          <span className="badge bg-danger">Inactive</span>
                        )}
                      </td>
                    </tr>
                  ))
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
        )}
      </Modal.Body>

      <Modal.Footer className="modal-footer-btn">
        <button
          type="button"
          className="btn btn-cancel"
          onClick={handleCurrencyClose}
        >
          Close
        </button>
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