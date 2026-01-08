import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";
import { useSelector } from 'react-redux';

const PurchaseOrderForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    costCenterList,
}) => {
    const formRef = useRef(null);
    const debtorId = useSelector((state) => state.selectedDebtorStore);
    useEffect(() => {
        if (showModel && formRef.current) {
            formRef.current.reset(); // Reset form each time modal opens
        }
    }, [showModel]);


    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;

        const purchaseData = {
            OrderNumber: form.OrderNumber?.value?.trim(),
            FK_SupplierID: form.FK_SupplierID.value ? parseInt(form.FK_SupplierID.value) : null,
            FK_CostCenterID: form.FK_CostCenterID.value ? parseInt(form.FK_CostCenterID.value) : null,
            FK_DebtorID: debtorId == null ? 1 : parseInt(debtorId),
            Notes: form.Notes.value.trim(),
            IsSubmitted: true,
        };

        if (data?.POS_PurchaseOrderID) {
            purchaseData.POS_PurchaseOrderID = data.POS_PurchaseOrderID;
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
                            <div className="input-blocks">
                                <label>Order Name</label>
                                <input name="OrderNumber" required type="text" defaultValue={data?.OrderNumber} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Supplier</label>
                                <select name="FK_SupplierID" className="form-select" required defaultValue={data?.SupplierID || ''}>
                                    <option value="">Please select..</option>
                                    <option value="1">Testing</option>
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Cost Center</label>
                                <select name="FK_CostCenterID" className="form-select" defaultValue={data?.CostCenterID || ''}>
                                    <option value="">Please select..</option>
                                    {costCenterList.map((item, index) => (
                                        <option key={index} value={item.CostCenterID}>
                                            {item.Name}
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

export default PurchaseOrderForm;


PurchaseOrderForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    costCenterList: PropTypes.array.isRequired,
};
