import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const PaymentTypeForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    iconList
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
            Name: form.Name.value.trim(),
        };

        if (data?.PaymentTypeID) {
            record.PaymentTypeID = data.PaymentTypeID;
        }

        if (onSubmit) {
            onSubmit(record);
        }
    };


    return (
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
                                <input name="Name" required type="text" defaultValue={data?.Name} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Icon Type</label>
                                <select name="FK_PaymentTypeIconID" className="form-select" defaultValue={data?.FK_PaymentTypeIconID || ''}>
                                    <option value="">Please select..</option>
                                    {iconList.map((item, index) => (
                                        <option key={index} value={item.PaymentTypeIconID}>
                                            {item.IconPath} / {item.Category}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-12 mt-3">
                            <div className="form-check mb-2">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    id="IsPrimary"
                                    name="IsPrimary"
                                    defaultChecked={data?.IsPrimary || false}
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
                                    defaultChecked={data?.IsSecondary || false}
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

export default PaymentTypeForm;


PaymentTypeForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    iconList: PropTypes.array.isRequired,
};
