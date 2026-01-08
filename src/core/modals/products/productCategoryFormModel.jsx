import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const ProductCategoryForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    categoryList
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

        const FK_ProductCategoryID = form.FK_ProductCategoryID.value || null;


        const categoryData = {
            CategoryName: form.CategoryName.value.trim(),
            FK_ProductCategoryID: FK_ProductCategoryID ? parseInt(FK_ProductCategoryID) : null,
            IsMaster: !FK_ProductCategoryID,
        };

        if (data?.POS_ProductCategoryID) {
            categoryData.POS_ProductCategoryID = data.POS_ProductCategoryID;
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
                                <input name="CategoryName" required type="text" defaultValue={data?.CategoryName} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Category</label>
                                <select name="FK_ProductCategoryID" className="form-select" defaultValue={data?.FK_ProductCategoryID}>
                                    <option value="">Please select..</option>
                                    {categoryList.map((item, index) => (
                                        <option key={index} value={item.POS_ProductCategoryID}>
                                            {item.CategoryName}
                                        </option>
                                    ))}
                                </select>
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

export default ProductCategoryForm;


ProductCategoryForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    categoryList: PropTypes.array.isRequired,
};
