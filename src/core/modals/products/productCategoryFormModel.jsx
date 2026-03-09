import React, { useEffect, useMemo, useRef, useState } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";
import Select from "react-select";

const ProductCategoryForm = ({
  onSubmit,
  showModel,
  handleClose,
  data,
  categoryList,
}) => {
  const formRef = useRef(null);
  const [selectedCategory, setSelectedCategory] = useState(null);

  const categoryOptions = useMemo(() => {
    return (categoryList || []).map((item) => ({
      value: item.POS_ProductCategoryID,
      label: item.CategoryName,
    }));
  }, [categoryList]);

  useEffect(() => {
    if (!showModel) {
      setSelectedCategory(null);
      return;
    }

    if (formRef.current) {
      formRef.current.reset();
    }

    const initialFk = data?.FK_ProductCategoryID ?? null;
    const existing =
      categoryOptions.find((o) => Number(o.value) === Number(initialFk)) || null;

    setSelectedCategory(existing);
  }, [showModel, data, categoryOptions]);

  const handleSubmit = (e) => {
    e.preventDefault();
    const form = e.target;

    const FK_ProductCategoryID = selectedCategory?.value ?? null;

    const categoryData = {
      CategoryName: form.CategoryName.value.trim(),
      FK_ProductCategoryID: FK_ProductCategoryID
        ? parseInt(FK_ProductCategoryID)
        : null,
      IsMaster: !FK_ProductCategoryID,
    };

    if (data?.POS_ProductCategoryID) {
      categoryData.POS_ProductCategoryID = data.POS_ProductCategoryID;
    }

    onSubmit?.(categoryData);
  };

  /**
   * ✅ WHY IT'S STILL WHITE
   * Some themes override react-select with CSS like:
   *   .react-select__input-container { color: #fff; }
   * or even apply color on a parent div.
   *
   * So we force it in TWO places:
   *  1) styles.input + styles.inputContainer
   *  2) theme() override (neutral50, neutral80 etc)
   *  3) option/menuPortal to ensure dropdown text is visible too
   */
  const selectStyles = useMemo(
    () => ({
      container: (base) => ({ ...base, width: "100%" }),

      control: (base, state) => ({
        ...base,
        minHeight: 38,
        height: 38,
        borderRadius: 8,
        backgroundColor: "#fff",
        color: "#000",
        boxShadow: state.isFocused ? "0 0 0 0.2rem rgba(13,110,253,.25)" : base.boxShadow,
        borderColor: state.isFocused ? "#86b7fe" : base.borderColor,
      }),

      valueContainer: (base) => ({
        ...base,
        height: 38,
        padding: "0 10px",
        color: "#000",
      }),

      // ✅ IMPORTANT: forces the wrapper that actually holds typed text
      inputContainer: (base) => ({
        ...base,
        color: "#000",
      }),

      // ✅ IMPORTANT: forces the actual input element
      input: (base) => ({
        ...base,
        color: "#000",
        caretColor: "#000", // ✅ cursor color too
      }),

      singleValue: (base) => ({
        ...base,
        color: "#000",
      }),

      placeholder: (base) => ({
        ...base,
        color: "#6c757d",
      }),

      option: (base, state) => ({
        ...base,
        color: "#000",
        backgroundColor: state.isFocused ? "#f1f1f1" : "#fff",
      }),

      menu: (base) => ({
        ...base,
        zIndex: 9999,
      }),

      // ✅ If your modal/parent has weird stacking, portal fixes it
      menuPortal: (base) => ({ ...base, zIndex: 99999 }),
    }),
    []
  );

  // ✅ Theme override helps if your CSS is clobbering colors
  const selectTheme = (theme) => ({
    ...theme,
    colors: {
      ...theme.colors,
      neutral80: "#000", // main text
      neutral50: "#6c757d", // placeholder
      neutral0: "#fff", // control bg
      primary25: "#f1f1f1", // option hover
      primary: "#0d6efd", // focus color
    },
  });

  return (
    <Modal
      show={showModel}
      onHide={handleClose}
      centered
      dialogClassName="custom-modal-two"
    >
      <form onSubmit={handleSubmit} ref={formRef}>
        <Modal.Header closeButton className="custom-modal-header border-0">
          <Modal.Title>Category</Modal.Title>
        </Modal.Header>

        <Modal.Body className="custom-modal-body">
          <div className="row">
            <div className="col-lg-6">
              <div className="input-blocks">
                <label>Category Name</label>
                <input
                  name="CategoryName"
                  required
                  type="text"
                  defaultValue={data?.CategoryName}
                  className="form-control"
                />
              </div>
            </div>

            <div className="col-lg-6">
              <div className="input-blocks">
                <label>Category</label>

                <Select
                  options={categoryOptions}
                  value={selectedCategory}
                  onChange={setSelectedCategory}
                  placeholder="Please select..."
                  isClearable
                  isSearchable
                  classNamePrefix="react-select"
                  styles={selectStyles}
                  theme={selectTheme}
                  menuPortalTarget={document.body} // ✅ helps in modals + z-index + some CSS inheritance
                  noOptionsMessage={() => "No categories found"}
                />

                <input
                  type="hidden"
                  name="FK_ProductCategoryID"
                  value={selectedCategory?.value ?? ""}
                  readOnly
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

export default ProductCategoryForm;

ProductCategoryForm.propTypes = {
  data: PropTypes.object,
  onSubmit: PropTypes.func.isRequired,
  showModel: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  categoryList: PropTypes.array.isRequired,
};