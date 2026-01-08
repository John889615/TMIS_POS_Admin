import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const StockRequestLineForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    productList,
    stockRequestList
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

        const stockDataLine = {
            POS_StockRequestLineID: null, // Use 0 instead of null if API requires number
            FK_StockRequestID: form.FK_StockRequestID.value
                ? parseInt(form.FK_StockRequestID.value)
                : 0,
            FK_ProductID: form.FK_ProductID.value
                ? parseInt(form.FK_ProductID.value)
                : 0,
            Quantity: form.Quantity.value ? parseInt(form.Quantity.value) : 0,
            Notes: form.Notes.value.trim(),
        };

        const payload = {
            StockRequestLines: [stockDataLine], // ✅ Wrap in array and key
        };

        if (onSubmit) {
            onSubmit(payload); // 🔄 Pass wrapped payload
        }
    };



    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Purchase Order Line</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Quantity</label>
                                <input name="Quantity" required type="number" defaultValue={data?.Quantity} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Stock Request</label>
                                <select name="FK_StockRequestID" required className="form-select" defaultValue={data?.FK_StockRequestID || ''}>
                                    <option value="">Please select..</option>
                                    {stockRequestList.map((item, index) => (
                                        <option key={index} value={item.POS_StockRequestID}>
                                            {item.RefNumber}
                                        </option>
                                    ))}

                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Product</label>
                                <select name="FK_ProductID" required className="form-select" defaultValue={data?.FK_ProductID || ''}>
                                    <option value="">Please select..</option>
                                    {productList.map((item, index) => (
                                        <option key={index} value={item.POS_ProductID}>
                                            {item.ProductName}
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

export default StockRequestLineForm;


StockRequestLineForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    productList: PropTypes.array.isRequired,
    stockRequestList: PropTypes.array.isRequired,
};
