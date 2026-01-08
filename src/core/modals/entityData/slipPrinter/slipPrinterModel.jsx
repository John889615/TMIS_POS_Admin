import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const SlipPrinterForm = ({ branchList,
    onSubmit,
    showModel,
    handleClose,
    data,
    costCenterList,
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

        const payload = {
            DebtorID: form.DebtorID.value ? parseInt(form.DebtorID.value) : 0,
            CostCenterID: form.CostCenterID.value ? parseInt(form.CostCenterID.value) : 0,
            Name: form.Name.value.trim(),
            Model: form.Model.value.trim(),
            IpAddress: form.IpAddress.value.trim(),
            Port: form.Port.value ? parseInt(form.Port.value) : 0,
            IsDefault: form.IsDefault.checked,
            IsActive: form.IsActive.checked
        };

        if (data?.SlipPrinterID) {
            payload.SlipPrinterID = data.SlipPrinterID;
        }

        if (onSubmit) {
            onSubmit(payload);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Slip Printer</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Debtor</label>
                                <select name="DebtorID" className="form-select" defaultValue={data?.DebtorID || ""}>
                                    <option value="">Select Debtor</option>
                                    {debtorList.map((item) => (
                                        <option key={item.DebtorID} value={item.DebtorID}>
                                            {item.Name}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Cost Center</label>
                                <select name="CostCenterID" className="form-select" defaultValue={data?.CostCenterID || ""}>
                                    <option value="">Select Cost Center</option>
                                    {costCenterList.map((item) => (
                                        <option key={item.CostCenterID} value={item.CostCenterID}>
                                            {item.Name}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Name</label>
                                <input name="Name" type="text" required defaultValue={data?.Name || ""} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Model</label>
                                <input name="Model" type="text" required defaultValue={data?.Model || ""} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>IP Address</label>
                                <input name="IpAddress" type="text" required defaultValue={data?.IpAddress || ""} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Port</label>
                                <input name="Port" type="number" required defaultValue={data?.Port || ""} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>
                                    <input name="IsDefault" type="checkbox" defaultChecked={data?.IsDefault ?? true} /> Is Default
                                </label>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>
                                    <input name="IsActive" type="checkbox" defaultChecked={data?.IsActive ?? true} /> Is Active
                                </label>
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

export default SlipPrinterForm;


SlipPrinterForm.propTypes = {
    data: PropTypes.object,
    costCenterList: PropTypes.array.isRequired,
    debtorList: PropTypes.array.isRequired,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
