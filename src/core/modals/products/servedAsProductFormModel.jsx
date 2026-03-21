import React, { useState } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";

const ServedAsProductForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    servedAsList,
    id,
}) => {
    const selectedServedAsId =
        data?.ServedAsID ||
        data?.FK_ServedAsID ||
        "";

    const [isQuantified, setIsQuantified] = useState(Boolean(data?.IsQuantified));

    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;

        const payload = {
            ServedAsProductID: Number(data?.ServedAsProductID) || 0,
            ProductID: Number(id) || 0,
            ServedAsID: Number(form.ServedAsID.value) || 0,
            IsQuantified: isQuantified,
            Quantity: isQuantified ? Number(form.Quantity.value) || 0 : 0,
        };

        if (onSubmit) {
            onSubmit(payload);
        }
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
                        {data?.ServedAsProductID ? "Edit Product Served As" : "Add Product Served As"}
                    </Modal.Title>
                </Modal.Header>

                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Served As</label>
                                <select
                                    name="ServedAsID"
                                    required
                                    defaultValue={selectedServedAsId}
                                    className="form-control"
                                >
                                    <option value="">Select served as</option>
                                    {(servedAsList || []).map((item) => (
                                        <option key={item.ServedAsID} value={item.ServedAsID}>
                                            {item.Name} ({item.ServedAsType})
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="col-lg-3">
                            <div className="input-blocks">
                                <label className="d-block">Is Quantified</label>
                                <div className="form-check mt-2">
                                    <input
                                        id="product-served-as-is-quantified"
                                        name="IsQuantified"
                                        type="checkbox"
                                        className="form-check-input"
                                        checked={isQuantified}
                                        onChange={(e) => setIsQuantified(e.target.checked)}
                                    />
                                    <label
                                        htmlFor="product-served-as-is-quantified"
                                        className="form-check-label"
                                    >
                                        Yes
                                    </label>
                                </div>
                            </div>
                        </div>

                        <div className="col-lg-3">
                            <div className="input-blocks">
                                <label>Quantity</label>
                                <input
                                    name="Quantity"
                                    type="number"
                                    min="0"
                                    step="1"
                                    defaultValue={data?.Quantity ?? 0}
                                    disabled={!isQuantified}
                                    className="form-control"
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

export default ServedAsProductForm;

ServedAsProductForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    servedAsList: PropTypes.array,
    id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
};