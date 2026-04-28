import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const MenuItemProductForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    menuItemList,
    productList
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

        const menuItemData = {
            FK_MenuItemID: form.FK_MenuItemID.value
                ? parseInt(form.FK_MenuItemID.value)
                : null,
            FK_ProductID: form.FK_ProductID.value
                ? parseInt(form.FK_ProductID.value)
                : null,
        };

        if (onSubmit) {
            onSubmit(menuItemData); // 🔄 Pass wrapped payload
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Menu Item</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Menu Item</label>
                                <select name="FK_MenuItemID" className="form-select" defaultValue={data?.FK_MenuItemID || ''}>
                                    <option value="">Please select..</option>
                                    {menuItemList.map((item, index) => (
                                        <option key={index} value={item.POS_MenuItemID}>
                                            {item.Item}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Product</label>
                                <select name="FK_ProductID" className="form-select" required defaultValue={data?.FK_ProductID || ''}>
                                    <option value="">Please select..</option>
                                    {productList.map((item, index) => (
                                        <option key={index} value={item.POS_ProductID}>
                                            {item.ProductName}
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

export default MenuItemProductForm;


MenuItemProductForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    menuItemList: PropTypes.array.isRequired,
    productList: PropTypes.array.isRequired,
};
