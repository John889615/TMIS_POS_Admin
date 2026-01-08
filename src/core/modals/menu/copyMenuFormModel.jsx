import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";
import { useSelector } from 'react-redux';


const CopyMenuForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    debtorList,
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
        const menuData = {
            SourceMenuID: data.MenuID,
            TargetDebtorID: parseFloat(form.TargetDebtorID.value) || 0,
            TargetCostCenterID: parseFloat(form.TargetCostCenterID.value) || 0,
            DefaultSlipPrinterID: 1,
            Override: true
        };


        if (onSubmit) {
            onSubmit(menuData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Menu</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Debtor</label>
                                <select
                                    name="TargetDebtorID"
                                    required
                                    className="form-select"
                                    defaultValue={debtorId || data?.TargetDebtorID || ""}
                                >
                                    <option value="">Select Debtor..</option>
                                    {debtorList?.map((item, idx) => (
                                        <option key={idx} value={item.DebtorID}>
                                            {item.Name} / {item.ShortCode}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Cost Center</label>
                                <select name="TargetCostCenterID" required className="form-select" defaultValue={data?.TargetCostCenterID || ''}>
                                    <option value="">Select Cost Center..</option>
                                    {costCenterList?.map((item, idx) => (
                                        <option key={idx} value={item.CostCenterID}>
                                            {item.Name}
                                        </option>
                                    ))}
                                </select>
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

export default CopyMenuForm;


CopyMenuForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    debtorList: PropTypes.array.isRequired,
    costCenterList: PropTypes.array.isRequired,
};
