import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const PreparationForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    methodList,
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

        const prepData = {
            FK_ProductID: parseFloat(id),                     // parent product
            FK_ProductPreparationMethodID: parseFloat(form.FK_ProductPreparationMethodID.value) || 0,
        };

        if (data?.ProductPreparationID) {
            prepData.ProductPreparationID = data.ProductPreparationID;
        }

        if (onSubmit) {
            onSubmit(prepData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Preparation</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Preparation Method</label>
                                <select name="FK_ProductPreparationMethodID" required className="form-select" defaultValue={data?.FK_ProductPreparationMethodID || ''}>
                                    <option value="">Select Preparation Method..</option>
                                    {methodList?.map((item, idx) => (
                                        <option key={idx} value={item.ProductPreparationMethodID}>
                                            {item.ShortCode} / {item.Method}
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

export default PreparationForm;


PreparationForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    methodList: PropTypes.array.isRequired,
    id: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string,
    ]),
};
