import React, { useEffect, useState } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const StockRequestApprovalForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    lines,
    readOnly = false,
}) => {
    const [decisions, setDecisions] = useState([]);
    const [managerNotes, setManagerNotes] = useState('');

    useEffect(() => {
        if (showModel) {
            setDecisions((lines || []).map(l => ({
                POS_StockRequestLineID: l.POS_StockRequestLineID,
                ProductName: l.ProductName,
                Unit: l.Unit,
                Symbol: l.Symbol,
                Quantity: l.Quantity,
                Notes: l.Notes,
                ApprovedQuantity: readOnly ? (l.ApprovedQuantity ?? l.Quantity ?? 0) : (l.Quantity ?? 0),
                IsDeclined: readOnly ? !!l.IsDeclined : false,
                ManagerNotes: readOnly ? (l.ManagerNotes || '') : '',
            })));
            setManagerNotes(readOnly ? (data?.ManagerNotes || '') : '');
        }
    }, [showModel, lines, readOnly, data]);

    const updateDecision = (idx, field, value) => {
        setDecisions(prev => prev.map((d, i) => {
            if (i !== idx) return d;
            const next = { ...d, [field]: value };
            if (field === 'IsDeclined' && value === true) {
                next.ApprovedQuantity = 0;
            }
            return next;
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (readOnly) return;
        const payload = {
            POS_StockRequestID: data?.POS_StockRequestID,
            ManagerNotes: managerNotes ? managerNotes.trim() : null,
            LineDecisions: decisions.map(d => ({
                POS_StockRequestLineID: d.POS_StockRequestLineID,
                ApprovedQuantity: d.IsDeclined ? 0 : (parseFloat(d.ApprovedQuantity) || 0),
                IsDeclined: !!d.IsDeclined,
                ManagerNotes: d.ManagerNotes ? d.ManagerNotes.trim() : null,
            })),
        };
        if (onSubmit) {
            onSubmit(payload);
        }
    };

    const titlePrefix = readOnly ? 'View Stock Request' : 'Approve Stock Request';

    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two" size="lg">
            <form onSubmit={handleSubmit}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>
                        {titlePrefix}{data?.RefNumber ? ` - ${data.RefNumber}` : ''}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>From Debtor</label>
                                <input className="form-control" disabled value={data?.FromDebtorName || ''} readOnly />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>To Debtor</label>
                                <input className="form-control" disabled value={data?.ToDebtorName || ''} readOnly />
                            </div>
                        </div>
                        <div className="col-lg-12 mt-2">
                            <div className="table-responsive">
                                <table className="table table-bordered table-sm align-middle">
                                    <thead>
                                        <tr>
                                            <th>Product</th>
                                            <th style={{ width: '10%' }}>Unit</th>
                                            <th style={{ width: '12%' }}>Requested</th>
                                            <th style={{ width: '15%' }}>Approved Qty</th>
                                            <th style={{ width: '8%' }}>Decline</th>
                                            <th>Manager Notes</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {decisions.length > 0 ? decisions.map((d, idx) => (
                                            <tr key={idx}>
                                                <td>{d.ProductName || 'N/A'}</td>
                                                <td>{d.Symbol || d.Unit || 'N/A'}</td>
                                                <td>{d.Quantity ?? 'N/A'}</td>
                                                <td>
                                                    <input type="number" min="0" step="0.0001"
                                                        className="form-control form-control-sm"
                                                        disabled={readOnly || d.IsDeclined}
                                                        value={d.ApprovedQuantity ?? ''}
                                                        onChange={(e) => updateDecision(idx, 'ApprovedQuantity', e.target.value)} />
                                                </td>
                                                <td className="text-center">
                                                    <input type="checkbox"
                                                        disabled={readOnly}
                                                        checked={!!d.IsDeclined}
                                                        onChange={(e) => updateDecision(idx, 'IsDeclined', e.target.checked)} />
                                                </td>
                                                <td>
                                                    <input type="text" className="form-control form-control-sm"
                                                        disabled={readOnly}
                                                        value={d.ManagerNotes || ''}
                                                        onChange={(e) => updateDecision(idx, 'ManagerNotes', e.target.value)} />
                                                </td>
                                            </tr>
                                        )) : (
                                            <tr>
                                                <td colSpan="6" className="text-center">No lines to approve</td>
                                            </tr>
                                        )}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Manager Notes (overall)</label>
                                <textarea rows={2} className="form-control"
                                    disabled={readOnly}
                                    value={managerNotes}
                                    onChange={(e) => setManagerNotes(e.target.value)}></textarea>
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
                        {readOnly ? 'Close' : 'Cancel'}
                    </button>
                    {!readOnly && (
                        <button
                            type="submit"
                            className="btn btn-submit"
                            disabled={decisions.length === 0}
                        >
                            Submit Decision
                        </button>
                    )}
                </Modal.Footer>
            </form>
        </Modal>
    );
}

export default StockRequestApprovalForm;


StockRequestApprovalForm.propTypes = {
    data: PropTypes.object,
    lines: PropTypes.array,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    readOnly: PropTypes.bool,
};
