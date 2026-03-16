import React, { useEffect, useMemo, useState } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";
import Select from "react-select";
import Swal from "sweetalert2";
import { getAllCurrency } from "../../../../services/entityData/currency";

const ExchangeRateForm = ({
  onSubmit,
  showModel,
  handleClose,
  data,
}) => {
  const [currencyList, setCurrencyList] = useState([]);
  const [selectedCurrency, setSelectedCurrency] = useState(null);
  const [exchangeRate, setExchangeRate] = useState("");
  const [loadingCurrencies, setLoadingCurrencies] = useState(false);

  useEffect(() => {
    if (showModel) {
      fetchCurrencies();
    }
  }, [showModel]);

  useEffect(() => {
    if (!showModel) return;

    setExchangeRate(
      data?.ExchangeRate !== undefined && data?.ExchangeRate !== null
        ? String(data.ExchangeRate)
        : ""
    );
  }, [showModel, data]);

  useEffect(() => {
    if (!showModel) return;
    if (!currencyList.length) return;

    if (data?.FK_CurrencyID) {
      const existing = currencyList.find(
        (item) => Number(item.CurrencyID) === Number(data.FK_CurrencyID)
      );

      if (existing) {
        setSelectedCurrency({
          value: existing.CurrencyID,
          label: `${existing.Currency} - ${existing.Name}`,
        });
        return;
      }
    }

    setSelectedCurrency(null);
  }, [showModel, data, currencyList]);

  const fetchCurrencies = async () => {
    try {
      setLoadingCurrencies(true);

      const response = await getAllCurrency();

      if (response?.Success && Array.isArray(response.Data)) {
        setCurrencyList(response.Data);
      } else if (Array.isArray(response)) {
        setCurrencyList(response);
      } else {
        setCurrencyList([]);
      }
    } catch (err) {
      console.error("Failed to load currencies:", err);
      setCurrencyList([]);
      Swal.fire({
        icon: "error",
        title: "Error",
        text: "Failed to load currencies.",
      });
    } finally {
      setLoadingCurrencies(false);
    }
  };

  const currencyOptions = useMemo(() => {
    return (currencyList || []).map((item) => ({
      value: item.CurrencyID,
      label: `${item.Currency} - ${item.Name}`,
    }));
  }, [currencyList]);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!selectedCurrency?.value) {
      Swal.fire({
        icon: "warning",
        title: "Validation",
        text: "Please select a currency.",
      });
      return;
    }

    if (
      exchangeRate === "" ||
      exchangeRate === null ||
      Number.isNaN(Number(exchangeRate))
    ) {
      Swal.fire({
        icon: "warning",
        title: "Validation",
        text: "Please enter a valid exchange rate.",
      });
      return;
    }

    const record = {
      FK_CurrencyID: Number(selectedCurrency.value),
      ExchangeRate: Number(exchangeRate),
    };

    if (data?.ExchangeRateID) {
      record.ExchangeRateID = data.ExchangeRateID;
    }

    onSubmit(record);
  };

  return (
    <Modal
      show={showModel}
      onHide={handleClose}
      centered
      dialogClassName="custom-modal-two"
    >
      <form onSubmit={handleSubmit}>
        <Modal.Header closeButton className="custom-modal-header border-0">
          <Modal.Title>
            {data?.ExchangeRateID ? "Edit Exchange Rate" : "Add Exchange Rate"}
          </Modal.Title>
        </Modal.Header>

        <Modal.Body className="custom-modal-body">
          <div className="row">
            <div className="col-lg-12">
              <div className="input-blocks">
                <label>Currency</label>
                <Select
                  options={currencyOptions}
                  value={selectedCurrency}
                  onChange={setSelectedCurrency}
                  isSearchable
                  placeholder={
                    loadingCurrencies ? "Loading currencies..." : "Select currency"
                  }
                  noOptionsMessage={() => "No currencies found"}
                  classNamePrefix="react-select"
                />
              </div>
            </div>

            <div className="col-lg-12">
              <div className="input-blocks">
                <label>Exchange Rate</label>
                <input
                  type="number"
                  step="0.000001"
                  min="0"
                  className="form-control"
                  value={exchangeRate}
                  onChange={(e) => setExchangeRate(e.target.value)}
                  placeholder="Enter exchange rate"
                />
              </div>
            </div>
          </div>
        </Modal.Body>

        <Modal.Footer className="modal-footer-btn d-flex justify-content-end">
          <button
            type="button"
            className="btn btn-cancel me-2"
            onClick={handleClose}
          >
            Cancel
          </button>
          <button type="submit" className="btn btn-submit">
            {data?.ExchangeRateID ? "Update" : "Submit"}
          </button>
        </Modal.Footer>
      </form>
    </Modal>
  );
};

export default ExchangeRateForm;

ExchangeRateForm.propTypes = {
  data: PropTypes.object,
  onSubmit: PropTypes.func.isRequired,
  showModel: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
};