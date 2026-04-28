import React, { useEffect, useState } from 'react';
import { Modal, Toast, Button } from 'react-bootstrap';
import PropTypes from 'prop-types';
import { OverlayTrigger, Tooltip } from 'react-bootstrap'


const AssignRoleToUser = ({ show, onHide, roleList, onSave, onDeleteRole }) => {
    const [showDeleteRoleToast, setShowDeleteRoleToast] = useState(false);
    
    useEffect(() => {
        const timeoutId = setTimeout(() => {
            setShowDeleteRoleToast(false);
        }, 6000);
        return () => clearTimeout(timeoutId);
    }, [roleList, showDeleteRoleToast]);

    const AssignRole = (roleId) => {
        onSave(roleId);
    };

    const handleDeleteRoleToastClose = () => {
        setShowDeleteRoleToast(false);
    };

    const handleDeleteRole = (userRoleroleId) => {
        if (userRoleroleId === null) {
            setShowDeleteRoleToast(true);
            return;
        }
        onDeleteRole(userRoleroleId);
    };

    return (
        <Modal show={show} onHide={onHide} centered size="lg" className="custom-modal-two">
            <Modal.Header closeButton className="custom-modal-header border-0">
                <Modal.Title>User Roles</Modal.Title>
            </Modal.Header>

            <Modal.Body className="custom-modal-body">
                <div className="table-responsive">
                    <table className="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Assigned</th>
                                <th>Name</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                        <tbody>
                            {roleList && roleList.length > 0 ? (
                                roleList.map((role, index) => (
                                    <tr key={index}>
                                        <td>
                                            {role.UserRoleIsActive ? (
                                                <i className="bi bi-check-circle-fill text-success"></i>
                                            ) : (
                                                <OverlayTrigger
                                                    placement="top"
                                                    overlay={<Tooltip id="tooltip-top">Assign Role</Tooltip>}
                                                >
                                                    <i className="bi bi-x-circle-fill text-danger"
                                                        style={{ cursor: 'pointer' }}
                                                        onClick={() => AssignRole(role.ApplicationTenantRoleID)}
                                                    ></i>
                                                </OverlayTrigger>

                                            )}
                                        </td>
                                        <td>{role.Role}</td>
                                        <td>
                                            <button
                                                type="button"
                                                className="btn btn-sm btn-link text-danger"
                                                onClick={() => handleDeleteRole(role.UserRoleID)}
                                            >
                                                <i className="bi bi-x-lg"></i>
                                            </button>
                                        </td>
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan="3" className="text-center">
                                        No roles available.
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
                <div className="toast-container position-fixed top-0 end-0 p-3">
                    <Toast
                        show={showDeleteRoleToast}
                        onClose={handleDeleteRoleToastClose}
                        id="dangerToast"
                        className="colored-toast bg-danger-transparent"
                        role="alert"
                        aria-live="assertive"
                        aria-atomic="true"
                    >
                        <Toast.Header closeButton className="bg-danger text-fixed-white">
                            <strong className="me-auto">Error</strong>
                            <Button
                                variant="close"
                                onClick={handleDeleteRoleToastClose}
                                aria-label="Close"
                            />
                        </Toast.Header>
                        <Toast.Body>
                            Please Assign role before delete.
                        </Toast.Body>
                    </Toast>
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
            </Modal.Footer>
        </Modal>
    );
};

export default AssignRoleToUser;

AssignRoleToUser.propTypes = {
    roleList: PropTypes.array.isRequired,
    onSave: PropTypes.func.isRequired,
    onDeleteRole: PropTypes.func.isRequired,
    show: PropTypes.bool.isRequired,
    onHide: PropTypes.func.isRequired,
};

