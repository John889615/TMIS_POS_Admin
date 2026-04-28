import { PlusCircle } from 'feather-icons-react/build/IconComponents'
import React, { useState, useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";



const AddUsers = ({ roleList, onSubmitUser, showModel, handleClose, userData }) => {
    const formRef = useRef(null);

    useEffect(() => {
        if (showModel && formRef.current) {
            formRef.current.reset(); // Reset form each time modal opens
        }
    }, [showModel]);

    const [showPassword, setShowPassword] = useState(false);
    const handleTogglePassword = () => {
        setShowPassword((prevShowPassword) => !prevShowPassword);
    };
    const [showConfirmPassword, setConfirmPassword] = useState(false);
    const handleToggleConfirmPassword = () => {
        setConfirmPassword((prevShowPassword) => !prevShowPassword);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;

        const password = form.password.value;
        const confirmPassword = form.confirmPassword.value;

        if (password !== confirmPassword) {
            // Set a custom validation message
            form.confirmPassword.setCustomValidity("Passwords do not match");
            form.confirmPassword.reportValidity(); // Show browser error tooltip
            return;
        } else {
            // Clear previous validation message if match is fixed
            form.confirmPassword.setCustomValidity("");
        }

        const userData = {
            // collect values from inputs here
            FirstName: form.firstName.value.trim(),
            LastName: form.lastName.value.trim(),
            Username: form.username.value.trim(),
            Email: form.email.value.trim(),
            FK_RoleID: form.role.value,
            Password: form.password.value,
            FK_TenentID: 2,
            ExternalAuthID: null,
            ExternalAuthProvider: null
        };

        if (onSubmitUser) {
            onSubmitUser(userData);
        }
    };

    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Add User</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="new-employee-field">
                                <span>Avatar</span>
                                <div className="profile-pic-upload mb-2">
                                    <div className="profile-pic">
                                        <span>
                                            <PlusCircle className="plus-down-add" />
                                            Profile Photo
                                        </span>
                                    </div>
                                    <div className="input-blocks mb-0">
                                        <div className="image-upload mb-0">
                                            <input type="file" />
                                            <div className="image-uploads">
                                                <h4>Change Image</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>First Name</label>
                                <input name="firstName" required type="text" defaultValue={userData?.FirstName} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Last Name</label>
                                <input name="lastName" type="text" defaultValue={userData?.LastName} className="form-control" required />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>User Name</label>
                                <input name="username" type="text" defaultValue={userData?.Username} className="form-control" required />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Email</label>
                                <input name="email" type="email" defaultValue={userData?.Email} className="form-control" required />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Role</label>
                                <select name="role" className="form-select" required>
                                    {roleList.map((role, index) => (
                                        <option key={index} value={role.ApplicationTenantRoleID}>
                                            {role.Role}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Password</label>
                                <div className="pass-group">
                                    <input
                                        name="password"
                                        required
                                        type={showPassword ? 'text' : 'password'}
                                        className="pass-input"
                                        placeholder="Enter your password"
                                    />
                                    <span
                                        className={`fas toggle-password ${showPassword ? 'fa-eye' : 'fa-eye-slash'}`}
                                        onClick={handleTogglePassword}
                                        style={{ cursor: 'pointer' }}
                                    />
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Confirm Password</label>
                                <div className="pass-group">
                                    <input
                                        type={showConfirmPassword ? 'text' : 'password'}
                                        className="pass-input"
                                        placeholder="Enter your password"
                                        required
                                        name="confirmPassword"
                                    />
                                    <span
                                        className={`fas toggle-password ${showConfirmPassword ? 'fa-eye' : 'fa-eye-slash'}`}
                                        onClick={handleToggleConfirmPassword}
                                        style={{ cursor: 'pointer' }}
                                    />
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

export default AddUsers;


AddUsers.propTypes = {
    userData: PropTypes.object,
    roleList: PropTypes.array.isRequired,
    onSubmitUser: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
