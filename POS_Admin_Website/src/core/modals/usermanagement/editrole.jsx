import React, { useEffect, useRef } from 'react';
import { Modal } from 'react-bootstrap';
import PropTypes from 'prop-types';

const EditRole = ({ show, onHide, roleData, onSave }) => {
    const formRef = useRef(null);
    useEffect(() => {

    }, [roleData]);



    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;


        const userData = {
            // collect values from inputs here
            RoleID: roleData?.ApplicationTenantRoleID,
            Role: form.Role.value.trim(),
            Description: form.Description.value.trim(),
        };

        if (onSave) {
            onSave(userData);
        }
    };


    return (
        <Modal show={show} onHide={onHide} centered className="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Edit Role</Modal.Title>
                </Modal.Header>

                <Modal.Body className="custom-modal-body">
                    <div className='row'>
                        <div className='col-12'>
                            <div className="input-blocks">
                                <label>Role Name</label>
                                <input name="Role" required type="text" defaultValue={roleData?.Role} className="form-control" />
                            </div>
                        </div>
                        <div className='col-12'>
                            <div className="input-blocks">
                                <label>Description</label>
                                <textarea className="form-control" name="Description" defaultValue={roleData?.Description}></textarea>
                            </div>
                        </div>
                    </div>

                </Modal.Body>

                <Modal.Footer className="modal-footer-btn">
                    <button
                        type="button"
                        className="btn btn-cancel me-2"
                        onClick={onHide}
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

export default EditRole;

EditRole.propTypes = {
    roleData: PropTypes.object.isRequired,
    onSave: PropTypes.func.isRequired,
    show: PropTypes.bool.isRequired,
    onHide: PropTypes.func.isRequired,
};

