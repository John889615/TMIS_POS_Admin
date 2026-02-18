import React, { useEffect, useRef, useState } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";

const MenuForm = ({ onSubmit, showModel, handleClose, data }) => {
  const formRef = useRef(null);

  const [filePreviewUrl, setFilePreviewUrl] = useState(null);

  // Reset form + preview when modal opens
  useEffect(() => {
    if (showModel && formRef.current) {
      formRef.current.reset();
      setFilePreviewUrl(null);
    }
  }, [showModel]);

  // Cleanup created object URLs
  useEffect(() => {
    return () => {
      if (filePreviewUrl) URL.revokeObjectURL(filePreviewUrl);
    };
  }, [filePreviewUrl]);

  const handleFileChange = (e) => {
    const file = e.target.files?.[0];
    if (!file) {
      setFilePreviewUrl(null);
      return;
    }

    // Replace old preview URL if any
    setFilePreviewUrl((prev) => {
      if (prev) URL.revokeObjectURL(prev);
      return URL.createObjectURL(file);
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const form = e.target;

    const menuData = {
      IsActive: form.IsActive.checked,
      MenuName: form.MenuName.value.trim(),
      ImageFile: form.ImageFile.files[0] || null,
    };

    // ✅ keep the ID when editing
    const id = data?.MenuID ?? data?.POS_MenuID;
    if (id) {
      menuData.MenuID = id;
      menuData.POS_MenuID = id; // keep only if your API expects it
    }

    onSubmit?.(menuData);
  };

  // Decide what image to show:
  // 1) newly selected file preview
  // 2) existing ImageUrl from API
  const previewSrc = filePreviewUrl || data?.ImageUrl || "";

  return (
    <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
      <form onSubmit={handleSubmit} ref={formRef}>
        <Modal.Header closeButton className="custom-modal-header border-0">
          <Modal.Title>Menu</Modal.Title>
        </Modal.Header>

        <Modal.Body className="custom-modal-body">
          <div className="row">
            <div className="col-lg-12">
              <div className="input-blocks">
                <label>Menu Name</label>
                <input
                  name="MenuName"
                  required
                  type="text"
                  defaultValue={data?.MenuName}
                  className="form-control"
                />
              </div>
            </div>

            <div className="col-lg-12">
              <div className="form-check mb-3">
                <input
                  type="checkbox"
                  className="form-check-input"
                  id="IsActive"
                  name="IsActive"
                  defaultChecked={data?.IsActive || false}
                />
                <label className="form-check-label" htmlFor="IsActive">
                  Is Active
                </label>
              </div>
            </div>

            {/* ✅ Image preview */}
            <div className="col-lg-12">
              <div className="input-blocks">
                <label>Menu Image</label>

                {previewSrc ? (
                  <div className="mb-2">
                    <img
                      src={previewSrc}
                      alt="Menu preview"
                      style={{
                        width: "100%",
                        maxWidth: 320,
                        height: 180,
                        objectFit: "cover",
                        borderRadius: 10,
                        border: "1px solid #e5e5e5",
                      }}
                      onError={(e) => {
                        // in case ImageUrl is broken
                        e.currentTarget.style.display = "none";
                      }}
                    />
                    <div className="small text-muted mt-1">
                      {filePreviewUrl ? "New image selected" : "Current image"}
                    </div>
                  </div>
                ) : (
                  <div className="small text-muted mb-2">No image yet</div>
                )}

                <input
                  name="ImageFile"
                  type="file"
                  accept="image/*"
                  className="form-control"
                  onChange={handleFileChange}
                />

                {/* Optional: "Remove selected" */}
                {filePreviewUrl && (
                  <button
                    type="button"
                    className="btn btn-sm btn-outline-secondary mt-2"
                    onClick={() => {
                      // Clear file input + preview
                      if (formRef.current) formRef.current.reset();
                      setFilePreviewUrl(null);
                    }}
                  >
                    Remove selected image
                  </button>
                )}
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
  );
};

export default MenuForm;

MenuForm.propTypes = {
  data: PropTypes.object,
  onSubmit: PropTypes.func.isRequired,
  showModel: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
};
