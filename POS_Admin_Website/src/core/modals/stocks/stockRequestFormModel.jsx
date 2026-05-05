import React, { useEffect, useMemo, useRef, useState } from 'react'
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";
import { Plus, Trash2 } from "react-feather";
import Select from "react-select";
import { useSelector } from 'react-redux';

const emptyLine = () => ({ FK_ProductID: '', FK_UnitID: '', Quantity: '', Notes: '' });

const StockRequestForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    existingLines,
    productList,
    unitList,
}) => {
    const formRef = useRef(null);
    const debtors = useSelector((state) => state.debtors_data);
    const isEdit = !!data?.POS_StockRequestID;
    const [lines, setLines] = useState([emptyLine()]);
    const [fromDebtor, setFromDebtor] = useState(null);
    const [toDebtor, setToDebtor] = useState(null);

    const debtorOptions = useMemo(
        () => (debtors || []).map(d => ({
            value: d.DebtorID,
            label: `${d.ShortCode || ''} ${d.Name || ''}`.trim(),
        })),
        [debtors]
    );

    const productOptions = useMemo(
        () => (productList || []).map(p => ({
            value: p.POS_ProductID,
            label: p.ProductName,
            defaultUnitId: p.FK_UnitID ?? p.FK_DefaultUnitID ?? null,
        })),
        [productList]
    );

    const unitOptions = useMemo(
        () => (unitList || []).map(u => ({
            value: u.POS_UnitID,
            label: u.Symbol ? `${u.Unit} (${u.Symbol})` : u.Unit,
        })),
        [unitList]
    );

    const selectStyles = useMemo(() => ({
        control: (base, state) => ({
            ...base,
            minHeight: 38,
            borderRadius: 8,
            backgroundColor: "#fff",
            color: "#000",
            boxShadow: state.isFocused ? "0 0 0 0.2rem rgba(13,110,253,.25)" : base.boxShadow,
            borderColor: state.isFocused ? "#86b7fe" : base.borderColor,
        }),
        valueContainer: (base) => ({ ...base, padding: "0 10px", color: "#000" }),
        input: (base) => ({ ...base, color: "#000", caretColor: "#000" }),
        singleValue: (base) => ({ ...base, color: "#000" }),
        placeholder: (base) => ({ ...base, color: "#6c757d" }),
        option: (base, state) => ({
            ...base,
            color: "#000",
            backgroundColor: state.isFocused ? "#f1f1f1" : "#fff",
        }),
        menu: (base) => ({ ...base, zIndex: 9999 }),
        menuPortal: (base) => ({ ...base, zIndex: 99999 }),
    }), []);

    const selectTheme = (theme) => ({
        ...theme,
        colors: {
            ...theme.colors,
            neutral80: "#000",
            neutral50: "#6c757d",
            neutral0: "#fff",
            primary25: "#f1f1f1",
            primary: "#0d6efd",
        },
    });

    const compactSelectStyles = useMemo(() => ({
        ...selectStyles,
        control: (base, state) => ({
            ...selectStyles.control(base, state),
            minHeight: 32,
        }),
        valueContainer: (base) => ({ ...base, padding: "0 8px", color: "#000" }),
    }), [selectStyles]);

    useEffect(() => {
        if (showModel && formRef.current) {
            formRef.current.reset();
        }
        if (showModel) {
            if (isEdit) {
                const seeded = (existingLines || []).map(l => ({
                    FK_ProductID: l.FK_ProductID ?? '',
                    FK_UnitID:    l.FK_UnitID ?? '',
                    Quantity:     l.Quantity ?? '',
                    Notes:        l.Notes ?? '',
                }));
                setLines(seeded.length > 0 ? seeded : [emptyLine()]);
            } else {
                setLines([emptyLine()]);
            }
            setFromDebtor(
                data?.FK_FromDebtorID
                    ? debtorOptions.find(o => o.value === data.FK_FromDebtorID) || null
                    : null
            );
            setToDebtor(
                data?.FK_ToDebtorID
                    ? debtorOptions.find(o => o.value === data.FK_ToDebtorID) || null
                    : null
            );
        }
    }, [showModel, data, debtorOptions, isEdit, existingLines]);

    const updateLine = (idx, field, value) => {
        setLines(prev => prev.map((line, i) => i === idx ? { ...line, [field]: value } : line));
    };

    const addLine = () => setLines(prev => [...prev, emptyLine()]);
    const removeLine = (idx) => setLines(prev => prev.filter((_, i) => i !== idx));

    const collectAndValidate = () => {
        const form = formRef.current;
        if (!form) return null;

        const refNumber = form.RefNumber?.value?.trim();
        if (!refNumber) {
            alert('Please enter a Ref Number.');
            return null;
        }
        if (!fromDebtor) {
            alert('Please select From Debtor.');
            return null;
        }
        if (!toDebtor) {
            alert('Please select To Debtor.');
            return null;
        }

        const stockData = {
            RefNumber: refNumber,
            FK_FromDebtorID: fromDebtor.value,
            FK_ToDebtorID: toDebtor.value,
            Notes: form.Notes?.value?.trim() ?? '',
        };

        const cleanLines = lines
            .filter(l => l.FK_ProductID && l.Quantity)
            .map(l => ({
                FK_ProductID: parseInt(l.FK_ProductID),
                FK_UnitID: l.FK_UnitID ? parseInt(l.FK_UnitID) : null,
                Quantity: parseFloat(l.Quantity),
                Notes: l.Notes ? l.Notes.trim() : null,
            }));
        if (cleanLines.length === 0) {
            alert("Please add at least one line item with product and quantity.");
            return null;
        }
        stockData.Lines = cleanLines;

        if (isEdit) {
            stockData.POS_StockRequestID = data.POS_StockRequestID;
        }

        return stockData;
    };

    const handleSaveClick = () => {
        const stockData = collectAndValidate();
        if (!stockData) return;
        if (onSubmit) onSubmit(stockData, false);
    };

    const handleSubmitClick = () => {
        const stockData = collectAndValidate();
        if (!stockData) return;
        if (onSubmit) onSubmit(stockData, true);
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two" size="lg">
            <form onSubmit={(e) => e.preventDefault()} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Stock Request</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Ref. Number</label>
                                <input name="RefNumber" required type="text" defaultValue={data?.RefNumber} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>From Debtor</label>
                                <Select
                                    classNamePrefix="react-select"
                                    options={debtorOptions}
                                    value={fromDebtor}
                                    onChange={setFromDebtor}
                                    placeholder="Please select.."
                                    isClearable
                                    isSearchable
                                    styles={selectStyles}
                                    theme={selectTheme}
                                    menuPortalTarget={document.body}
                                    noOptionsMessage={() => "No debtors found"}
                                />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>To Debtor</label>
                                <Select
                                    classNamePrefix="react-select"
                                    options={debtorOptions}
                                    value={toDebtor}
                                    onChange={setToDebtor}
                                    placeholder="Please select.."
                                    isClearable
                                    isSearchable
                                    styles={selectStyles}
                                    theme={selectTheme}
                                    menuPortalTarget={document.body}
                                    noOptionsMessage={() => "No debtors found"}
                                />
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Notes</label>
                                <textarea rows={3} name="Notes" className="form-control" defaultValue={data?.Notes}></textarea>
                            </div>
                        </div>

                        <div className="col-lg-12 mt-3">
                            <div className="d-flex justify-content-between align-items-center">
                                <h6 className="mb-0">Line Items</h6>
                                <button type="button" className="btn btn-sm btn-primary" onClick={addLine}>
                                    <Plus size={14} className="me-1" /> Add Line
                                </button>
                            </div>
                        </div>
                                <div className="col-lg-12 mt-2">
                                    <div className="table-responsive">
                                        <table className="table table-bordered table-sm align-middle">
                                            <thead>
                                                <tr>
                                                    <th style={{ width: '35%' }}>Product</th>
                                                    <th style={{ width: '20%' }}>Unit</th>
                                                    <th style={{ width: '15%' }}>Quantity</th>
                                                    <th>Notes</th>
                                                    <th style={{ width: '50px' }}></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {lines.map((line, idx) => {
                                                    const productValue = line.FK_ProductID
                                                        ? productOptions.find(o => o.value === parseInt(line.FK_ProductID)) || null
                                                        : null;
                                                    const unitValue = line.FK_UnitID
                                                        ? unitOptions.find(o => o.value === parseInt(line.FK_UnitID)) || null
                                                        : null;
                                                    return (
                                                        <tr key={idx}>
                                                            <td>
                                                                <Select
                                                                    classNamePrefix="react-select"
                                                                    options={productOptions}
                                                                    value={productValue}
                                                                    onChange={(opt) => {
                                                                        const productId = opt ? opt.value : '';
                                                                        setLines(prev => prev.map((l, i) => {
                                                                            if (i !== idx) return l;
                                                                            const next = { ...l, FK_ProductID: productId };
                                                                            if (opt && opt.defaultUnitId && !l.FK_UnitID) {
                                                                                next.FK_UnitID = opt.defaultUnitId;
                                                                            }
                                                                            return next;
                                                                        }));
                                                                    }}
                                                                    placeholder="Select product.."
                                                                    isClearable
                                                                    isSearchable
                                                                    styles={compactSelectStyles}
                                                                    theme={selectTheme}
                                                                    menuPortalTarget={document.body}
                                                                    noOptionsMessage={() => "No products found"}
                                                                />
                                                            </td>
                                                            <td>
                                                                <Select
                                                                    classNamePrefix="react-select"
                                                                    options={unitOptions}
                                                                    value={unitValue}
                                                                    onChange={(opt) => updateLine(idx, 'FK_UnitID', opt ? opt.value : '')}
                                                                    placeholder="Select unit.."
                                                                    isClearable
                                                                    isSearchable
                                                                    styles={compactSelectStyles}
                                                                    theme={selectTheme}
                                                                    menuPortalTarget={document.body}
                                                                    noOptionsMessage={() => "No units found"}
                                                                />
                                                            </td>
                                                            <td>
                                                                <input required type="number" min="0.0001" step="0.0001"
                                                                    className="form-control form-control-sm"
                                                                    value={line.Quantity}
                                                                    onChange={(e) => updateLine(idx, 'Quantity', e.target.value)} />
                                                            </td>
                                                            <td>
                                                                <input type="text" className="form-control form-control-sm"
                                                                    value={line.Notes}
                                                                    onChange={(e) => updateLine(idx, 'Notes', e.target.value)} />
                                                            </td>
                                                            <td className="text-center">
                                                                {lines.length > 1 && (
                                                                    <button type="button" className="btn btn-sm btn-link text-danger p-0"
                                                                        onClick={() => removeLine(idx)}>
                                                                        <Trash2 size={16} />
                                                                    </button>
                                                                )}
                                                            </td>
                                                        </tr>
                                                    );
                                                })}
                                            </tbody>
                                        </table>
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
                    <button type="button" className="btn btn-secondary me-2" onClick={handleSaveClick}>
                        Save
                    </button>
                    <button type="button" className="btn btn-submit" onClick={handleSubmitClick}>
                        Submit for Approval
                    </button>
                </Modal.Footer>

            </form>
        </Modal>
    );
}

export default StockRequestForm;


StockRequestForm.propTypes = {
    data: PropTypes.object,
    existingLines: PropTypes.array,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    productList: PropTypes.array,
    unitList: PropTypes.array,
};
