import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";
import { useSelector } from 'react-redux';

const DebtorProductPriceForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    priceCodeList,
    taxList,
    id
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

        const parseDateToISOString = (fieldName) => {
            const value = form[fieldName]?.value;
            if (!value) return null; // no value supplied
            const d = new Date(value);
            return isNaN(d.getTime()) ? null : d.toISOString();
        };

        const purchaseData = {
            FK_DebtorProductID: parseFloat(id),
            FK_PriceCodeID: form.FK_PriceCodeID.value ? parseInt(form.FK_PriceCodeID.value) : 0,
            FK_TaxID: form.FK_TaxID.value ? parseInt(form.FK_TaxID.value) : 0,
            Vat: form.Vat?.value ? parseFloat(form.Vat.value) : 0,
            Inclusive: form.Inclusive.checked,
            IsActive: form.IsActive.checked,
            StartDate: parseDateToISOString('StartDate'),
            EndDate: parseDateToISOString('EndDate'),
        };

        if (data?.POS_DebtorProductPriceID) {
            purchaseData.POS_DebtorProductPriceID = data.POS_DebtorProductPriceID;
        }

        debugger;
        if (onSubmit) {
            onSubmit(purchaseData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Location Product</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Price Code</label>
                                <select name="FK_PriceCodeID" required className="form-select" defaultValue={data?.FK_PriceCodeID || ''}>
                                    <option value="">Please select..</option>
                                    {priceCodeList.map((item, index) => (
                                        <option key={index} value={item.POS_PriceCodeID}>
                                            {item.PriceCode}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Tax Type</label>
                                <select name="FK_TaxID" required className="form-select" defaultValue={data?.FK_TaxID || ''}>
                                    <option value="">Please select..</option>
                                    {taxList.map((item, index) => (
                                        <option key={index} value={item.TaxTypeID}>
                                            {item.TaxName}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Item Price</label>
                                <input name="ItemPrice" required type="number" defaultValue={data?.ItemPrice} className="form-control" />
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Vat</label>
                                <input name="Vat" type="number" defaultValue={data?.Vat} className="form-control" />
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Start Date</label>
                                <input
                                    name="StartDate"
                                    type="datetime-local"
                                    required
                                    defaultValue={data?.StartDate || ""}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>End Date</label>
                                <input
                                    name="EndDate"
                                    type="datetime-local"
                                    required
                                    defaultValue={data?.EndDate || ""}
                                    className="form-control"
                                />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks mt-4">
                                <div className="form-check">
                                    <input type="checkbox" className="form-check-input" id="Inclusive" name="Inclusive" defaultChecked={data?.Inclusive} />
                                    <label className="form-check-label" htmlFor="Inclusive">Inclusive</label>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks mt-4">
                                <div className="form-check">
                                    <input type="checkbox" className="form-check-input" id="IsActive" name="IsActive" defaultChecked={data?.IsActive} />
                                    <label className="form-check-label" htmlFor="IsActive">Is Active</label>
                                </div>
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

export default DebtorProductPriceForm;


DebtorProductPriceForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    priceCodeList: PropTypes.array.isRequired,
    taxList: PropTypes.array.isRequired,
    id: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string,
    ]),
};
