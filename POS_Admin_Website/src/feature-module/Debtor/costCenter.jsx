import React, { useState, useEffect, useMemo } from "react";
import {
    getAllCostCenter,
    getAllCostCenterTypes,
    newCostCenter,
    updateCostCenter,
    getCostCenterPrinters,
    newCostCenterPrinter,
    updateCostCenterPrinter,
    } from "../../services/debtors/costCenter";
    import { getAllDebtors } from "../../services/debtors/debtors";
    import { getAllStatus } from "../../services/entityData/status";
    import { getAllSlipPrinter, getAllSlipTypes } from "../../services/entityData/slipPrinter";
    import { Button, Modal, Form } from "react-bootstrap";
    import { Link } from "react-router-dom";
    import { PlusCircle, Printer, ArrowLeft } from "react-feather";
    import CostCenterForm from "../../core/modals/debtors/costCenterFormModel";
    const CostCenter = () => {
    const [listData, setListData] = useState([]);
    const [costTypeList, setCostTypeList] = useState([]);
    const [statusList, setStatusList] = useState([]);
    const [debtorList, setDebtorList] = useState([]);

    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);

    const [showPrinterView, setShowPrinterView] = useState(false);
    const [selectedCostCenter, setSelectedCostCenter] = useState(null);
    const [printerSearchTerm, setPrinterSearchTerm] = useState("");
    const [printerGridData, setPrinterGridData] = useState([]);
    const [printerMasterList, setPrinterMasterList] = useState([]);
    const [printerLoading, setPrinterLoading] = useState(false);

    const [showPrinterFormModal, setShowPrinterFormModal] = useState(false);
    const [selectedPrinterRow, setSelectedPrinterRow] = useState(null);

    const [printerFormData, setPrinterFormData] = useState({
        CostCenterPrinterID: 0,
        FK_CostCenterID: 0,
        FK_PrinterID: "",
        FK_InvoiceSlipTypeID: "",
        FK_TabSlipTypeID: "",
    });

    const [slipTypeList, setSlipTypeList] = useState([]);
    const [savingPrinter, setSavingPrinter] = useState(false);

    useEffect(() => {
        fetchRecords();
        fetchPrinterMasterList();
        fetchSlipTypeList();
    }, []);

    const fetchRecords = async () => {
        try {
            const data = await getAllCostCenter();
            setListData(data || []);

            const type = await getAllCostCenterTypes();
            setCostTypeList(type || []);

            const status = await getAllStatus();
            setStatusList(status || []);

            const debtor = await getAllDebtors();
            setDebtorList(debtor || []);
        } catch (err) {
            console.error("Failed to load cost center records:", err.message);
        }
    };

    const fetchPrinterMasterList = async () => {
        try {
            const printers = await getAllSlipPrinter();
            setPrinterMasterList(Array.isArray(printers) ? printers : []);
        } catch (err) {
            console.error("Failed to load printer master list:", err);
            setPrinterMasterList([]);
        }
    };

    const fetchSlipTypeList = async () => {
        try {
            const slipTypes = await getAllSlipTypes();
            setSlipTypeList(Array.isArray(slipTypes) ? slipTypes : []);
        } catch (err) {
            console.error("Failed to load slip type list:", err);
            setSlipTypeList([]);
        }
    };

    const filteredData = listData.filter((item) =>
        Object.values(item || {}).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    const filteredPrinters = printerGridData.filter((item) =>
        Object.values(item || {}).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(printerSearchTerm.toLowerCase())
        )
    );

    const printerOptions = useMemo(() => {
        return (printerMasterList || []).map((printer) => ({
            PrinterID: printer.PrinterID ?? printer.SlipPrinterID ?? printer.ID ?? 0,
            PrinterName:
                printer.PrinterName ??
                printer.Name ??
                printer.Description ??
                "Unnamed Printer",
        }));
    }, [printerMasterList]);

 const slipTypeOptions = useMemo(() => {
    return (slipTypeList || []).map((item) => ({
        SlipTypeID: item.SlipTypeID ?? item.ID ?? 0,
        SlipType: item.SlipType ?? item.Name ?? "",
        SlipCode: item.SlipCode ?? "",
    }));
}, [slipTypeList]);

const invoiceSlipTypeOptions = useMemo(() => {
    return slipTypeOptions.filter((item) =>
        String(item.SlipType || "").toLowerCase().includes("invoice")
    );
}, [slipTypeOptions]);

const tabSlipTypeOptions = useMemo(() => {
    return slipTypeOptions.filter((item) =>
        String(item.SlipType || "").toLowerCase().includes("tab")
    );
}, [slipTypeOptions]);

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true);
    };

    const handleClose = () => {
        setModelShow(false);
        setSelectedData(null);
    };

    const handleAddCostCenter = async (data) => {
        try {
            if (data.CostCenterID) {
                await updateCostCenter(data);
            } else {
                await newCostCenter(data);
            }

            await fetchRecords();
            setModelShow(false);
            setSelectedData(null);
        } catch (err) {
            console.error("Error saving cost center:", err.message);
        }
    };

    const handleEditCostCenter = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };

    const isTemplateRow = (record) => {
        if (!record) return false;

        if (record.SourceType === "Global") return true;
        if (record.Location === "Template") return true;
        if (record.IsTemplate === true) return true;
        if (record.Template === true) return true;

        return false;
    };

    const loadCostCenterPrinters = async (costCenterId) => {
        try {
            setPrinterLoading(true);

            const response = await getCostCenterPrinters({
                FK_CostCenterID: costCenterId,
            });

            const rows = Array.isArray(response?.Data) ? response.Data : [];
            setPrinterGridData(rows);
        } catch (err) {
            console.error("Failed to load cost center printers:", err);
            setPrinterGridData([]);
        } finally {
            setPrinterLoading(false);
        }
    };

    const handleOpenPrinterView = async (record) => {
        setSelectedCostCenter(record);
        setPrinterSearchTerm("");
        setShowPrinterView(true);
        await loadCostCenterPrinters(record.CostCenterID);
    };

    const handleClosePrinterView = () => {
        setSelectedCostCenter(null);
        setPrinterSearchTerm("");
        setPrinterGridData([]);
        setShowPrinterView(false);
    };

    const handleOpenAddPrinterModal = () => {
        setSelectedPrinterRow(null);
        setPrinterFormData({
            CostCenterPrinterID: 0,
            FK_CostCenterID: selectedCostCenter?.CostCenterID || 0,
            FK_PrinterID: "",
            FK_InvoiceSlipTypeID: "",
            FK_TabSlipTypeID: "",
        });
        setShowPrinterFormModal(true);
    };

    const handleOpenEditPrinterModal = (row) => {
    setSelectedPrinterRow(row);
    setPrinterFormData({
        CostCenterPrinterID: row?.CostCenterPrinterID || 0,
        FK_CostCenterID:
            row?.FK_CostCenterID || selectedCostCenter?.CostCenterID || 0,
        FK_PrinterID: row?.FK_PrinterID || "",
        FK_InvoiceSlipTypeID:
            row?.FK_InvoiceSlipTypeID ||
            row?.InvoiceSlipTypeID ||
            "",
        FK_TabSlipTypeID:
            row?.FK_TabSlipTypeID ||
            row?.TabSlipTypeID ||
            "",
    });
    setShowPrinterFormModal(true);
};

    const handleClosePrinterFormModal = () => {
        setShowPrinterFormModal(false);
        setSelectedPrinterRow(null);
        setPrinterFormData({
            CostCenterPrinterID: 0,
            FK_CostCenterID: 0,
            FK_PrinterID: "",
            FK_InvoiceSlipTypeID: "",
            FK_TabSlipTypeID: "",
        });
    };

    const handleSavePrinter = async () => {
        try {
           if (
    !printerFormData.FK_CostCenterID ||
    !printerFormData.FK_PrinterID
) {
    return;
}

            setSavingPrinter(true);

           const payload = {
    FK_CostCenterID: Number(printerFormData.FK_CostCenterID),
    FK_PrinterID: Number(printerFormData.FK_PrinterID),
    FK_InvoiceSlipTypeID: printerFormData.FK_InvoiceSlipTypeID
        ? Number(printerFormData.FK_InvoiceSlipTypeID)
        : null,
    FK_TabSlipTypeID: printerFormData.FK_TabSlipTypeID
        ? Number(printerFormData.FK_TabSlipTypeID)
        : null,
};

            if (printerFormData.CostCenterPrinterID && Number(printerFormData.CostCenterPrinterID) > 0) {
                await updateCostCenterPrinter({
                    CostCenterPrinterID: Number(printerFormData.CostCenterPrinterID),
                    ...payload,
                });
            } else {
                await newCostCenterPrinter(payload);
            }

            await loadCostCenterPrinters(selectedCostCenter.CostCenterID);
            handleClosePrinterFormModal();
        } catch (err) {
            console.error("Failed to save cost center printer:", err);
        } finally {
            setSavingPrinter(false);
        }
    };

    const getPrinterName = (row) => {
    const match = printerMasterList.find(
        (x) =>
            Number(x.PrinterID ?? x.SlipPrinterID ?? x.ID) ===
            Number(row?.FK_PrinterID)
    );

    return (
        match?.PrinterName ??
        match?.Name ??
        match?.Description ??
        `Printer #${row?.FK_PrinterID}`
    );
};

const getSlipCodeById = (slipTypeId) => {
    const match = slipTypeOptions.find(
        (x) => Number(x.SlipTypeID) === Number(slipTypeId)
    );

    return match?.SlipCode || "N/A";
};

const getInvoiceSlipCode = (row) => {
    return (
        row?.InvoiceSlipCode ||
        row?.InvoiceSlipTypeCode ||
        row?.FK_InvoiceSlipTypeCode ||
        getSlipCodeById(row?.FK_InvoiceSlipTypeID || row?.InvoiceSlipTypeID)
    );
};

const getTabSlipCode = (row) => {
    return (
        row?.TabSlipCode ||
        row?.TabSlipTypeCode ||
        row?.FK_TabSlipTypeCode ||
        getSlipCodeById(row?.FK_TabSlipTypeID || row?.TabSlipTypeID)
    );
};

    return (
        <div className="page-wrapper">
            <div className="content">
                {!showPrinterView ? (
                    <>
                        <div className="page-header">
                            <div className="add-item d-flex">
                                <div className="page-title">
                                    <h4>Cost Centers</h4>
                                    <h6>Manage Your Cost Center</h6>
                                </div>
                            </div>
                            <div className="page-btn">
                                <Button variant="none" className="btn btn-added" onClick={handleShow}>
                                    <PlusCircle className="me-2" />
                                    Add New Cost Center
                                </Button>
                            </div>
                        </div>

                        <div className="card table-list-card">
                            <div className="card-body">
                                <div className="table-top">
                                    <div className="search-set">
                                        <div className="search-input">
                                            <input
                                                type="text"
                                                placeholder="Search"
                                                className="form-control"
                                                value={searchTerm}
                                                onChange={(e) => setSearchTerm(e.target.value)}
                                            />
                                            <Link to="#" className="btn btn-searchset">
                                                <i data-feather="search" className="feather-search" />
                                            </Link>
                                        </div>
                                    </div>
                                </div>

                                <div className="table-responsive">
                                    <table className="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>Name</th>
                                                <th>Debtor</th>
                                                <th>Status</th>
                                                <th>Type</th>
                                                <th>Billing Reference</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {filteredData.length > 0 ? (
                                                filteredData.map((item, index) => (
                                                    <tr key={index}>
                                                        <td>{item.Name || "N/A"}</td>
                                                        <td>{item.Debtor || "N/A"}</td>
                                                        <td>{item.Status || "N/A"}</td>
                                                        <td>{item.Type || "N/A"}</td>
                                                        <td>{item.BillingReference || "N/A"}</td>
                                                        <td>
                                                            <div className="d-flex align-items-center gap-2">
                                                                <button
                                                                    type="button"
                                                                    onClick={() => handleEditCostCenter(item)}
                                                                    className="btn btn-sm btn-primary"
                                                                    title="Edit Cost Center"
                                                                >
                                                                    <i className="feather-edit"></i>
                                                                </button>

                                                                {!isTemplateRow(item) && (
                                                                    <button
                                                                        type="button"
                                                                        onClick={() => handleOpenPrinterView(item)}
                                                                        className="btn btn-sm btn-dark"
                                                                        title="Manage Printers"
                                                                    >
                                                                        <Printer size={14} />
                                                                    </button>
                                                                )}
                                                            </div>
                                                        </td>
                                                    </tr>
                                                ))
                                            ) : (
                                                <tr>
                                                    <td colSpan="6" className="text-center">
                                                        No records found
                                                    </td>
                                                </tr>
                                            )}
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </>
                ) : (
                    <>
                        <div className="page-header">
                            <div className="add-item d-flex align-items-center">
                                <button
                                    type="button"
                                    className="btn btn-sm btn-outline-secondary me-3"
                                    onClick={handleClosePrinterView}
                                    title="Back"
                                >
                                    <ArrowLeft size={16} />
                                </button>

                                <div className="page-title">
                                    <h4>Cost Center Printers</h4>
                                    <h6>{selectedCostCenter?.Name || "Selected Cost Center"}</h6>
                                </div>
                            </div>

                            <div className="page-btn">
                                <Button
                                    variant="none"
                                    className="btn btn-added"
                                    onClick={handleOpenAddPrinterModal}
                                >
                                    <PlusCircle className="me-2" />
                                    Add New Printer
                                </Button>
                            </div>
                        </div>

                        <div className="card table-list-card">
                            <div className="card-body">
                                <div className="table-top">
                                    <div className="search-set">
                                        <div className="search-input">
                                            <input
                                                type="text"
                                                placeholder="Search printers"
                                                className="form-control"
                                                value={printerSearchTerm}
                                                onChange={(e) => setPrinterSearchTerm(e.target.value)}
                                            />
                                            <Link to="#" className="btn btn-searchset">
                                                <i data-feather="search" className="feather-search" />
                                            </Link>
                                        </div>
                                    </div>
                                </div>

                                <div className="table-responsive">
                                    <table className="table table-bordered table-striped">
                                        <thead>
    <tr>
        <th>Printer</th>
        <th>Invoice Type</th>
        <th>Tab Type</th>
        <th>Action</th>
    </tr>
</thead>
                                        <tbody>
    {printerLoading ? (
        <tr>
            <td colSpan="4" className="text-center">
                Loading printers...
            </td>
        </tr>
    ) : filteredPrinters.length > 0 ? (
        filteredPrinters.map((item, index) => (
            <tr key={item.CostCenterPrinterID || index}>
                <td>{getPrinterName(item)}</td>
                <td>{getInvoiceSlipCode(item)}</td>
                <td>{getTabSlipCode(item)}</td>
                <td>
                    <div className="d-flex align-items-center gap-2">
                        <button
                            type="button"
                            onClick={() => handleOpenEditPrinterModal(item)}
                            className="btn btn-sm btn-primary"
                            title="Edit Printer Link"
                        >
                            <i className="feather-edit"></i>
                        </button>
                    </div>
                </td>
            </tr>
        ))
    ) : (
        <tr>
            <td colSpan="4" className="text-center">
                No printers found
            </td>
        </tr>
    )}
</tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </>
                )}
            </div>

            <CostCenterForm
                costTypeList={costTypeList}
                debtorList={debtorList}
                onSubmitCostCenter={handleAddCostCenter}
                showModel={showModel}
                handleClose={handleClose}
                data={selectedData}
                statusList={statusList}
            />

            <Modal
                show={showPrinterFormModal}
                onHide={handleClosePrinterFormModal}
                centered
            >
                <Modal.Header closeButton>
                    <Modal.Title>
                        {selectedPrinterRow ? "Edit Printer Link" : "Add Printer Link"}
                    </Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <Form.Group className="mb-3">
                        <Form.Label>Printer</Form.Label>
                        <Form.Select
                            value={printerFormData.FK_PrinterID}
                            onChange={(e) =>
                                setPrinterFormData((prev) => ({
                                    ...prev,
                                    FK_PrinterID: e.target.value,
                                }))
                            }
                        >
                            <option value="">Select Printer</option>
                            {printerOptions.map((printer) => (
                                <option key={printer.PrinterID} value={printer.PrinterID}>
                                    {printer.PrinterName}
                                </option>
                            ))}
                        </Form.Select>
                    </Form.Group>

                    <Form.Group className="mb-3">
    <Form.Label>Invoice Slip Type</Form.Label>
    <Form.Select
        value={printerFormData.FK_InvoiceSlipTypeID}
        onChange={(e) =>
            setPrinterFormData((prev) => ({
                ...prev,
                FK_InvoiceSlipTypeID: e.target.value,
            }))
        }
    >
        <option value="">Select Invoice Slip Type</option>
        {invoiceSlipTypeOptions.map((item) => (
            <option key={item.SlipTypeID} value={item.SlipTypeID}>
                {item.SlipCode}
            </option>
        ))}
    </Form.Select>
</Form.Group>

                    <Form.Group>
    <Form.Label>Tab Slip Type</Form.Label>
    <Form.Select
        value={printerFormData.FK_TabSlipTypeID}
        onChange={(e) =>
            setPrinterFormData((prev) => ({
                ...prev,
                FK_TabSlipTypeID: e.target.value,
            }))
        }
    >
        <option value="">Select Tab Slip Type</option>
        {tabSlipTypeOptions.map((item) => (
            <option key={item.SlipTypeID} value={item.SlipTypeID}>
                {item.SlipCode}
            </option>
        ))}
    </Form.Select>
</Form.Group>
                </Modal.Body>

                <Modal.Footer>
                    <Button variant="light" onClick={handleClosePrinterFormModal}>
                        Cancel
                    </Button>
                    <Button
                        variant="primary"
                        onClick={handleSavePrinter}
                        disabled={
    savingPrinter ||
    !printerFormData.FK_PrinterID
}
                    >
                        {savingPrinter ? "Saving..." : "Save"}
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default CostCenter;