import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const ProductForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    categoryList,
    typeList,
    unitList
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

        const productData = {
            ProductName: form.ProductName.value.trim(),
            Description: form.Description.value.trim(),
            FK_ProductTypeID: form.FK_ProductTypeID.value ? parseInt(form.FK_ProductTypeID.value) : null,
            IsStockTracked: form.IsStockTracked.checked,
            FK_UnitID: form.FK_UnitID.value ? parseInt(form.FK_UnitID.value) : null,
            FK_DefaultUnitID: form.FK_DefaultUnitID.value ? parseInt(form.FK_DefaultUnitID.value) : null,
            FK_ProductCategoryID: form.FK_ProductCategoryID.value ? parseInt(form.FK_ProductCategoryID.value) : null,
            SKU: form.SKU.value.trim(),
            Barcode: form.Barcode.value.trim(),
            QrCode: form.QrCode.value.trim(),
            ImageFile: form.ImageFile.files[0] || null
        };

        if (data?.POS_ProductID) {
            productData.POS_ProductID = data.POS_ProductID;
        }

        if (onSubmit) {
            onSubmit(productData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Product</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Product Name</label>
                                <input name="ProductName" required type="text" defaultValue={data?.ProductName} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Description</label>
                                <textarea name="Description" defaultValue={data?.Description} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Product Type</label>
                                <select name="FK_ProductTypeID" className="form-select" required defaultValue={data?.FK_ProductTypeID || ''}>
                                    <option value="">Please select..</option>
                                    {typeList.map((item, index) => (
                                        <option key={index} value={item.POS_ProductTypeID}>
                                            {item.ProductType}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks mt-4">
                                <div className="form-check">
                                    <input
                                        type="checkbox"
                                        className="form-check-input"
                                        name="IsStockTracked"
                                        defaultChecked={data?.IsStockTracked}
                                    />
                                    <label className="form-check-label">Track Stock</label>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Unit</label>
                                <select name="FK_UnitID" className="form-select" required defaultValue={data?.FK_UnitID || ''}>
                                    <option value="">Please select..</option>
                                    {unitList.map((item, index) => (
                                        <option key={index} value={item.POS_UnitID}>
                                            {item.Unit}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Default Unit</label>
                                <select name="FK_DefaultUnitID" className="form-select" required defaultValue={data?.FK_DefaultUnitID || ''}>
                                    <option value="">Please select..</option>
                                    {unitList.map((item, index) => (
                                        <option key={index} value={item.POS_UnitID}>
                                            {item.Unit}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Category</label>
                                <select name="FK_ProductCategoryID" className="form-select" required defaultValue={data?.FK_ProductCategoryID || ''}>
                                    <option value="">Please select..</option>
                                    {categoryList.map((item, index) => (
                                        <option key={index} value={item.POS_ProductCategoryID}>
                                            {item.CategoryName}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>SKU</label>
                                <input name="SKU" type="text" className="form-control" defaultValue={data?.SKU} />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Barcode</label>
                                <input name="Barcode" type="text" className="form-control" defaultValue={data?.Barcode} />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>QR Code</label>
                                <input name="QrCode" type="text" className="form-control" defaultValue={data?.QrCode} />
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Product Image</label>
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

export default ProductForm;


ProductForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    categoryList: PropTypes.array.isRequired,
    typeList: PropTypes.array.isRequired,
    unitList: PropTypes.array.isRequired,
};
