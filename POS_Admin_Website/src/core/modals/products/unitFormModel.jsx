import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const UnitForm = ({
    onSubmit,
    showModel,
    handleClose,
    data
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

        const unitData = {
            Unit: form.Unit.value.trim(),
            Symbol: form.Symbol.value.trim()
        };

        if (data?.POS_UnitID) {
            unitData.POS_UnitID = data.POS_UnitID;
        }

        if (onSubmit) {
            onSubmit(unitData);
        }
    };

    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Unit</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Unit Name</label>
                                <input name="Unit" required type="text" defaultValue={data?.Unit} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Symbol</label>
                                <input name="Symbol" type="text" defaultValue={data?.Symbol} className="form-control" />
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

export default UnitForm;


UnitForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};
