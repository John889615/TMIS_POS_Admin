import React, { useEffect, useRef, useState, useMemo } from "react";
import Swal from "sweetalert2";

import {
  getAllMenu,
  newMenu,
  updateMenu,
  copyMenu,
} from "../../services/menu/menuService";

import { getAllDebtors } from "../../services/debtors/debtors";
import { getAllCostCenter } from "../../services/debtors/costCenter";
import { getAllSlipPrinter } from "../../services/entityData/slipPrinter";

import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { PlusCircle } from "react-feather";
import { useSelector, useDispatch } from "react-redux";

import MenuForm from "../../core/modals/menu/menuFormModel";
import CopyMenuForm from "../../core/modals/menu/copyMenuFormModel";

/**
 * ✅ Tries to extract the debtor ID from whatever shape your debtor object is.
 * Adjust this if your API returns a different key.
 */
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
  const dispatch = useDispatch(); // optional if you want to set redux debtor
  const navigate = useNavigate();

  const debtorIdFromRedux = useSelector((state) => state.selectedDebtorStore);

  const [listData, setListData] = useState([]);
  const [debtorList, setDebtorList] = useState([]);
  const [costCenterList, setCostCenterList] = useState([]);
  const [slipPrinterList, setSlipPrinterList] = useState([]);

  const [showModel, setModelShow] = useState(false);
  const [selectedData, setSelectedData] = useState(null);

  const [showCopyModel, setCopyModelShow] = useState(false);

  const [searchTerm, setSearchTerm] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const recordsPerPage = 10;

  // ✅ Parent holds a reset function provided by MenuForm
  const menuFormResetRef = useRef(null);

  /**
   * ✅ Resolve which debtorId to actually use:
   * - If redux has a valid debtor id -> use it
   * - Else if we have debtors loaded -> use the first debtor's id
   * - Else -> null (until we load debtors)
   */
  const resolvedDebtorId = useMemo(() => {
    const redux = Number(debtorIdFromRedux);
    if (Number.isFinite(redux) && redux > 0) return redux;

    if (debtorList && debtorList.length > 0) {
      const firstId = getDebtorIdValue(debtorList[0]);
      return firstId;
    }

    return null;
  }, [debtorIdFromRedux, debtorList]);

  /**
   * ✅ Load supporting lists first (debtors/cost/printers).
   * Once debtors are loaded, resolvedDebtorId will become the first debtor ID
   * if redux was null.
   */
  useEffect(() => {
    (async () => {
      try {
        const d = await getAllDebtors();
        setDebtorList(Array.isArray(d) ? d : []);

        const cost = await getAllCostCenter();
        setCostCenterList(Array.isArray(cost) ? cost : []);

        const printers = await getAllSlipPrinter();
        setSlipPrinterList(Array.isArray(printers) ? printers : []);
      } catch (err) {
        console.error("Failed to load debtor/cost/printers:", err?.message || err);
      }
    })();
  }, []);

  /**
   * ✅ When resolvedDebtorId changes, load menus for that debtor.
   * Also reset paging when debtor changes.
   */
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

    // OPTIONAL: If you want redux to be set automatically when it was null,
    // dispatch it here. You must replace `setSelectedDebtorStore` with your actual action.
    //
    // if (!debtorIdFromRedux || Number(debtorIdFromRedux) <= 0) {
    //   dispatch(setSelectedDebtorStore(resolvedDebtorId));
    // }
  }, [resolvedDebtorId]); // intentionally not depending on debtorIdFromRedux

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

  // ✅ this will handle BOTH add + edit
  const handleAddProduct = async (data) => {
    try {
      const id = data?.MenuID ?? data?.POS_MenuID;

      let response;
      if (id) {
        response = await updateMenu({ ...data, MenuID: id });
      } else {
        response = await newMenu(data);
      }

      // Your service returns API error payload instead of throwing.
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

        // ✅ Keep modal OPEN, clear ALL inputs
        setSelectedData(null);
        if (menuFormResetRef.current) menuFormResetRef.current();
        return;
      }

      // ✅ success
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

      // ✅ Keep modal OPEN, clear ALL inputs
      setSelectedData(null);
      if (menuFormResetRef.current) menuFormResetRef.current();
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

  return (
    <div className="page-wrapper">
      <div className="content">
        <div className="page-header">
          <div className="add-item d-flex">
            <div className="page-title">
              <h4>All Menu</h4>
              <h6>Manage Your Menu</h6>
              <div className="text-muted" style={{ fontSize: 12 }}>
                Active Debtor ID: {resolvedDebtorId ?? "Loading..."}
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
      </div>

      {/* ✅ Menu Modal */}
      {showModel && (
        <MenuForm
          onSubmit={handleAddProduct}
          showModel={showModel}
          handleClose={handleClose}
          data={selectedData}
          onRegisterReset={(resetFn) => (menuFormResetRef.current = resetFn)}
        />
      )}

      {/* ✅ Copy Menu Modal */}
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
    </div>
  );
};

export default MenuPage;