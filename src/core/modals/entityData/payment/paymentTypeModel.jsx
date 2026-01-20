import React, { useEffect, useRef, useState } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";
import IconPickerModal from "./IconPickerModal"; // adjust path

const PaymentTypeForm = ({ onSubmit, showModel, handleClose, data, iconList }) => {
  const formRef = useRef(null);

  const [showIconPicker, setShowIconPicker] = useState(false);
  const [selectedIcon, setSelectedIcon] = useState(null);

  useEffect(() => {
    if (showModel) {
      // reset form visuals
      if (formRef.current) formRef.current.reset();

      // set existing icon from data when editing
      const existing = iconList?.find(x => x.PaymentTypeIconID === data?.FK_PaymentTypeIconID) || null;
      setSelectedIcon(existing);
    }
  }, [showModel, data, iconList]);

  const handleSubmit = (e) => {
    e.preventDefault();
    const form = e.target;

    const record = {
      IsActive: form.IsActive.checked,
      IsPrimary: form.IsPrimary.checked,
      IsSecondary: form.IsSecondary.checked,
      SettlePayment: form.SettlePayment.checked,
      RequireAdditionalInfo: form.RequireAdditionalInfo.checked,
      RequireElevation: form.RequireElevation.checked,
      Name: form.Name.value.trim(),
      FK_PaymentTypeIconID: selectedIcon?.PaymentTypeIconID || null,
    };

    if (data?.PaymentTypeID) record.PaymentTypeID = data.PaymentTypeID;

    onSubmit?.(record);
  };

  return (
    <>
      <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
        <form onSubmit={handleSubmit} ref={formRef}>
          <Modal.Header closeButton className="custom-modal-header border-0">
            <Modal.Title>Payment Type</Modal.Title>
          </Modal.Header>

          <Modal.Body className="custom-modal-body">
            <div className="row">
              <div className="col-lg-12">
                <div className="input-blocks">
                  <label>Name</label>
                  <input
                    name="Name"
                    required
                    type="text"
                    defaultValue={data?.Name}
                    className="form-control"
                  />
                </div>
              </div>

              {/* Icon picker preview + button */}
              <div className="col-lg-12 mt-2">
                <label className="mb-1 d-block">Icon</label>

                <div className="d-flex align-items-center gap-3">
                  <div
                    className="border rounded d-flex align-items-center justify-content-center"
                    style={{ width: 54, height: 54 }}
                    title={selectedIcon?.IconPath || "No icon selected"}
                  >
                    {selectedIcon ? (
                      <i className={selectedIcon.IconPath} style={{ fontSize: 26 }} />
                    ) : (
                      <span style={{ fontSize: 12, opacity: 0.7 }}>None</span>
                    )}
                  </div>

                  <div className="flex-grow-1">
                    <div style={{ fontSize: 12, opacity: 0.8 }}>
                      {selectedIcon ? selectedIcon.IconPath : "No icon selected"}
                    </div>
                    <button
                      type="button"
                      className="btn btn-outline-primary btn-sm mt-1"
                      onClick={() => setShowIconPicker(true)}
                    >
                      Choose Icon
                    </button>

                    {selectedIcon && (
                      <button
                        type="button"
                        className="btn btn-outline-danger btn-sm mt-1 ms-2"
                        onClick={() => setSelectedIcon(null)}
                      >
                        Clear
                      </button>
                    )}
                  </div>
                </div>

                {/* Hidden input if you want it present in the form */}
                <input
                  type="hidden"
                  name="FK_PaymentTypeIconID"
                  value={selectedIcon?.PaymentTypeIconID || ""}
                  readOnly
                />
              </div>

              <div className="col-lg-12 mt-3">
                <div className="form-check mb-2">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    id="IsPrimary"
                    name="IsPrimary"
                    defaultChecked={data?.IsPrimary ?? false}
                  />
                  <label className="form-check-label" htmlFor="IsPrimary">
                    Is Primary
                  </label>
                </div>

                <div className="form-check mb-3">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    id="IsSecondary"
                    name="IsSecondary"
                    defaultChecked={data?.IsSecondary ?? false}
                  />
                  <label className="form-check-label" htmlFor="IsSecondary">
                    Is Secondary
                  </label>
                </div>

                <div className="form-check mb-3">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    id="IsActive"
                    name="IsActive"
                    defaultChecked={data?.IsActive ?? true}
                  />
                  <label className="form-check-label" htmlFor="IsActive">
                    Is Active
                  </label>
                </div>

                <div className="form-check mb-3">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    id="SettlePayment"
                    name="SettlePayment"
                    defaultChecked={data?.SettlePayment ?? false}
                  />
                  <label className="form-check-label" htmlFor="SettlePayment">
                    Settle Payment
                  </label>
                </div>

                <div className="form-check mb-3">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    id="RequireAdditionalInfo"
                    name="RequireAdditionalInfo"
                    defaultChecked={data?.RequireAdditionalInfo ?? false}
                  />
                  <label className="form-check-label" htmlFor="RequireAdditionalInfo">
                    Require Additional Info
                  </label>
                </div>

                <div className="form-check mb-3">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    id="RequireElevation"
                    name="RequireElevation"
                    defaultChecked={data?.RequireElevation ?? false}
                  />
                  <label className="form-check-label" htmlFor="RequireElevation">
                    Require Elevation
                  </label>
                </div>
              </div>
            </div>
          </Modal.Body>

          <Modal.Footer className="modal-footer-btn d-flex justify-content-end">
            <button type="button" className="btn btn-cancel me-2" onClick={handleClose}>
              Cancel
            </button>
            <button type="submit" className="btn btn-submit">
              Submit
            </button>
          </Modal.Footer>
        </form>
      </Modal>

      {/* Second modal */}
      <IconPickerModal
        show={showIconPicker}
        onClose={() => setShowIconPicker(false)}
        iconList={iconList || []}
        selectedIconId={selectedIcon?.PaymentTypeIconID}
        onPick={(item) => {
          setSelectedIcon(item);
          setShowIconPicker(false);
        }}
      />
    </>
  );
};

export default PaymentTypeForm;

PaymentTypeForm.propTypes = {
  data: PropTypes.object,
  onSubmit: PropTypes.func.isRequired,
  showModel: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  iconList: PropTypes.array.isRequired,
};
