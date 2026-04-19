import React, { useEffect, useRef, useState, useMemo } from "react";
import Swal from "sweetalert2";

import {
  getAllMenu,
  newMenu,
  updateMenu,
  copyMenu,
  updateDebtorMenu,
  addDebtorMenuPrinter,
  updateDebtorMenuPrinter,
} from "../../services/menu/menuService";
import { getAllSlipPrinter, getAllSlipTypes } from "../../services/entityData/slipPrinter";
import { getAllDebtors } from "../../services/debtors/debtors";
import { getAllCostCenter } from "../../services/debtors/costCenter";

import { Button, Modal, Form } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { PlusCircle, ArrowLeft, Printer } from "react-feather";
import { useSelector, useDispatch } from "react-redux";

import MenuForm from "../../core/modals/menu/menuFormModel";
import CopyMenuForm from "../../core/modals/menu/copyMenuFormModel";

function getDebtorIdValue(d) {
  if (!d) return null;

  const candidates = [
    d.DebtorID,
    d.POS_DebtorID,
    d.EntityID,
    d.ID,
    d.Id,
    d.DebtorId,
    d.pos_DebtorID,
  ];

  const found = candidates.find((x) => x !== undefined && x !== null && x !== "");
  const n = Number(found);
  return Number.isFinite(n) && n > 0 ? n : null;
}

const MenuPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const debtorIdFromRedux = useSelector((state) => state.selectedDebtorStore);

  const [listData, setListData] = useState([]);
  const [debtorList, setDebtorList] = useState([]);
  const [costCenterList, setCostCenterList] = useState([]);
  const [slipPrinterList, setSlipPrinterList] = useState([]);
const [slipTypeList, setSlipTypeList] = useState([]);
  const [showModel, setModelShow] = useState(false);
  const [selectedData, setSelectedData] = useState(null);

  const [showCopyModel, setCopyModelShow] = useState(false);

  const [searchTerm, setSearchTerm] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const recordsPerPage = 10;

  const menuFormResetRef = useRef(null);

  const [showPrinterView, setShowPrinterView] = useState(false);
  const [selectedMenu, setSelectedMenu] = useState(null);
  const [printerSearchTerm, setPrinterSearchTerm] = useState("");

  // For now this is local state until you share the list endpoint
  const [menuPrinterRows, setMenuPrinterRows] = useState([]);

  const [showPrinterModal, setShowPrinterModal] = useState(false);
  const [selectedPrinterRow, setSelectedPrinterRow] = useState(null);
  const [savingPrinter, setSavingPrinter] = useState(false);

  const [printerFormData, setPrinterFormData] = useState({
    DebtorMenuPrinterID: 0,
    FK_DebtorMenuID: 0,
    FK_PrinterID: "",
    FK_OrderSlipTypeID: "",
  });

  const resolvedDebtorId = useMemo(() => {
    const redux = Number(debtorIdFromRedux);
    if (Number.isFinite(redux) && redux > 0) return redux;

    if (debtorList && debtorList.length > 0) {
      const firstId = getDebtorIdValue(debtorList[0]);
      return firstId;
    }

    return null;
  }, [debtorIdFromRedux, debtorList]);

useEffect(() => {
  (async () => {
    try {
      const d = await getAllDebtors();
      setDebtorList(Array.isArray(d) ? d : []);

      const cost = await getAllCostCenter();
      setCostCenterList(Array.isArray(cost) ? cost : []);

      const printers = await getAllSlipPrinter();
      setSlipPrinterList(Array.isArray(printers) ? printers : []);

      const slipTypes = await getAllSlipTypes();
      setSlipTypeList(Array.isArray(slipTypes) ? slipTypes : []);
    } catch (err) {
      console.error("Failed to load debtor/cost/printers/slip types:", err?.message || err);
    }
  })();
}, []);

  useEffect(() => {
    if (!resolvedDebtorId) return;

    setCurrentPage(1);

    (async () => {
      try {
        const data = await getAllMenu(resolvedDebtorId);
        setListData(Array.isArray(data) ? data : []);
      } catch (err) {
        console.error("Failed to load menus:", err?.message || err);
        setListData([]);
      }
    })();
  }, [resolvedDebtorId]);

  const filteredData = (listData || []).filter((item) =>
    Object.values(item || {}).some(
      (value) =>
        typeof value === "string" &&
        value.toLowerCase().includes(searchTerm.toLowerCase())
    )
  );

  const totalPages = Math.max(1, Math.ceil(filteredData.length / recordsPerPage));
  const startIndex = (currentPage - 1) * recordsPerPage;
  const currentData = filteredData.slice(startIndex, startIndex + recordsPerPage);

  const filteredPrinterRows = (menuPrinterRows || []).filter((item) =>
    Object.values(item || {}).some(
      (value) =>
        typeof value === "string" &&
        value.toLowerCase().includes(printerSearchTerm.toLowerCase())
    )
  );

  const printerOptions = useMemo(() => {
    return (slipPrinterList || []).map((printer) => ({
      PrinterID: printer.PrinterID ?? printer.SlipPrinterID ?? printer.ID ?? 0,
      PrinterName:
        printer.PrinterName ??
        printer.Name ??
        printer.Description ??
        "Unnamed Printer",
    }));
  }, [slipPrinterList]);

const orderSlipTypeOptions = useMemo(() => {
  return (slipTypeList || []).map((item) => ({
    OrderSlipTypeID: item.SlipTypeID ?? 0,
    OrderSlipTypeName: item.SlipType ?? "Unnamed Slip Type",
    SlipCode: item.SlipCode ?? "",
    Description: item.Description ?? "",
  }));
}, [slipTypeList]);

  const goToPage = (page) => {
    if (page >= 1 && page <= totalPages) setCurrentPage(page);
  };

  const handleShow = () => {
    setSelectedData(null);
    setModelShow(true);
  };

  const handleClose = () => {
    setModelShow(false);
    setSelectedData(null);
  };

  const refreshMenus = async () => {
    if (!resolvedDebtorId) return;
    const data = await getAllMenu(resolvedDebtorId);
    setListData(Array.isArray(data) ? data : []);
  };

  const isLinkedMenu = (menu) => {
    if (!menu) return false;

    if (menu.SourceType && menu.SourceType !== "Global") {
      return true;
    }

    if (menu.FK_CostCenterID || menu.CostCenterID) {
      return true;
    }

    if (menu.FK_LocationID || menu.LocationID) {
      return true;
    }

    return false;
  };

  const isTemplateMenu = (menu) => {
    if (!menu) return false;

    if (menu.SourceType === "Global") return true;
    if (!menu.Location || String(menu.Location).trim() === "") return true;
    if (String(menu.Location).toLowerCase() === "template") return true;
    if (menu.IsTemplate === true) return true;
    if (menu.Template === true) return true;

    return false;
  };

  const handleAddProduct = async (formData) => {
    try {
      const id =
        formData?.MenuID ??
        formData?.POS_MenuID ??
        selectedData?.MenuID ??
        selectedData?.POS_MenuID;

      const editing = !!id;

      let response;

      if (editing) {
        const payload = {
          ...selectedData,
          ...formData,
          MenuID: id,
        };

        if (isLinkedMenu(selectedData)) {
          response = await updateDebtorMenu(payload);
        } else {
          response = await updateMenu(payload);
        }
      } else {
        response = await newMenu(formData);
      }

      if (response?.Success === false) {
        const msg =
          response?.Messages?.[0] ||
          response?.Errors?.[0] ||
          "Menu could not be saved.";

        await Swal.fire({
          icon: "error",
          title: "Could not save menu",
          text: msg,
          confirmButtonText: "OK",
          allowOutsideClick: false,
        });

        setSelectedData(editing ? selectedData : null);

        if (!editing && menuFormResetRef.current) {
          menuFormResetRef.current();
        }

        return;
      }

      await refreshMenus();
      setModelShow(false);
      setSelectedData(null);
    } catch (err) {
      console.error("Error saving menu:", err);

      await Swal.fire({
        icon: "error",
        title: "Unexpected error",
        text: err?.message || "Something went wrong.",
        confirmButtonText: "OK",
        allowOutsideClick: false,
      });

      if (!selectedData && menuFormResetRef.current) {
        menuFormResetRef.current();
      }
    }
  };

  const handleEditProduct = (record) => {
    setSelectedData(record);
    setModelShow(true);
  };

  const handleMenuClick = (menuId, type) => {
    if (type === "Global") navigate(`/menu-tree/${menuId}`);
    else navigate(`/menu-tree-camp/${menuId}`);
  };

  const HandleCopyMenu = (record) => {
    setSelectedData(record);
    setCopyModelShow(true);
  };

  const handleCopyClose = () => {
    setCopyModelShow(false);
    setSelectedData(null);
  };

  const handleAddCopyMenu = async (data) => {
    try {
      const response = await copyMenu(data);

      if (response?.Success) {
        setCopyModelShow(false);
        setSelectedData(null);
        await refreshMenus();
      } else {
        const msg =
          response?.Messages?.[0] ||
          response?.Errors?.[0] ||
          "Copy failed.";

        await Swal.fire({
          icon: "error",
          title: "Copy Menu Error",
          text: msg,
          confirmButtonText: "OK",
          allowOutsideClick: false,
        });
      }
    } catch (err) {
      console.error("Error copying menu:", err?.message || err);

      await Swal.fire({
        icon: "error",
        title: "Unexpected error",
        text: err?.message || "Something went wrong.",
        confirmButtonText: "OK",
        allowOutsideClick: false,
      });
    }
  };

  const handleOpenPrinterView = (menu) => {
    setSelectedMenu(menu);
    setPrinterSearchTerm("");
    setShowPrinterView(true);

    // Until list endpoint exists, keep local / empty state
    setMenuPrinterRows([]);
  };

  const handleClosePrinterView = () => {
    setShowPrinterView(false);
    setSelectedMenu(null);
    setPrinterSearchTerm("");
    setMenuPrinterRows([]);
  };

  const handleOpenAddPrinterModal = () => {
    const debtorMenuId = selectedMenu?.DebtorMenuID ?? selectedMenu?.MenuID ?? 0;

    setSelectedPrinterRow(null);
    setPrinterFormData({
      DebtorMenuPrinterID: 0,
      FK_DebtorMenuID: debtorMenuId,
      FK_PrinterID: "",
      FK_OrderSlipTypeID: "",
    });
    setShowPrinterModal(true);
  };

  const handleOpenEditPrinterModal = (row) => {
    const debtorMenuId =
      row?.FK_DebtorMenuID ??
      selectedMenu?.DebtorMenuID ??
      selectedMenu?.MenuID ??
      0;

    setSelectedPrinterRow(row);
    setPrinterFormData({
      DebtorMenuPrinterID: row?.DebtorMenuPrinterID ?? 0,
      FK_DebtorMenuID: debtorMenuId,
      FK_PrinterID: row?.FK_PrinterID ?? "",
      FK_OrderSlipTypeID: row?.FK_OrderSlipTypeID ?? "",
    });
    setShowPrinterModal(true);
  };

  const handleClosePrinterModal = () => {
    setShowPrinterModal(false);
    setSelectedPrinterRow(null);
    setSavingPrinter(false);
    setPrinterFormData({
      DebtorMenuPrinterID: 0,
      FK_DebtorMenuID: 0,
      FK_PrinterID: "",
      FK_OrderSlipTypeID: "",
    });
  };

  const getPrinterName = (printerId) => {
    const match = printerOptions.find(
      (x) => Number(x.PrinterID) === Number(printerId)
    );

    return match?.PrinterName || "N/A";
  };

  const getOrderSlipTypeName = (orderSlipTypeId) => {
    const match = orderSlipTypeOptions.find(
      (x) => Number(x.OrderSlipTypeID) === Number(orderSlipTypeId)
    );

    return match?.OrderSlipTypeName || "N/A";
  };

  const handleSaveMenuPrinter = async () => {
    try {
      if (!printerFormData.FK_DebtorMenuID || !printerFormData.FK_PrinterID || !printerFormData.FK_OrderSlipTypeID) {
        await Swal.fire({
          icon: "warning",
          title: "Missing fields",
          text: "Please select a printer and order slip type.",
          confirmButtonText: "OK",
        });
        return;
      }

      setSavingPrinter(true);

      const payload = {
        DebtorMenuPrinterID: Number(printerFormData.DebtorMenuPrinterID || 0),
        FK_DebtorMenuID: Number(printerFormData.FK_DebtorMenuID),
        FK_PrinterID: Number(printerFormData.FK_PrinterID),
        FK_OrderSlipTypeID: Number(printerFormData.FK_OrderSlipTypeID),
      };

      let response;

      if (payload.DebtorMenuPrinterID > 0) {
        response = await updateDebtorMenuPrinter(payload);
      } else {
        response = await addDebtorMenuPrinter({
          FK_DebtorMenuID: payload.FK_DebtorMenuID,
          FK_PrinterID: payload.FK_PrinterID,
          FK_OrderSlipTypeID: payload.FK_OrderSlipTypeID,
        });
      }

      if (response?.Success === false) {
        const msg =
          response?.Messages?.[0] ||
          response?.Errors?.[0] ||
          "Could not save menu printer.";

        await Swal.fire({
          icon: "error",
          title: "Save failed",
          text: msg,
          confirmButtonText: "OK",
        });

        return;
      }

      if (payload.DebtorMenuPrinterID > 0) {
        setMenuPrinterRows((prev) =>
          prev.map((item) =>
            Number(item.DebtorMenuPrinterID) === payload.DebtorMenuPrinterID
              ? { ...item, ...payload }
              : item
          )
        );
      } else {
        const newId =
          response?.Data?.DebtorMenuPrinterID ??
          response?.Data?.MenuPrinterID ??
          Date.now();

        setMenuPrinterRows((prev) => [
          ...prev,
          {
            DebtorMenuPrinterID: newId,
            FK_DebtorMenuID: payload.FK_DebtorMenuID,
            FK_PrinterID: payload.FK_PrinterID,
            FK_OrderSlipTypeID: payload.FK_OrderSlipTypeID,
          },
        ]);
      }

      handleClosePrinterModal();
    } catch (err) {
      console.error("Failed to save menu printer:", err);

      await Swal.fire({
        icon: "error",
        title: "Unexpected error",
        text: err?.message || "Something went wrong.",
        confirmButtonText: "OK",
      });
    } finally {
      setSavingPrinter(false);
    }
  };

  return (
    <div className="page-wrapper">
      <div className="content">
        {!showPrinterView ? (
          <>
            <div className="page-header">
              <div className="add-item d-flex">
                <div className="page-title">
                  <h4>All Menu</h4>
                  <h6>Manage Your Menu</h6>
                  <div className="text-muted" style={{ fontSize: 12 }}>
                    {/* Active Debtor ID: {resolvedDebtorId ?? "Loading..."} */}
                  </div>
                </div>
              </div>

              <div className="page-btn">
                <Button variant="none" className="btn btn-added" onClick={handleShow}>
                  <PlusCircle className="me-2" />
                  Add New Menu
                </Button>
              </div>
            </div>

            <div className="card table-list-card">
              <div className="card-body">
                <div className="table-top d-flex justify-content-between align-items-center">
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
                        <th>Menu Name</th>
                        <th>Location</th>
                        <th>Is Active</th>
                        <th>Action</th>
                      </tr>
                    </thead>

                    <tbody>
                      {currentData.length > 0 ? (
                        currentData.map((item, index) => (
                          <tr key={index}>
                            <td
                              style={{ cursor: "pointer", textDecoration: "underline" }}
                              onClick={() => handleMenuClick(item.MenuID, item.SourceType)}
                            >
                              {item.MenuName || "N/A"}
                            </td>
                            <td>{item.Location ? item.Location : "Template"}</td>
                            <td>{item.IsActive ? "Yes" : "No"}</td>
                            <td>
                              <button
                                type="button"
                                onClick={() => handleEditProduct(item)}
                                className="btn btn-sm btn-primary me-2"
                              >
                                <i className="feather-edit"></i>
                              </button>
                              
                              {!isTemplateMenu(item) && (
                                <button
                                  type="button"
                                  onClick={() => handleOpenPrinterView(item)}
                                  className="btn btn-sm btn-secondary me-2"
                                  title="Menu Printers"
                                >
                                  <Printer size={14} />
                                </button>
                              )}
                             

                              {item.SourceType === "Global" && (
                                <button
                                  type="button"
                                  onClick={() => HandleCopyMenu(item)}
                                  className="btn btn-sm btn-primary me-2"
                                >
                                  Copy Menu
                                </button>
                              )}
                            </td>
                          </tr>
                        ))
                      ) : (
                        <tr>
                          <td colSpan="4" className="text-center">
                            {resolvedDebtorId ? "No records found" : "Loading..."}
                          </td>
                        </tr>
                      )}
                    </tbody>
                  </table>

                  {totalPages > 1 && (
                    <div className="d-flex justify-content-between align-items-center mt-3">
                      <span>
                        Page {currentPage} of {totalPages}
                      </span>

                      <div>
                        {Array.from({ length: totalPages }, (_, i) => (
                          <Button
                            key={i}
                            variant={currentPage === i + 1 ? "primary" : "light"}
                            size="sm"
                            className="mx-1"
                            onClick={() => goToPage(i + 1)}
                          >
                            {i + 1}
                          </Button>
                        ))}
                      </div>

                      <div>
                        <Button
                          variant="secondary"
                          size="sm"
                          disabled={currentPage === 1}
                          onClick={() => setCurrentPage(currentPage - 1)}
                        >
                          Previous
                        </Button>
                        <Button
                          variant="secondary"
                          size="sm"
                          className="ms-2"
                          disabled={currentPage === totalPages}
                          onClick={() => setCurrentPage(currentPage + 1)}
                        >
                          Next
                        </Button>
                      </div>
                    </div>
                  )}
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
                  <h4>Menu Printers</h4>
                  <h6>{selectedMenu?.MenuName || "Selected Menu"}</h6>
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
                <div className="table-top d-flex justify-content-between align-items-center">
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
                        <th>Order Slip Type</th>
                        <th>Action</th>
                      </tr>
                    </thead>

                    <tbody>
                      {filteredPrinterRows.length > 0 ? (
                        filteredPrinterRows.map((item, index) => (
                          <tr key={item.DebtorMenuPrinterID || index}>
                            <td>{getPrinterName(item.FK_PrinterID)}</td>
                            <td>{getOrderSlipTypeName(item.FK_OrderSlipTypeID)}</td>
                            <td>
                              <button
                                type="button"
                                onClick={() => handleOpenEditPrinterModal(item)}
                                className="btn btn-sm btn-primary me-2"
                                title="Edit Menu Printer"
                              >
                                <i className="feather-edit"></i>
                              </button>
                            </td>
                          </tr>
                        ))
                      ) : (
                        <tr>
                          <td colSpan="3" className="text-center">
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

      {showModel && (
        <MenuForm
          onSubmit={handleAddProduct}
          showModel={showModel}
          handleClose={handleClose}
          data={selectedData}
          onRegisterReset={(resetFn) => (menuFormResetRef.current = resetFn)}
        />
      )}

      {showCopyModel && (
        <CopyMenuForm
          onSubmit={handleAddCopyMenu}
          showModel={showCopyModel}
          handleClose={handleCopyClose}
          data={selectedData}
          debtorList={debtorList}
          costCenterList={costCenterList}
          slipPrinterList={slipPrinterList}
        />
      )}

      <Modal show={showPrinterModal} onHide={handleClosePrinterModal} centered>
        <Modal.Header closeButton>
          <Modal.Title>
            {selectedPrinterRow ? "Edit Menu Printer" : "Add Menu Printer"}
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

          <Form.Group>
            <Form.Label>Order Slip Type</Form.Label>
            <Form.Select
              value={printerFormData.FK_OrderSlipTypeID}
              onChange={(e) =>
                setPrinterFormData((prev) => ({
                  ...prev,
                  FK_OrderSlipTypeID: e.target.value,
                }))
              }
            >
              <option value="">Select Order Slip Type</option>
              {orderSlipTypeOptions.map((item) => (
                <option key={item.OrderSlipTypeID} value={item.OrderSlipTypeID}>
                  {item.OrderSlipTypeName}
                </option>
              ))}
            </Form.Select>
          </Form.Group>
        </Modal.Body>

        <Modal.Footer>
          <Button variant="light" onClick={handleClosePrinterModal}>
            Cancel
          </Button>
          <Button
            variant="primary"
            onClick={handleSaveMenuPrinter}
            disabled={
              savingPrinter ||
              !printerFormData.FK_PrinterID ||
              !printerFormData.FK_OrderSlipTypeID
            }
          >
            {savingPrinter ? "Saving..." : "Save"}
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default MenuPage;