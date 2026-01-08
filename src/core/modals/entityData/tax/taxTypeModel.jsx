import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const TaxTypeForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
}) => {
    const formRef = useRef(null);

    useEffect(() => {
        if (showModel && formRef.current) {
            formRef.current.reset(); // Reset form each time modal opens
        }
    }, [showModel]);


    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;

        const record = {
            IsActive: form.IsActive.checked,
            TaxName: form.TaxName.value.trim(),
            TaxPercentage: Number(form.TaxPercentage.value),
            ValidFrom: form.ValidFrom.value,
            ValidTo: form.ValidTo.value
        };

        if (data?.POS_TaxTypeID) {
            record.POS_TaxTypeID = data.POS_TaxTypeID;
        }

        if (onSubmit) {
            onSubmit(record);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Tax Type</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Name</label>
                                <input name="TaxName" required type="text" defaultValue={data?.TaxName} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Percentage</label>
                                <input name="TaxPercentage" required type="number" defaultValue={data?.TaxPercentage} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Valid From</label>
                                <input name="ValidFrom" type="datetime-local" defaultValue={data?.ValidFrom ? data.ValidFrom.substring(0, 16) : ""} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Valid To</label>
                                <input name="ValidTo" type="datetime-local" defaultValue={data?.ValidTo ? data.ValidTo.substring(0, 16) : ""} className="form-control" />
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
}

export default TaxTypeForm;


TaxTypeForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
