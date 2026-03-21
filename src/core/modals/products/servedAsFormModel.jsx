import React from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";

const SERVED_AS_TYPE_OPTIONS = [
    { value: "Food", label: "Food" },
    { value: "Drink", label: "Drink" },
];

const ServedAsForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
}) => {
    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;

        const payload = {
            ServedAsID: data?.ServedAsID || 0,
            ServedAsType: form.ServedAsType.value,
            Name: form.Name.value.trim(),
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
                        {data?.ServedAsID ? "Edit Served As" : "Add Served As"}
                    </Modal.Title>
                </Modal.Header>

                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Served As Type</label>
                                <select
                                    name="ServedAsType"
                                    required
                                    defaultValue={data?.ServedAsType || ""}
                                    className="form-control"
                                >
                                    <option value="">Select type</option>
                                    {SERVED_AS_TYPE_OPTIONS.map((option) => (
                                        <option key={option.value} value={option.value}>
                                            {option.label}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Name</label>
                                <input
                                    name="Name"
                                    required
                                    type="text"
                                    defaultValue={data?.Name || ""}
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

export default ServedAsForm;

ServedAsForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};