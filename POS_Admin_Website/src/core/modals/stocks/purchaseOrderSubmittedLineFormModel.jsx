import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const PurchaseOrderSubmittedLineForm = ({
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

        const purchaseData = {
            IsDeclined: form.IsDeclined.checked,
            ManagerNotes: form.ManagerNotes.value.trim(),
        };

        if (data?.POS_PurchaseOrderLineID) {
            purchaseData.POS_PurchaseOrderLineID = data.POS_PurchaseOrderLineID;
        }
        if (data?.PurchaseOrderID) {
            purchaseData.FK_PurchaseOrderID = data.PurchaseOrderID;
        }
        if (onSubmit) {
            onSubmit(purchaseData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Purchase Order</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="form-check mb-3">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    id="IsDeclined"
                                    name="IsDeclined"
                                    defaultChecked={data?.IsDeclined || false}
                                />
                                <label className="form-check-label" htmlFor="IsDeclined">
                                    Is Declined
                                </label>
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Manager Notes</label>
                                <textarea rows={3} name="ManagerNotes" className="form-control" defaultValue={data?.ManagerNotes}></textarea>
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

export default PurchaseOrderSubmittedLineForm;


PurchaseOrderSubmittedLineForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
