import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const MenuForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
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
        const menuData = {
            IsActive: form.IsActive.checked,
            MenuName: form.MenuName.value.trim(),
            ImageFile: form.ImageFile.files[0] || null
        };
        if (data?.POS_MenuID) {
            menuData.POS_MenuID = data.POS_MenuID;
        }
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
                                <label>Menu Name</label>
                                <input name="MenuName" required type="text" defaultValue={data?.MenuName} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="form-check mb-3">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    id="IsActive"
                                    name="IsActive"
                                    defaultChecked={data?.IsActive || false}
                                />
                                <label className="form-check-label" htmlFor="IsActive">
                                    Is Active
                                </label>
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Menu Image</label>
                                <input
                                    name="ImageFile"
                                    type="file"
                                    accept="image/*"
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

export default MenuForm;


MenuForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
