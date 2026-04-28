import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const ProductTypeForm = ({
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

        const productTypeData = {
            ProductType: form.ProductType.value.trim(),
            IsInventory: form.IsInventory.checked,
            IsManufactured: form.IsManufactured.checked,
            IsService: form.IsService.checked,
            IsComposite: form.IsComposite.checked,
        };

        // Add ID for update case
        if (data?.POS_ProductTypeID) {
            productTypeData.POS_ProductTypeID = data.POS_ProductTypeID;
        }

        if (onSubmit) {
            onSubmit(productTypeData);
        }
    };

    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Product Type</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Product Type</label>
                                <input name="ProductType" required type="text" defaultValue={data?.ProductType} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="form-check mt-4">
                                <input type="checkbox" className="form-check-input" name="IsInventory" defaultChecked={data?.IsInventory} />
                                <label className="form-check-label">Inventory</label>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="form-check mt-4">
                                <input type="checkbox" className="form-check-input" name="IsManufactured" defaultChecked={data?.IsManufactured} />
                                <label className="form-check-label">Manufactured</label>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="form-check mt-4">
                                <input type="checkbox" className="form-check-input" name="IsService" defaultChecked={data?.IsService} />
                                <label className="form-check-label">Service</label>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="form-check mt-4">
                                <input type="checkbox" className="form-check-input" name="IsComposite" defaultChecked={data?.IsComposite} />
                                <label className="form-check-label">Composite</label>
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

export default ProductTypeForm;


ProductTypeForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
