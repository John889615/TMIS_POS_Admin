import React, { useEffect, useRef, useState } from 'react';
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";
import Select from 'react-select';

const DebtorForm = ({
    currencyList,
    onSubmitDebtor,
    showModel,
    handleClose,
    debtorData,
}) => {
    const formRef = useRef(null);
    const [selectedCurrency, setSelectedCurrency] = useState(null);

    // Build currency options
    const currencyOptions = currencyList.map(item => ({
        value: item.CurrencyID,
        label: `${item.Name}${item.Code ? ` (${item.Code})` : ''}`,
    }));

    // Reset form helper
    const resetForm = () => {
        formRef.current?.reset();
        setSelectedCurrency(null);
    };

    // Handle modal open / edit mode
    useEffect(() => {
        if (showModel) {
            if (debtorData?.FK_CurrencyID) {
                const existingCurrency = currencyOptions.find(
                    c => c.value === debtorData.FK_CurrencyID
                );
                setSelectedCurrency(existingCurrency || null);
            } else {
                resetForm();
            }
        } else {
            resetForm();
        }
    }, [showModel, debtorData]);

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!selectedCurrency) return;

        const form = e.target;

        const data = {
            ShortCode: form.ShortCode.value.trim(),
            Name: form.Name.value.trim(),
            FK_CurrencyID: selectedCurrency.value,
            IsActive: form.IsActive.checked,
        };

        if (debtorData?.DebtorID) {
            data.DebtorID = debtorData.DebtorID;
        }

        await onSubmitDebtor(data);

        // ✅ Clear after successful submit
        resetForm();
    };

    return (
        <Modal
            show={showModel}
            onHide={handleClose}
            centered
            dialogClassName="custom-modal-two"
        >
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Location</Modal.Title>
                </Modal.Header>

                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Short Code</label>
                                <input
                                    name="ShortCode"
                                    type="text"
                                    className="form-control"
                                    defaultValue={debtorData?.ShortCode || ''}
                                    required
                                />
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Name</label>
                                <input
                                    name="Name"
                                    type="text"
                                    className="form-control"
                                    defaultValue={debtorData?.Name || ''}
                                    required
                                />
                            </div>
                        </div>

                        <div className="col-lg-6 mt-3">
                            <div className="input-blocks">
                                <label>Currency</label>
                                <Select
                                    options={currencyOptions}
                                    value={selectedCurrency}
                                    onChange={setSelectedCurrency}
                                    placeholder="Search currency..."
                                    isClearable
                                    classNamePrefix="react-select"
                                />
                            </div>
                        </div>

                        <div className="col-lg-6 mt-4">
                            <div className="input-blocks form-check">
                                <input
                                    type="checkbox"
                                    name="IsActive"
                                    defaultChecked={debtorData?.IsActive ?? true}
                                    className="form-check-input"
                                    id="isActive"
                                />
                                <label
                                    className="form-check-label"
                                    htmlFor="isActive"
                                >
                                    Is Active?
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
};

DebtorForm.propTypes = {
    currencyList: PropTypes.array.isRequired,
    debtorData: PropTypes.object,
    onSubmitDebtor: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};

export default DebtorForm;
