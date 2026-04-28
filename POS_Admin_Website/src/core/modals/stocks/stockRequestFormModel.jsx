import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";
import { useSelector } from 'react-redux';

const StockRequestForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
}) => {
    const formRef = useRef(null);
    const debtors = useSelector((state) => state.debtors_data);
    useEffect(() => {
        if (showModel && formRef.current) {
            formRef.current.reset(); // Reset form each time modal opens
        }
    }, [showModel]);


    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;

        const stockData = {
            RefNumber: form.RefNumber?.value?.trim(),
            FK_FromDebtorID: form.FK_FromDebtorID.value ? parseInt(form.FK_FromDebtorID.value) : null,
            FK_ToDebtorID: form.FK_ToDebtorID.value ? parseInt(form.FK_ToDebtorID.value) : null,
            Notes: form.Notes.value.trim(),
            IsSubmitted: true,
        };

        if (data?.POS_StockRequestID) {
            stockData.POS_StockRequestID = data.POS_StockRequestID;
        }
        if (onSubmit) {
            onSubmit(stockData);
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
                            <div className="input-blocks">
                                <label>Ref. Number</label>
                                <input name="RefNumber" required type="text" defaultValue={data?.RefNumber} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>From Debtor</label>
                                <select name="FK_FromDebtorID" className="form-select" required defaultValue={data?.FK_FromDebtorID || ''}>
                                    <option value="">Please select..</option>
                                    {debtors.map((item, index) => (
                                        <option key={index} value={item.DebtorID}>
                                            {item.ShortCode} {item.Name}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>To Debtor</label>
                                <select name="FK_ToDebtorID" className="form-select" required defaultValue={data?.FK_ToDebtorID || ''}>
                                    <option value="">Please select..</option>
                                    {debtors.map((item, index) => (
                                        <option key={index} value={item.DebtorID}>
                                            {item.ShortCode} {item.Name}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Notes</label>
                                <textarea rows={3} name="Notes" className="form-control" defaultValue={data?.Notes}></textarea>
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

export default StockRequestForm;


StockRequestForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
