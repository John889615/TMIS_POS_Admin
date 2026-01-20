import React, { useEffect, useRef, useState, useMemo } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";
import Select from "react-select";

const CombinationForm = ({
  onSubmit,
  showModel,
  handleClose,
  data,
  productList,
  id,
}) => {
  const formRef = useRef(null);

  // Controlled react-select value (option object)
  const [selectedProductItem, setSelectedProductItem] = useState(null);

  // Build options once per productList change
  const productOptions = useMemo(() => {
    return (productList || []).map((p) => ({
      value: Number(p.POS_ProductID), // FORCE numeric ID
      label: p.ProductName || `Product ${p.POS_ProductID}`,
    }));
  }, [productList]);

  // Better filtering (multi-word)
  const filterOption = (candidate, input) => {
    const text = (input || "").trim().toLowerCase();
    if (!text) return true;
    const label = (candidate.label || "").toLowerCase();
    return text.split(/\s+/).every((w) => label.includes(w));
  };

  useEffect(() => {
    if (!showModel) return;

    // Reset native inputs
    if (formRef.current) formRef.current.reset();

    // When editing, preselect the correct option by matching numeric IDs
    const fk = data?.FK_ProductItemID != null ? Number(data.FK_ProductItemID) : null;

    if (fk != null) {
      const match = productOptions.find((o) => o.value === fk) || null;
      setSelectedProductItem(match);
    } else {
      setSelectedProductItem(null);
    }
  }, [showModel, data, productOptions]);

  const handleSubmit = (e) => {
    e.preventDefault();
    const form = e.target;

    const productData = {
      FK_ProductID: Number(id) || 0, // parent product
      // IMPORTANT: only ever send the selected option's value (POS_ProductID)
      FK_ProductItemID: selectedProductItem ? Number(selectedProductItem.value) : 0,
      IsQuantified: form.IsQuantified.checked,
      Quantity: form.Quantity.value ? Number(form.Quantity.value) : 0,
      IsOptional: form.IsOptional.checked,
      IsExtraCharge: form.IsExtraCharge.checked,
      DisplayOrder: form.DisplayOrder.value ? parseInt(form.DisplayOrder.value, 10) : 0,
    };

    if (data?.ProductCombinationID) {
      productData.ProductCombinationID = data.ProductCombinationID;
    }

    if (onSubmit) onSubmit(productData);
  };

  return (
    <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
      <form onSubmit={handleSubmit} ref={formRef}>
        <Modal.Header closeButton className="custom-modal-header border-0">
          <Modal.Title>Combination</Modal.Title>
        </Modal.Header>

        <Modal.Body className="custom-modal-body">
          <div className="row">
            <div className="col-lg-6">
              <div className="input-blocks">
                <label>Product Item</label>

                <Select
                  name="FK_ProductItemID"
                  options={productOptions}
                  value={selectedProductItem}
                  onChange={(opt) => setSelectedProductItem(opt)}
                  isClearable
                  isSearchable
                  placeholder="Select Product.."
                  filterOption={filterOption}
                  // Ensures react-select treats options as stable by ID
                  getOptionValue={(opt) => String(opt.value)}
                  getOptionLabel={(opt) => opt.label}
                  // Fix dropdown inside modal (z-index)
                  menuPortalTarget={document.body}
                  styles={{
                    menuPortal: (base) => ({ ...base, zIndex: 9999 }),
                  }}
                />
              </div>
            </div>

            <div className="col-lg-6">
              <div className="input-blocks mt-4">
                <div className="form-check">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    name="IsQuantified"
                    defaultChecked={!!data?.IsQuantified}
                  />
                  <label className="form-check-label">Is Quantified</label>
                </div>
              </div>
            </div>

            <div className="col-lg-6">
              <div className="input-blocks">
                <label>Quantity</label>
                <input
                  name="Quantity"
                  type="number"
                  className="form-control"
                  defaultValue={data?.Quantity ?? 0}
                />
              </div>
            </div>

            <div className="col-lg-6">
              <div className="input-blocks mt-4">
                <div className="form-check">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    name="IsOptional"
                    defaultChecked={!!data?.IsOptional}
                  />
                  <label className="form-check-label">Is Optional</label>
                </div>
              </div>
            </div>

            <div className="col-lg-6">
              <div className="input-blocks mt-4">
                <div className="form-check">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    name="IsExtraCharge"
                    defaultChecked={!!data?.IsExtraCharge}
                  />
                  <label className="form-check-label">Is Extra Charge</label>
                </div>
              </div>
            </div>

            <div className="col-lg-6">
              <div className="input-blocks">
                <label>Display Order</label>
                <input
                  name="DisplayOrder"
                  type="number"
                  className="form-control"
                  defaultValue={data?.DisplayOrder ?? 0}
                />
              </div>
            </div>
          </div>
        </Modal.Body>

        <Modal.Footer className="modal-footer-btn">
          <button type="button" className="btn btn-cancel me-2" onClick={handleClose}>
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

export default CombinationForm;

CombinationForm.propTypes = {
  data: PropTypes.object,
  onSubmit: PropTypes.func.isRequired,
  showModel: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  productList: PropTypes.array.isRequired,
  id: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
};
