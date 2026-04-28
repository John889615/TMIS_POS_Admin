import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const SubstitutionForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    productList,
    id
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
            FK_ProductID: parseFloat(id),                     // parent product
            FK_ProductSubstitutionID: parseFloat(form.FK_ProductSubstitutionID.value) || 0,         // child product id
            IsQuantified: form.IsQuantified.checked,
            Quantity: form.Quantity.value ? parseFloat(form.Quantity.value) : 0,
            IsExtraCharge: form.IsExtraCharge.checked,
        };

        if (data?.ProductSubstitutionID) {
            productData.ProductSubstitutionID = data.ProductSubstitutionID;
        }

        if (onSubmit) {
           onSubmit(productData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Substitutions</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Product Item</label>
                                <select name="FK_ProductSubstitutionID" className="form-select" defaultValue={data?.FK_ProductSubstitutionID || ''}>
                                    <option value="">Select Product..</option>
                                    {productList?.map((item, idx) => (
                                        <option key={idx} value={item.POS_ProductID}>
                                            {item.ProductName}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks mt-4">
                                <div className="form-check">
                                    <input type="checkbox" className="form-check-input" name="IsQuantified" defaultChecked={data?.IsQuantified} />
                                    <label className="form-check-label">Is Quantified</label>
                                </div>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Quantity</label>
                                <input name="Quantity" type="number" className="form-control" defaultValue={data?.Quantity || 0} />
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks mt-4">
                                <div className="form-check">
                                    <input type="checkbox" className="form-check-input" name="IsExtraCharge" defaultChecked={data?.IsExtraCharge} />
                                    <label className="form-check-label">Is Extra Charge</label>
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

export default SubstitutionForm;


SubstitutionForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    productList: PropTypes.array.isRequired,
    id: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string,
    ]),
};
