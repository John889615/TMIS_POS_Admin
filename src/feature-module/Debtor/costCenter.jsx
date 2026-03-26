import React, { useState, useEffect } from "react";
import {
    getAllCostCenter,
    getAllCostCenterTypes,
    newCostCenter,
    updateCostCenter,
    getCostCenterPrinters,
    toggleCostCenterPrinter,
} from "../../services/debtors/costCenter";
import { getAllDebtors } from "../../services/debtors/debtors";
import { getAllStatus } from "../../services/entityData/status";
import { Button, Modal } from "react-bootstrap";
import { Link } from "react-router-dom";
import {
    PlusCircle,
    Printer,
    Link as LinkIcon,
    XCircle,
} from "react-feather";
import CostCenterForm from "../../core/modals/debtors/costCenterFormModel";
import { getAllSlipPrinter } from "../../services/entityData/slipPrinter";

const CostCenter = () => {
    const [listData, setListData] = useState([]);
    const [costTypeList, setCostTypeList] = useState([]);
    const [statusList, setStatusList] = useState([]);
    const [debtorList, setDebtorList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [showModel, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);

    const [showPrinterModal, setShowPrinterModal] = useState(false);
    const [selectedCostCenter, setSelectedCostCenter] = useState(null);
    const [printerSearch, setPrinterSearch] = useState("");
    const [printerLoading, setPrinterLoading] = useState(false);
    const [printerActionLoadingId, setPrinterActionLoadingId] = useState(null);

    const [printerList, setPrinterList] = useState([]);

    useEffect(() => {
        fetchRecords();
    }, []);

   const fetchRecords = async () => {
    try {
        const data = await getAllCostCenter();
        console.log("getAllCostCenter result", data);
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

const loadAllPrinters = async () => {
    try {
        const printers = await getAllSlipPrinter();

        const mappedPrinters = Array.isArray(printers)
            ? printers.map((printer) => ({
                  ...printer,
                  PrinterID:
                      printer.PrinterID ??
                      printer.SlipPrinterID ??
                      printer.ID,
                  PrinterName:
                      printer.PrinterName ??
                      printer.Name ??
                      printer.Description ??
                      "Unnamed Printer",
                  IsLinked: false,
              }))
            : [];

        setPrinterList(mappedPrinters);
    } catch (err) {
        console.error("Failed to load printer master list:", err);
        setPrinterList([]);
    }
};

    const filteredData = listData.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    const filteredPrinters = printerList.filter((printer) =>
        (printer.PrinterName || "").toLowerCase().includes(printerSearch.toLowerCase())
    );

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true);
    };

    const handleClose = () => setModelShow(false);

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

    const loadLinkedPrinters = async (costCenterId) => {
    try {
        setPrinterLoading(true);

        const [allPrinters, response] = await Promise.all([
            getAllSlipPrinter(),
            getCostCenterPrinters({
                FK_CostCenterID: costCenterId,
            }),
        ]);

        const linkedRows = Array.isArray(response?.Data) ? response.Data : [];
        const linkedPrinterIds = new Set(
            linkedRows
                .map((x) => x.FK_PrinterID)
                .filter((x) => x !== null && x !== undefined)
        );

        const mappedPrinters = Array.isArray(allPrinters)
            ? allPrinters.map((printer) => {
                  const printerId =
                      printer.PrinterID ??
                      printer.SlipPrinterID ??
                      printer.ID;

                  return {
                      ...printer,
                      PrinterID: printerId,
                      PrinterName:
                          printer.PrinterName ??
                          printer.Name ??
                          printer.Description ??
                          "Unnamed Printer",
                      IsLinked: linkedPrinterIds.has(printerId),
                  };
              })
            : [];

        setPrinterList(mappedPrinters);
    } catch (err) {
        console.error("Failed to load linked printers:", err);
        setPrinterList([]);
    } finally {
        setPrinterLoading(false);
    }
};

   const handleOpenPrinterModal = async (record) => {
    console.log("selected cost center row", record);

    if (!record?.CostCenterID) {
        console.error("Invalid cost center row passed to printer modal.", record);
        return;
    }

    setSelectedCostCenter(record);
    setPrinterSearch("");
    setShowPrinterModal(true);

    await loadLinkedPrinters(record.CostCenterID);
};

    const handleClosePrinterModal = () => {
        setShowPrinterModal(false);
        setSelectedCostCenter(null);
        setPrinterSearch("");
        setPrinterActionLoadingId(null);
    };

    const handleTogglePrinterLink = async (printer) => {
    if (!selectedCostCenter?.CostCenterID) {
        console.error("No valid CostCenterID found on selected cost center.", selectedCostCenter);
        return;
    }

    const payload = {
        FK_CostCenterID: selectedCostCenter.CostCenterID,
        FK_PrinterID: printer.PrinterID,
    };

    console.log("toggle payload", payload);

    try {
        setPrinterActionLoadingId(printer.PrinterID);

        const response = await toggleCostCenterPrinter(payload);
        const returnedRow = response?.Data || null;

        setPrinterList((prev) =>
            prev.map((item) =>
                item.PrinterID === printer.PrinterID
                    ? {
                          ...item,
                          IsLinked: !!returnedRow?.CostCenterPrinterID,
                      }
                    : item
            )
        );
    } catch (err) {
        console.error("Failed to toggle printer link:", err);
    } finally {
        setPrinterActionLoadingId(null);
    }
};

    return (
        <div className="page-wrapper">
            <div className="content">
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
                                    <Link to className="btn btn-searchset">
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
                                                <td>{item.Type ? "Yes" : "No"}</td>
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

                                                        <button
                                                            type="button"
                                                            onClick={() => handleOpenPrinterModal(item)}
                                                            className="btn btn-sm btn-dark"
                                                            title="Link / Unlink Printers"
                                                        >
                                                            <Printer size={14} />
                                                        </button>
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

            <Modal show={showPrinterModal} onHide={handleClosePrinterModal} size="lg" centered>
                <Modal.Header closeButton className="border-0 pb-0">
                    <div className="w-100">
                        <h4 className="mb-1">Printer Linking</h4>
                        <h6 className="text-muted mb-0">
                            {selectedCostCenter?.Name || "Cost Center"}
                        </h6>
                    </div>
                </Modal.Header>

                <Modal.Body className="pt-3">
                    <div className="card table-list-card mb-0">
                        <div className="card-body">
                            <div className="table-top mb-3">
                                <div className="search-set">
                                    <div className="search-input">
                                        <input
                                            type="text"
                                            placeholder="Search printers..."
                                            className="form-control"
                                            value={printerSearch}
                                            onChange={(e) => setPrinterSearch(e.target.value)}
                                        />
                                        <Link to className="btn btn-searchset">
                                            <i data-feather="search" className="feather-search" />
                                        </Link>
                                    </div>
                                </div>
                            </div>

                            <div className="table-responsive">
                                <table className="table table-bordered table-striped mb-0">
                                    <thead>
                                        <tr>
                                            <th>Printer Name</th>
                                            <th style={{ width: "70px" }}>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {printerLoading ? (
                                            <tr>
                                                <td colSpan="2" className="text-center">
                                                    Loading printers...
                                                </td>
                                            </tr>
                                        ) : filteredPrinters.length > 0 ? (
                                            filteredPrinters.map((printer) => {
                                                const isBusy = printerActionLoadingId === printer.PrinterID;

                                                return (
                                                    <tr key={printer.PrinterID}>
                                                        <td>{printer.PrinterName}</td>
                                                        <td>
                                                            <button
                                                                type="button"
                                                                className={`btn btn-sm d-inline-flex align-items-center justify-content-center ${
                                                                    printer.IsLinked
                                                                        ? "btn-outline-danger"
                                                                        : "btn-outline-success"
                                                                }`}
                                                                onClick={() => handleTogglePrinterLink(printer)}
                                                                title={printer.IsLinked ? "Unlink Printer" : "Link Printer"}
                                                                style={{ minWidth: "42px", height: "32px" }}
                                                                disabled={isBusy}
                                                            >
                                                                {printer.IsLinked ? (
                                                                    <XCircle size={14} />
                                                                ) : (
                                                                    <LinkIcon size={14} />
                                                                )}
                                                            </button>
                                                        </td>
                                                    </tr>
                                                );
                                            })
                                        ) : (
                                            <tr>
                                                <td colSpan="2" className="text-center">
                                                    No printers found
                                                </td>
                                            </tr>
                                        )}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </Modal.Body>

                <Modal.Footer className="border-0 pt-2">
                    <Button variant="light" onClick={handleClosePrinterModal}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default CostCenter;