import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const DebtorForm = ({ currencyList, 
    branchList,
    onSubmitDebtor,
    showModel,
    handleClose,
    debtorData,
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


        const data = {
            ShortCode: form.ShortCode.value.trim(),
            Name: form.Name.value.trim(),
            FK_CurrencyID: parseInt(form.Currency.value, 10),
            IsActive: form.IsActive.checked,
        };

        if (debtorData?.DebtorID) {
            data.DebtorID = debtorData.DebtorID;
        }

        if (onSubmitDebtor) {
            onSubmitDebtor(data);
        }
    };



    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Location</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Short Code</label>
                                <input name="ShortCode" required type="text" defaultValue={debtorData?.ShortCode} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Name</label>
                                <input name="Name" type="text" defaultValue={debtorData?.Name} className="form-control" required />
                            </div>
                        </div>
                        
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Currency</label>
                                  <select name="Currency" className="form-select" required>
                                    <option value="">Please select..</option>
                                    console.log(currencyList);
                                    {currencyList.map((item, index) => (
                                        <option key={index} value={item.CurrencyID}>
                                            {item.Name}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="row mt-3">
                            <div className="col-lg-6">
                                <div className="input-blocks form-check">
                                    <input
                                        type="checkbox"
                                        name="IsActive"
                                        defaultChecked={debtorData?.IsActive}
                                        className="form-check-input"
                                        id="isActive"
                                    />
                                    <label className="form-check-label" htmlFor="isActive">
                                        Is Active?
                                    </label>
                                </div>
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

export default DebtorForm;


DebtorForm.propTypes = {
    debtorData: PropTypes.object,
    onSubmitDebtor: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
