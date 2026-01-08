import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const ExtraCategoryForm = ({
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

        const categoryData = {
            Category: form.Category.value.trim(),
            DisplayOrder: form.DisplayOrder.value ? parseInt(form.DisplayOrder.value) : 0
        };

        if (data?.ProductExtraCategoryID) {
            categoryData.ProductExtraCategoryID = data.ProductExtraCategoryID;
        }

        if (onSubmit) {
            onSubmit(categoryData);
        }
    };

    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Category</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Category Name</label>
                                <input name="Category" required type="text" defaultValue={data?.Category} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Display Order</label>
                                <input name="DisplayOrder" type="number" className="form-control" defaultValue={data?.DisplayOrder || 0} />
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

export default ExtraCategoryForm;


ExtraCategoryForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
