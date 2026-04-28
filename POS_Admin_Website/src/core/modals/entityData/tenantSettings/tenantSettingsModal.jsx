import React, { useEffect, useMemo, useState } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";
import Select from "react-select";
import Swal from "sweetalert2";

import { getAllCurrency } from "../../../../services/entityData/currency";

const TenantSettingsForm = ({
  onSubmit,
  showModel,
  handleClose,
  data,
  isFirstSetup,
}) => {
  const [company, setCompany] = useState("");
  const [currencyList, setCurrencyList] = useState([]);
  const [selectedCurrency, setSelectedCurrency] = useState(null);
  const [loadingCurrencies, setLoadingCurrencies] = useState(false);

  useEffect(() => {
    if (showModel) {
      fetchCurrencies();
    }
  }, [showModel]);

  useEffect(() => {
    if (!showModel) return;

    setCompany(data?.Company || "");
  }, [showModel, data]);

  useEffect(() => {
    if (!showModel) return;

    if (!currencyList.length) {
      setSelectedCurrency(null);
      return;
    }

    if (data?.FK_CurrencyID) {
      const existingCurrency = currencyList.find(
        (item) => Number(item.CurrencyID) === Number(data.FK_CurrencyID)
      );

      if (existingCurrency) {
        setSelectedCurrency({
          value: existingCurrency.CurrencyID,
          label: `${existingCurrency.Currency} - ${existingCurrency.Name}`,
          currency: existingCurrency.Currency,
          name: existingCurrency.Name,
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
        return;
      }

      if (Array.isArray(response)) {
        setCurrencyList(response);
        return;
      }

      setCurrencyList([]);
    } catch (err) {
      console.error("Failed to load currencies:", err);
      Swal.fire({
        icon: "error",
        title: "Error",
        text: err?.response?.data?.Messages?.[0] || "Failed to load currencies",
      });
      setCurrencyList([]);
    } finally {
      setLoadingCurrencies(false);
    }
  };

  const currencyOptions = useMemo(() => {
    return (currencyList || []).map((item) => ({
      value: item.CurrencyID,
      label: `${item.Currency} - ${item.Name}`,
      currency: item.Currency,
      name: item.Name,
    }));
  }, [currencyList]);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!company.trim()) {
      Swal.fire({
        icon: "warning",
        title: "Validation",
        text: "Company is required.",
      });
      return;
    }

    if (!selectedCurrency?.value) {
      Swal.fire({
        icon: "warning",
        title: "Validation",
        text: "Please select a currency.",
      });
      return;
    }

    const record = {
      Company: company.trim(),
      FK_CurrencyID: Number(selectedCurrency.value),
    };

    if (data?.SettingID) {
      record.SettingID = data.SettingID;
    }

    onSubmit(record);
  };

  return (
    <Modal
      show={showModel}
      onHide={isFirstSetup ? undefined : handleClose}
      backdrop={isFirstSetup ? "static" : true}
      keyboard={!isFirstSetup}
      centered
      dialogClassName="custom-modal-two"
    >
      <form onSubmit={handleSubmit}>
        <Modal.Header
          closeButton={!isFirstSetup}
          className="custom-modal-header border-0"
        >
          <Modal.Title>
            {data?.SettingID ? "Update Settings" : "Add Settings"}
          </Modal.Title>
        </Modal.Header>

        <Modal.Body className="custom-modal-body">
          <div className="row">
            <div className="col-lg-12">
              <div className="input-blocks">
                <label>Company</label>
                <input
                  name="Company"
                  required
                  type="text"
                  value={company}
                  onChange={(e) => setCompany(e.target.value)}
                  className="form-control"
                  placeholder="Enter company name"
                />
              </div>
            </div>

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
          </div>
        </Modal.Body>

        <Modal.Footer className="modal-footer-btn d-flex justify-content-end">
          {!isFirstSetup && (
            <button
              type="button"
              className="btn btn-cancel me-2"
              onClick={handleClose}
            >
              Cancel
            </button>
          )}

          <button type="submit" className="btn btn-submit">
            {data?.SettingID ? "Update" : "Submit"}
          </button>
        </Modal.Footer>
      </form>
    </Modal>
  );
};

export default TenantSettingsForm;

TenantSettingsForm.propTypes = {
  data: PropTypes.object,
  onSubmit: PropTypes.func.isRequired,
  showModel: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  isFirstSetup: PropTypes.bool,
};

TenantSettingsForm.defaultProps = {
  data: null,
  isFirstSetup: false,
};