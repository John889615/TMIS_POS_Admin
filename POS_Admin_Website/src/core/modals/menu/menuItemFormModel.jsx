import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const MenuItemForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    menuItemList
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
            Item: form.Item.value.trim(),
            Description: form.Description.value.trim(),
            FK_POS_MenuItemID: form.FK_POS_MenuItemID.value
                ? parseInt(form.FK_POS_MenuItemID.value)
                : null,
        };

        if (data?.POS_MenuItemID) {
            menuItemData.POS_MenuItemID = data.POS_MenuItemID;
        }

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
                                <label>Item</label>
                                <input name="Item" required type="text" defaultValue={data?.Item} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Parent Menu Item</label>
                                <select name="FK_POS_MenuItemID" className="form-select" defaultValue={data?.FK_ParentMenuItemID || ''}>
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
                                <label>Description</label>
                                <textarea rows={3} name="Description" className="form-control" defaultValue={data?.Description}></textarea>
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

export default MenuItemForm;


MenuItemForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    menuItemList: PropTypes.array.isRequired,
};
