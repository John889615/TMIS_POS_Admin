import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";
import { useSelector } from 'react-redux';

const CostCenterProductForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    productList,
    taxList,
    sellUnitList,
    costCenterId
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
            FK_ProductID: form.FK_ProductID.value ? parseInt(form.FK_ProductID.value) : 0,
            FK_CostCenterID: costCenterId == null ? 0 : parseInt(costCenterId),
            FK_TaxTypeID: form.FK_TaxTypeID.value ? parseInt(form.FK_TaxTypeID.value) : 0,
            Value: form.Value.value ? parseFloat(form.Value.value) : 0,
            Vat: form.Vat.value ? parseFloat(form.Vat.value) : 0,
            ItemPrice: form.ItemPrice.value ? parseFloat(form.ItemPrice.value) : 0,
            FK_SellUnitID: form.FK_SellUnitID.value ? parseInt(form.FK_SellUnitID.value) : 0,
            QuantityOnHand: form.QuantityOnHand.value ? parseInt(form.QuantityOnHand.value) : 0,
            IsAvailable: !!form.IsAvailable.checked,
            IsActive: !!form.IsActive.checked,
        };

        if (data?.POS_CostCenterProductID) {
            purchaseData.POS_CostCenterProductID = data.POS_CostCenterProductID;
        }

        if (onSubmit) {
            onSubmit(purchaseData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Cost Center Product</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Products</label>
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
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Tax Type</label>
                                <select name="FK_TaxTypeID" required className="form-select" defaultValue={data?.FK_TaxTypeID || ''}>
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
                                <label>Value</label>
                                <input name="Value" required type="number" defaultValue={data?.Value} className="form-control" />
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
                                <label>Item Price</label>
                                <input name="ItemPrice" required type="number" defaultValue={data?.ItemPrice} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Sell Unit</label>
                                <select name="FK_SellUnitID" className="form-select" defaultValue={data?.FK_SellUnitID || ''}>
                                    <option value="">Please select..</option>
                                    {sellUnitList.map((item, index) => (
                                        <option key={index} value={item.UnitID}>
                                            {item.Unit} / {item.Symbol}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Quantity On Hand</label>
                                <input name="QuantityOnHand" type="number" defaultValue={data?.QuantityOnHand} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks form-check mt-3">
                                <input
                                    type="checkbox"
                                    id="IsAvailable"
                                    name="IsAvailable"
                                    className="form-check-input"
                                    defaultChecked={typeof data?.IsAvailable === 'boolean' ? data.IsAvailable : true}
                                />
                                <label className="form-check-label" htmlFor="IsAvailable">Is Available</label>
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

export default CostCenterProductForm;


CostCenterProductForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    productList: PropTypes.array.isRequired,
    taxList: PropTypes.array.isRequired,
    sellUnitList: PropTypes.array.isRequired,
    costCenterId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
};
