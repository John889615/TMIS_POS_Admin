import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";
import { useSelector } from 'react-redux';

const PriceCodeForm = ({
    onSubmit,
    showModel,
    handleClose,
    data
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

        const codeData = {
            PriceCode: form.PriceCode?.value?.trim(),
            Description: form.Description?.value?.trim(),
            IsActive: !!form.IsActive.checked,
        };

        if (data?.POS_PriceCodeID) {
            codeData.POS_PriceCodeID = data.POS_PriceCodeID;
        }
        if (onSubmit) {
            onSubmit(codeData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Price Code</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Price Code</label>
                                <input name="PriceCode" required type="text" defaultValue={data?.PriceCode} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Description</label>
                                <textarea
                                    name="Description"
                                    defaultValue={data?.Description}
                                    className="form-control"
                                    rows={4}   // adjust rows as needed
                                />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks form-check mt-3">
                                <input
                                    type="checkbox"
                                    id="IsActive"
                                    name="IsActive"
                                    className="form-check-input"
                                    defaultChecked={typeof data?.IsActive === 'boolean' ? data.IsActive : true}
                                />
                                <label className="form-check-label" htmlFor="IsActive">Is Active</label>
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

export default PriceCodeForm;


PriceCodeForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired
};
