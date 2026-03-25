import React, { useEffect, useMemo, useRef, useState } from 'react';
import PropTypes from 'prop-types';
import { Modal } from "react-bootstrap";

const SlipPrinterForm = ({
    branchList,
    onSubmit,
    showModel,
    handleClose,
    data,
    costCenterList,
    debtorList
}) => {
    const formRef = useRef(null);

    const debtorWrapperRef = useRef(null);
    const debtorSearchRef = useRef(null);

    const costCenterWrapperRef = useRef(null);
    const costCenterSearchRef = useRef(null);

    const [debtorOpen, setDebtorOpen] = useState(false);
    const [debtorFilter, setDebtorFilter] = useState("");
    const [selectedDebtorID, setSelectedDebtorID] = useState("");
    const [selectedDebtorName, setSelectedDebtorName] = useState("");

    const [costCenterOpen, setCostCenterOpen] = useState(false);
    const [costCenterFilter, setCostCenterFilter] = useState("");
    const [selectedCostCenterID, setSelectedCostCenterID] = useState("");
    const [selectedCostCenterName, setSelectedCostCenterName] = useState("");

    useEffect(() => {
        if (showModel) {
            const initialDebtorID = data?.DebtorID ? String(data.DebtorID) : "";
            const initialDebtor = debtorList.find(
                (item) => String(item.DebtorID) === initialDebtorID
            );

            const initialCostCenterID = data?.CostCenterID ? String(data.CostCenterID) : "";
            const initialCostCenter = costCenterList.find(
                (item) => String(item.CostCenterID) === initialCostCenterID
            );

            if (formRef.current) {
                formRef.current.reset();
            }

            setDebtorOpen(false);
            setDebtorFilter("");
            setSelectedDebtorID(initialDebtorID);
            setSelectedDebtorName(initialDebtor?.Name || "");

            setCostCenterOpen(false);
            setCostCenterFilter("");
            setSelectedCostCenterID(initialCostCenterID);
            setSelectedCostCenterName(initialCostCenter?.Name || "");
        }
    }, [showModel, data, debtorList, costCenterList]);

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (
                debtorWrapperRef.current &&
                !debtorWrapperRef.current.contains(event.target)
            ) {
                setDebtorOpen(false);
            }

            if (
                costCenterWrapperRef.current &&
                !costCenterWrapperRef.current.contains(event.target)
            ) {
                setCostCenterOpen(false);
            }
        };

        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    useEffect(() => {
        if (debtorOpen && debtorSearchRef.current) {
            debtorSearchRef.current.focus();
        }
    }, [debtorOpen]);

    useEffect(() => {
        if (costCenterOpen && costCenterSearchRef.current) {
            costCenterSearchRef.current.focus();
        }
    }, [costCenterOpen]);

    const filteredDebtors = useMemo(() => {
        const term = debtorFilter.trim().toLowerCase();

        if (!term) return debtorList;

        return debtorList.filter((item) =>
            (item?.Name || "").toLowerCase().includes(term)
        );
    }, [debtorList, debtorFilter]);

    const filteredCostCenters = useMemo(() => {
        const term = costCenterFilter.trim().toLowerCase();

        if (!term) return costCenterList;

        return costCenterList.filter((item) =>
            (item?.Name || "").toLowerCase().includes(term)
        );
    }, [costCenterList, costCenterFilter]);

    const handleDebtorSelect = (item) => {
        const nextID = item?.DebtorID ? String(item.DebtorID) : "";
        const nextName = item?.Name || "";

        setSelectedDebtorID(nextID);
        setSelectedDebtorName(nextName);
        setDebtorFilter("");
        setDebtorOpen(false);
    };

    const handleCostCenterSelect = (item) => {
        const nextID = item?.CostCenterID ? String(item.CostCenterID) : "";
        const nextName = item?.Name || "";

        setSelectedCostCenterID(nextID);
        setSelectedCostCenterName(nextName);
        setCostCenterFilter("");
        setCostCenterOpen(false);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;

        const payload = {
            DebtorID: selectedDebtorID ? parseInt(selectedDebtorID) : 0,
            CostCenterID: selectedCostCenterID ? parseInt(selectedCostCenterID) : null,
            Name: form.Name.value.trim(),
            Model: form.Model.value.trim(),
            IpAddress: form.IpAddress.value.trim(),
            Port: form.Port.value ? parseInt(form.Port.value) : 0,
            IsDefault: form.IsDefault.checked,
            IsActive: form.IsActive.checked
        };

        if (data?.SlipPrinterID) {
            payload.SlipPrinterID = data.SlipPrinterID;
        }

        if (onSubmit) {
            onSubmit(payload);
        }
    };

    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Slip Printer</Modal.Title>
                </Modal.Header>

                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Debtor</label>

                                <div className="position-relative" ref={debtorWrapperRef}>
                                    <input
                                        type="hidden"
                                        name="DebtorID"
                                        value={selectedDebtorID}
                                        readOnly
                                    />

                                    <div
                                        className="form-control d-flex align-items-center justify-content-between"
                                        style={{
                                            cursor: "pointer",
                                            minHeight: "38px",
                                            userSelect: "none"
                                        }}
                                        onClick={() => setDebtorOpen((prev) => !prev)}
                                    >
                                        <span className={selectedDebtorName ? "" : "text-muted"}>
                                            {selectedDebtorName || "Select Debtor"}
                                        </span>
                                        <span style={{ fontSize: "12px" }}>
                                            {debtorOpen ? "▲" : "▼"}
                                        </span>
                                    </div>

                                    {debtorOpen && (
                                        <div
                                            className="position-absolute w-100 bg-white border rounded shadow"
                                            style={{
                                                top: "100%",
                                                left: 0,
                                                marginTop: "4px",
                                                zIndex: 9999
                                            }}
                                        >
                                            <div className="p-2 border-bottom">
                                                <input
                                                    ref={debtorSearchRef}
                                                    type="text"
                                                    className="form-control"
                                                    placeholder="Search debtor..."
                                                    value={debtorFilter}
                                                    onChange={(e) => setDebtorFilter(e.target.value)}
                                                    onClick={(e) => e.stopPropagation()}
                                                />
                                            </div>

                                            <div
                                                style={{
                                                    maxHeight: "220px",
                                                    overflowY: "auto"
                                                }}
                                            >
                                                <div
                                                    className="px-3 py-2"
                                                    style={{ cursor: "pointer" }}
                                                    onClick={() => handleDebtorSelect(null)}
                                                >
                                                    Select Debtor
                                                </div>

                                                {filteredDebtors.length > 0 ? (
                                                    filteredDebtors.map((item) => (
                                                        <div
                                                            key={item.DebtorID}
                                                            className="px-3 py-2"
                                                            style={{
                                                                cursor: "pointer",
                                                                backgroundColor:
                                                                    String(selectedDebtorID) === String(item.DebtorID)
                                                                        ? "#f8f9fa"
                                                                        : "transparent"
                                                            }}
                                                            onClick={() => handleDebtorSelect(item)}
                                                        >
                                                            {item.Name}
                                                        </div>
                                                    ))
                                                ) : (
                                                    <div className="px-3 py-2 text-muted">
                                                        No debtors found
                                                    </div>
                                                )}
                                            </div>
                                        </div>
                                    )}
                                </div>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Cost Center</label>

                                <div className="position-relative" ref={costCenterWrapperRef}>
                                    <input
                                        type="hidden"
                                        name="CostCenterID"
                                        value={selectedCostCenterID}
                                        readOnly
                                    />

                                    <div
                                        className="form-control d-flex align-items-center justify-content-between"
                                        style={{
                                            cursor: "pointer",
                                            minHeight: "38px",
                                            userSelect: "none"
                                        }}
                                        onClick={() => setCostCenterOpen((prev) => !prev)}
                                    >
                                        <span className={selectedCostCenterName ? "" : "text-muted"}>
                                            {selectedCostCenterName || "Select Cost Center"}
                                        </span>
                                        <span style={{ fontSize: "12px" }}>
                                            {costCenterOpen ? "▲" : "▼"}
                                        </span>
                                    </div>

                                    {costCenterOpen && (
                                        <div
                                            className="position-absolute w-100 bg-white border rounded shadow"
                                            style={{
                                                top: "100%",
                                                left: 0,
                                                marginTop: "4px",
                                                zIndex: 9999
                                            }}
                                        >
                                            <div className="p-2 border-bottom">
                                                <input
                                                    ref={costCenterSearchRef}
                                                    type="text"
                                                    className="form-control"
                                                    placeholder="Search cost center..."
                                                    value={costCenterFilter}
                                                    onChange={(e) => setCostCenterFilter(e.target.value)}
                                                    onClick={(e) => e.stopPropagation()}
                                                />
                                            </div>

                                            <div
                                                style={{
                                                    maxHeight: "220px",
                                                    overflowY: "auto"
                                                }}
                                            >
                                                <div
                                                    className="px-3 py-2"
                                                    style={{ cursor: "pointer" }}
                                                    onClick={() => handleCostCenterSelect(null)}
                                                >
                                                    Select Cost Center
                                                </div>

                                                {filteredCostCenters.length > 0 ? (
                                                    filteredCostCenters.map((item) => (
                                                        <div
                                                            key={item.CostCenterID}
                                                            className="px-3 py-2"
                                                            style={{
                                                                cursor: "pointer",
                                                                backgroundColor:
                                                                    String(selectedCostCenterID) === String(item.CostCenterID)
                                                                        ? "#f8f9fa"
                                                                        : "transparent"
                                                            }}
                                                            onClick={() => handleCostCenterSelect(item)}
                                                        >
                                                            {item.Name}
                                                        </div>
                                                    ))
                                                ) : (
                                                    <div className="px-3 py-2 text-muted">
                                                        No cost centers found
                                                    </div>
                                                )}
                                            </div>
                                        </div>
                                    )}
                                </div>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Name</label>
                                <input
                                    name="Name"
                                    type="text"
                                    required
                                    defaultValue={data?.Name || ""}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Model</label>
                                <input
                                    name="Model"
                                    type="text"
                                    required
                                    defaultValue={data?.Model || ""}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>IP Address</label>
                                <input
                                    name="IpAddress"
                                    type="text"
                                    required
                                    defaultValue={data?.IpAddress || ""}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Port</label>
                                <input
                                    name="Port"
                                    type="number"
                                    required
                                    defaultValue={data?.Port || ""}
                                    className="form-control"
                                />
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>
                                    <input
                                        name="IsDefault"
                                        type="checkbox"
                                        defaultChecked={data?.IsDefault ?? true}
                                    />{" "}
                                    Is Default
                                </label>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>
                                    <input
                                        name="AutoCut"
                                        type="checkbox"
                                        defaultChecked={data?.AutoCut ?? true}
                                    />{" "}
                                    Auto Cut
                                </label>
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>
                                    <input
                                        name="IsActive"
                                        type="checkbox"
                                        defaultChecked={data?.IsActive ?? true}
                                    />{" "}
                                    Is Active
                                </label>
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
};

export default SlipPrinterForm;

SlipPrinterForm.propTypes = {
    data: PropTypes.object,
    costCenterList: PropTypes.array.isRequired,
    debtorList: PropTypes.array.isRequired,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
};