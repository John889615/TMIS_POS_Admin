import React, { useEffect, useRef } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";

const DebtorForm = ({
  currencyList,
  onSubmitDebtor,
  showModel,
  handleClose,
  debtorData,
}) => {
  const formRef = useRef(null);

  const resetForm = () => {
    formRef.current?.reset();
  };

  useEffect(() => {
    if (!showModel) {
      resetForm();
    }
  }, [showModel]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const form = e.target;

    const defaultCurrency = (currencyList || [])[0];

    const data = {
      ShortCode: form.ShortCode.value.trim(),
      Name: form.Name.value.trim(),
      FK_CurrencyID: defaultCurrency?.CurrencyID ?? null,
      IsActive: form.IsActive.checked,
    };

    if (debtorData?.DebtorID) {
      data.DebtorID = debtorData.DebtorID;
    }

    await onSubmitDebtor(data);

    resetForm();
  };

  return (
    <Modal
      show={showModel}
      onHide={handleClose}
      centered
      dialogClassName="custom-modal-two"
    >
      <form onSubmit={handleSubmit} ref={formRef}>
        <Modal.Header closeButton className="custom-modal-header border-0">
          <Modal.Title>{debtorData?.DebtorID ? "Update Debtor" : "Add Debtor"}</Modal.Title>
        </Modal.Header>

        <Modal.Body className="custom-modal-body">
          <div className="row">
            <div className="col-lg-6">
              <div className="input-blocks">
                <label>Short Code</label>
                <input
                  name="ShortCode"
                  type="text"
                  className="form-control"
                  defaultValue={debtorData?.ShortCode || ""}
                  required
                />
              </div>
            </div>

            <div className="col-lg-6">
              <div className="input-blocks">
                <label>Name</label>
                <input
                  name="Name"
                  type="text"
                  className="form-control"
                  defaultValue={debtorData?.Name || ""}
                  required
                />
              </div>
            </div>

            <div className="col-lg-12 mt-2">
              <div className="alert alert-light border">
                Default currency:{" "}
                <strong>
                  {currencyList?.[0]
                    ? `${currencyList[0].Name}${currencyList[0].Code ? ` (${currencyList[0].Code})` : ""}`
                    : "No currency found"}
                </strong>
              </div>
            </div>

            <div className="col-lg-6 mt-3">
              <div className="input-blocks form-check">
                <input
                  type="checkbox"
                  name="IsActive"
                  defaultChecked={debtorData?.IsActive ?? true}
                  className="form-check-input"
                  id="debtorIsActive"
                />
                <label className="form-check-label" htmlFor="debtorIsActive">
                  Is Active?
                </label>
              </div>
            </div>
          </div>
        </Modal.Body>

        <Modal.Footer className="modal-footer-btn">
          <button
            type="button"
            className="btn btn-cancel me-2"
            onClick={handleClose}
          >
            Cancel
          </button>
          <button type="submit" className="btn btn-submit">
            Submit
          </button>
        </Modal.Footer>
      </form>
    </Modal>
  );
};

DebtorForm.propTypes = {
  currencyList: PropTypes.array.isRequired,
  debtorData: PropTypes.object,
  onSubmitDebtor: PropTypes.func.isRequired,
  showModel: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
};

export default DebtorForm;