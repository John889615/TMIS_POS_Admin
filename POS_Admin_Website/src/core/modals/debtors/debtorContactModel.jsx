import React, { useEffect, useRef, useState } from 'react'
import PropTypes, { string } from 'prop-types';
import { Modal } from "react-bootstrap";

const DebtorContactForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    contactTypeList,
    dialogCodeList,
    debtorId
}) => {
    debugger;
    const [selectedContactType, setSelectedContactType] = useState("");
    const [selectedDialingCode, setSelectedDialingCode] = useState("");
    const formRef = useRef(null);

    useEffect(() => {
        if (data) {
            setSelectedContactType(data.FK_ContactTypeID || "");
            setSelectedDialingCode(data.FK_DialingCodeID || "");
        }
    }, [data]);

    useEffect(() => {
        if (showModel && formRef.current) {
            formRef.current.reset();
        }
    }, [showModel]);

    const handleContactTypeChange = (e) => {
        setSelectedContactType(e.target.value);
    };

    const handleDialingCodeChange = (e) => {
        setSelectedDialingCode(e.target.value);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;

        const contactData = {
            ContactValue: form.ContactValue.value.trim(),
            FK_ContactTypeID: Number(form.FK_ContactTypeID.value),
            FK_DialingCodeID: Number(form.FK_DialingCodeID.value),
            IsVerified: form.IsVerified.checked,
            VerificationToken: form.VerificationToken.value.trim(),
            VerifiedAt: form.VerifiedAt.value ? new Date(form.VerifiedAt.value).toISOString() : null,
            Notes: form.Notes.value.trim(),
            FK_DebtorID: debtorId,
            IsPrimary: form.IsPrimary.checked,
            IsMarketing: form.IsMarketing.checked,
            IsEmergency: form.IsEmergency.checked,
            PreferredContactTime: form.PreferredContactTime.value.trim(),
            PreferredLanguageCode: form.PreferredLanguageCode.value.trim(),
            ValidFrom: form.ValidFrom.value ? new Date(form.ValidFrom.value).toISOString() : null,
            ValidTo: form.ValidTo.value ? new Date(form.ValidTo.value).toISOString() : null,
        };

        if (data?.FK_ContactID) {
            contactData.ContactID = data.FK_ContactID;
        }

        if (onSubmit) {
            onSubmit(contactData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Location Address</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        {/* Contact Value */}
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Contact Value</label>
                                <input
                                    name="ContactValue"
                                    required
                                    type="text"
                                    defaultValue={data?.ContactValue}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        {/* Contact Type */}
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Contact Type</label>
                                <select
                                    name="FK_ContactTypeID"
                                    required
                                    className="form-select"
                                    value={selectedContactType}
                                    onChange={handleContactTypeChange}
                                >
                                    <option value="">Select Type</option>
                                    {contactTypeList.map((c) => (
                                        <option key={c.ContactTypeID} value={c.ContactTypeID}>
                                            {c.Type}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        {/* Dialing Code */}
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Dialing Code</label>
                                <select
                                    name="FK_DialingCodeID"
                                    className="form-select"
                                    value={selectedDialingCode}
                                    onChange={handleDialingCodeChange}
                                >
                                    <option value="">Select Dialing Code</option>
                                    {dialogCodeList.map((d) => (
                                        <option key={d.DialingCodeID} value={d.DialingCodeID}>
                                            {d.DialingCode}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        {/* Verification */}
                        <div className="col-lg-6">
                            <div className="form-check mb-3 mt-4">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    id="IsVerified"
                                    name="IsVerified"
                                    defaultChecked={data?.IsVerified || false}
                                />
                                <label className="form-check-label" htmlFor="IsVerified">
                                    Is Verified
                                </label>
                            </div>
                        </div>

                        {/* Verification Token */}
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Verification Token</label>
                                <input
                                    name="VerificationToken"
                                    type="text"
                                    defaultValue={data?.VerificationToken}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        {/* Verified At */}
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Verified At</label>
                                <input
                                    name="VerifiedAt"
                                    type="datetime-local"
                                    defaultValue={data?.VerifiedAt ? data.VerifiedAt.substring(0, 16) : ""}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        {/* Notes */}
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Notes</label>
                                <textarea
                                    name="Notes"
                                    defaultValue={data?.Notes}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        {/* Is Primary */}
                        <div className="col-lg-6">
                            <div className="form-check mb-3 mt-4">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    id="IsPrimary"
                                    name="IsPrimary"
                                    defaultChecked={data?.IsPrimary || false}
                                />
                                <label className="form-check-label" htmlFor="IsPrimary">
                                    Is Primary
                                </label>
                            </div>
                        </div>

                        {/* Is Marketing */}
                        <div className="col-lg-6">
                            <div className="form-check mb-3 mt-4">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    id="IsMarketing"
                                    name="IsMarketing"
                                    defaultChecked={data?.IsMarketing || false}
                                />
                                <label className="form-check-label" htmlFor="IsMarketing">
                                    Is Marketing
                                </label>
                            </div>
                        </div>

                        {/* Is Emergency */}
                        <div className="col-lg-6">
                            <div className="form-check mb-3 mt-4">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    id="IsEmergency"
                                    name="IsEmergency"
                                    defaultChecked={data?.IsEmergency || false}
                                />
                                <label className="form-check-label" htmlFor="IsEmergency">
                                    Is Emergency
                                </label>
                            </div>
                        </div>

                        {/* Preferred Contact Time */}
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Preferred Contact Time</label>
                                <input
                                    name="PreferredContactTime"
                                    type="text"
                                    defaultValue={data?.PreferredContactTime}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        {/* Preferred Language */}
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Preferred Language</label>
                                <input
                                    name="PreferredLanguageCode"
                                    type="text"
                                    defaultValue={data?.PreferredLanguageCode}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        {/* Valid From */}
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Valid From</label>
                                <input
                                    name="ValidFrom"
                                    type="datetime-local"
                                    defaultValue={data?.ValidFrom ? data.ValidFrom.substring(0, 16) : ""}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        {/* Valid To */}
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Valid To</label>
                                <input
                                    name="ValidTo"
                                    type="datetime-local"
                                    defaultValue={data?.ValidTo ? data.ValidTo.substring(0, 16) : ""}
                                    className="form-control"
                                />
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

export default DebtorContactForm;


DebtorContactForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    contactTypeList: PropTypes.array.isRequired,
    dialogCodeList: PropTypes.array.isRequired,
    debtorId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
};
