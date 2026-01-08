import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const AddressTypeForm = ({ branchList,
    onSubmit,
    showModel,
    handleClose,
    data,
    entitiesList
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


        const record = {
            FK_EntityID: Number(form.FK_EntityID.value),
            Type: form.Type.value.trim(),
            IsRequired: form.IsRequired.checked,
            CanEdit: form.CanEdit.checked,
        };

        if (data?.AddressTypeID) {
            record.AddressTypeID = data?.AddressTypeID;
        }

        if (onSubmit) {
            onSubmit(record);
        }
    };

    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>New Address Type</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Entities</label>
                                <select name="FK_EntityID" required className="form-select" defaultValue={data?.FK_EntityID || ""}>
                                    <option value="">Select Type</option>
                                    {entitiesList.map(e => (
                                        <option key={e.EntityID} value={e.EntityID}>{e.Name}</option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Type</label>
                                <input name="Type" required type="text" defaultValue={data?.Type} className="form-control" />
                            </div>
                        </div>
                        <div className="row mt-3">
                            <div className="col-lg-6">
                                <div className="input-blocks form-check">
                                    <input
                                        type="checkbox"
                                        name="IsRequired"
                                        defaultChecked={data?.IsRequired}
                                        className="form-check-input"
                                        id="IsRequired"
                                    />
                                    <label className="form-check-label" htmlFor="IsRequired">
                                        Is Required?
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div className="row mt-3">
                            <div className="col-lg-6">
                                <div className="input-blocks form-check">
                                    <input
                                        type="checkbox"
                                        name="CanEdit"
                                        defaultChecked={data?.CanEdit}
                                        className="form-check-input"
                                        id="CanEdit"
                                    />
                                    <label className="form-check-label" htmlFor="CanEdit">
                                        Can Edit?
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

export default AddressTypeForm;


AddressTypeForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    entitiesList: PropTypes.array.isRequired,
};
