import React, { useEffect, useRef, useState, useMemo } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";
import Select from "react-select";

const ExtraForm = ({
  onSubmit,
  showModel,
  handleClose,
  data,
  productList,
  extraCategoryList,
  id,
}) => {
  const formRef = useRef(null);

  // Controlled react-select values (option objects)
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [selectedProductExtraItem, setSelectedProductExtraItem] = useState(null);

  // Build options once per list change
  const categoryOptions = useMemo(() => {
    return (extraCategoryList || []).map((c) => ({
      value: Number(c.ProductExtraCategoryID), // FORCE numeric ID
      label: c.Category || `Category ${c.ProductExtraCategoryID}`,
    }));
  }, [extraCategoryList]);

  const productOptions = useMemo(() => {
    return (productList || []).map((p) => ({
      value: Number(p.POS_ProductID), // FORCE numeric ID
      label: p.Description || `Product ${p.POS_ProductID}`,
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

    // Preselect Category on edit
    const fkCat =
      data?.FK_ProductExtraCategoryID != null
        ? Number(data.FK_ProductExtraCategoryID)
        : null;

    if (fkCat != null) {
      const match = categoryOptions.find((o) => o.value === fkCat) || null;
      setSelectedCategory(match);
    } else {
      setSelectedCategory(null);
    }

    // Preselect Product on edit
    const fkProd =
      data?.FK_ProductExtraID != null ? Number(data.FK_ProductExtraID) : null;

    if (fkProd != null) {
      const match = productOptions.find((o) => o.value === fkProd) || null;
      setSelectedProductExtraItem(match);
    } else {
      setSelectedProductExtraItem(null);
    }
  }, [showModel, data, categoryOptions, productOptions]);

  const handleSubmit = (e) => {
    e.preventDefault();
    const form = e.target;

    const extraData = {
      FK_ProductID: Number(id) || 0, // parent product
      // IMPORTANT: only ever send the selected option's value
      FK_ProductExtraCategoryID: selectedCategory ? Number(selectedCategory.value) : 0,
      FK_ProductExtraID: selectedProductExtraItem
        ? Number(selectedProductExtraItem.value)
        : 0,
      IsQuantified: form.IsQuantified.checked,
      Quantity: form.Quantity.value ? Number(form.Quantity.value) : 0,
      IsExtraCharge: form.IsExtraCharge.checked,
      DisplayOrder: form.DisplayOrder.value
        ? parseInt(form.DisplayOrder.value, 10)
        : 0,
    };

    if (data?.ProductExtraID) {
      extraData.ProductExtraID = data.ProductExtraID;
    }

    if (onSubmit) onSubmit(extraData);
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
          <Modal.Title>Extra</Modal.Title>
        </Modal.Header>

        <Modal.Body className="custom-modal-body">
          <div className="row">
            <div className="col-lg-6">
              <div className="input-blocks">
                <label>Extra Category</label>

                <Select
                  name="FK_ProductExtraCategoryID"
                  options={categoryOptions}
                  value={selectedCategory}
                  onChange={(opt) => setSelectedCategory(opt)}
                  isClearable
                  isSearchable
                  placeholder="Select Extra Category.."
                  filterOption={filterOption}
                  getOptionValue={(opt) => String(opt.value)}
                  getOptionLabel={(opt) => opt.label}
                  menuPortalTarget={document.body}
                  styles={{
                    menuPortal: (base) => ({ ...base, zIndex: 9999 }),
                  }}
                />
              </div>
            </div>

            <div className="col-lg-6">
              <div className="input-blocks">
                <label>Product Item</label>

                <Select
                  name="FK_ProductExtraID"
                  options={productOptions}
                  value={selectedProductExtraItem}
                  onChange={(opt) => setSelectedProductExtraItem(opt)}
                  isClearable
                  isSearchable
                  placeholder="Select Product.."
                  filterOption={filterOption}
                  getOptionValue={(opt) => String(opt.value)}
                  getOptionLabel={(opt) => opt.label}
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

export default ExtraForm;

ExtraForm.propTypes = {
  data: PropTypes.object,
  onSubmit: PropTypes.func.isRequired,
  showModel: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  productList: PropTypes.array.isRequired,
  extraCategoryList: PropTypes.array.isRequired,
  id: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
};
