import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const CreditorForm = ({ creditorTypeList,
    onSubmitCreditor,
    showModel,
    handleClose,
    data,
    statusList,
    creditorList
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

        const FK_MasterCreditorID = form.FK_MasterCreditorID.value || null;

        const creditorData = {
            Name: form.Name.value.trim(),
            ShortCode: form.ShortCode.value.trim(),
            FK_CreditorTypeID: parseInt(form.FK_CreditorTypeID.value) || 0,
            FK_StatusID: parseInt(form.FK_StatusID.value) || 0,
            IsMasterCreditor: !FK_MasterCreditorID,
            FK_MasterCreditorID: FK_MasterCreditorID ? parseInt(FK_MasterCreditorID) : null,
        };

        // If updating, include the POS_CostCenterID
        // if (data?.CostCenterID) {
        //     costCenterData.POS_CostCenterID = data.CostCenterID;
        // }

        if (onSubmitCreditor) {
            onSubmitCreditor(creditorData);
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
                                <label>Short Code</label>
                                <input name="ShortCode" type="text" defaultValue={data?.ShortCode} className="form-control" required />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Name</label>
                                <input name="Name" required type="text" defaultValue={data?.Name} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Creditor</label>
                                <select name="FK_MasterCreditorID" className="form-select" defaultValue={data?.FK_MasterCreditorID}>
                                    <option value="">Please select..</option>
                                    {creditorList.map((item, index) => (
                                        <option key={index} value={item.CreditorID}>
                                            {item.Name + " / " + item.ShortCode}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Types</label>
                                <select name="FK_CreditorTypeID" className="form-select" required defaultValue={data?.FK_CreditorTypeID}>
                                    <option value="">Please select..</option>
                                    {creditorTypeList.map((item, index) => (
                                        <option key={index} value={item.CreditorTypeID}>
                                            {item.Type}
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

export default CreditorForm;


CreditorForm.propTypes = {
    data: PropTypes.object,
    creditorTypeList: PropTypes.array.isRequired,
    creditorList: PropTypes.array.isRequired,
    statusList: PropTypes.array.isRequired,
    onSubmitCreditor: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
