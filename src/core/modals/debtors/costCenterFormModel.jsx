import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const CostCenterForm = ({ costTypeList,
    onSubmitCostCenter,
    showModel,
    handleClose,
    data,
    statusList,
    debtorList
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

        const costCenterData = {
            Name: form.Name.value.trim(),
            BillingReference: form.BillingReference.value.trim(),
            FK_CostCenterTypeID: parseInt(form.FK_CostCenterTypeID.value) || 0,
            FK_StatusID: parseInt(form.FK_StatusID.value) || 0,
            FK_DebtorID: parseInt(form.FK_DebtorID.value) || 0,
        };

        // If updating, include the POS_CostCenterID
        if (data?.CostCenterID) {
            costCenterData.POS_CostCenterID = data.CostCenterID;
        }

        if (onSubmitCostCenter) {
            onSubmitCostCenter(costCenterData);
        }
    };

    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Cost Center</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Name</label>
                                <input name="Name" type="text" defaultValue={data?.Name} className="form-control" required />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Billing Reference</label>
                                <input name="BillingReference" required type="text" defaultValue={data?.BillingReference} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Debtor</label>
                                <select name="FK_DebtorID" className="form-select" required defaultValue={data?.DebtorID}>
                                    <option value="">Please select..</option>
                                    {debtorList.map((item, index) => (
                                        <option key={index} value={item.DebtorID}>
                                            {item.Name + " / " + item.ShortCode}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Types</label>
                                <select name="FK_CostCenterTypeID" className="form-select" required defaultValue={data?.CostCenterTypeID}>
                                    <option value="">Please select..</option>
                                    {costTypeList.map((item, index) => (
                                        <option key={index} value={item.POS_CostCenterTypeID}>
                                            {item.Name}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Status</label>
                                <select name="FK_StatusID" required className="form-select" defaultValue={data?.StatusID}>
                                    <option value="">Please select..</option>
                                    {statusList.map((item, index) => (
                                        <option key={index} value={item.StatusID}>
                                            {item.DisplayName}
                                        </option>
                                    ))}
                                </select>
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
}

export default CostCenterForm;


CostCenterForm.propTypes = {
    data: PropTypes.object,
    costTypeList: PropTypes.array.isRequired,
    debtorList: PropTypes.array.isRequired,
    statusList: PropTypes.array.isRequired,
    onSubmitCostCenter: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
