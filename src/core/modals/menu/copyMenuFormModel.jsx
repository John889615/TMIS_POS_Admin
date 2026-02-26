import React, { useEffect, useMemo, useRef, useState } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";
import { useSelector } from "react-redux";
import Select from "react-select";

const CopyMenuForm = ({
  onSubmit,
  showModel,
  handleClose,
  data,
  debtorList,
  costCenterList,
  slipPrinterList,
}) => {
  const formRef = useRef(null);
  const debtorId = useSelector((state) => state.selectedDebtorStore);

  const [selectedDebtor, setSelectedDebtor] = useState(null);
  const [selectedCostCenter, setSelectedCostCenter] = useState(null);
  const [selectedSlipPrinter, setSelectedSlipPrinter] = useState(null);

  const debtorOptions = useMemo(
    () =>
      (debtorList || []).map((item) => ({
        value: item.DebtorID,
        label: `${item.Name} / ${item.ShortCode}`,
      })),
    [debtorList]
  );

  const costCenterOptions = useMemo(
    () =>
      (costCenterList || []).map((item) => ({
        value: item.CostCenterID,
        label: item.Name,
      })),
    [costCenterList]
  );

  const slipPrinterOptions = useMemo(
    () =>
      (slipPrinterList || []).map((item) => ({
        value: item.SlipPrinterID,
        label: item.Name,
      })),
    [costCenterList]
  );

  const resetForm = () => {
    formRef.current?.reset();
    setSelectedDebtor(null);
    setSelectedCostCenter(null);
    setSelectedSlipPrinter(null);
  };

  // When modal opens, set defaults (debtorId takes priority), otherwise reset
  useEffect(() => {
    if (!showModel) {
      resetForm();
      return;
    }

    const initialDebtorId = debtorId || data?.TargetDebtorID || null;
    const initialCostCenterId = data?.TargetCostCenterID || null;
    const initialSlipPrinterId = data?.SlipPrinterID || null;

    const existingDebtor = debtorOptions.find((d) => d.value === initialDebtorId) || null;
    const existingCostCenter = costCenterOptions.find((c) => c.value === initialCostCenterId) || null;
    const existingSlipPrinter = slipPrinterOptions.find((c) => c.value === initialSlipPrinterId) || null;

    setSelectedDebtor(existingDebtor);
    setSelectedCostCenter(existingCostCenter);
    setSelectedSlipPrinter(existingSlipPrinter);
  }, [showModel, data, debtorId, debtorOptions, costCenterOptions, slipPrinterOptions]);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!selectedDebtor) return; // Debtor is required

    const menuData = {
      SourceMenuID: data.MenuID,
      TargetDebtorID: parseFloat(selectedDebtor.value) || 0,
      TargetCostCenterID: selectedCostCenter ? parseFloat(selectedCostCenter.value) : null,
      TargetSlipPrinterID: selectedSlipPrinter ? parseFloat(selectedSlipPrinter.value) : null,
      Override: true,
    };

    onSubmit?.(menuData);
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
          <Modal.Title>Menu</Modal.Title>
        </Modal.Header>

        <Modal.Body className="custom-modal-body">
          <div className="row">
            <div className="col-lg-12">
              <div className="input-blocks">
                <label>Debtor</label>
                <Select
                  options={debtorOptions}
                  value={selectedDebtor}
                  onChange={setSelectedDebtor}
                  placeholder="Search debtor..."
                  isClearable
                  classNamePrefix="react-select"
                />
                {/* if you want it visually "required" add a small message when empty */}
                {!selectedDebtor && (
                  <small className="text-danger">Debtor is required.</small>
                )}
              </div>
            </div>

            <div className="col-lg-12 mt-2">
              <div className="input-blocks">
                <label>Cost Center</label>
                <Select
                  options={costCenterOptions}
                  value={selectedCostCenter}
                  onChange={setSelectedCostCenter}
                  placeholder="Search cost center..."
                  isClearable
                  classNamePrefix="react-select"
                />
              </div>
            </div>

            <div className="col-lg-12 mt-2">
              <div className="input-blocks">
                <label>Slip Printer</label>
                <Select
                  options={slipPrinterOptions}
                  value={selectedSlipPrinter}
                  onChange={setSelectedSlipPrinter}
                  placeholder="Search slip printer..."
                  isClearable
                  classNamePrefix="react-select"
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
            Submit
          </button>
        </Modal.Footer>
      </form>
    </Modal>
  );
};

export default CopyMenuForm;

CopyMenuForm.propTypes = {
  data: PropTypes.object,
  onSubmit: PropTypes.func.isRequired,
  showModel: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  debtorList: PropTypes.array.isRequired,
  costCenterList: PropTypes.array.isRequired,
  slipPrinterList: PropTypes.array.isRequired,
};
