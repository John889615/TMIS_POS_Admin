import React, { useState, useEffect, useMemo } from "react";
import {
  getAllProducts,
  syncAllProducts,
  newProduct,
  updateProduct,
} from "../../services/product/product";
import { getAllProductCategory } from "../../services/product/productCategory";
import { getAllProductTypes } from "../../services/product/productType";
import { getAllUnits } from "../../services/product/units";
import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { PlusCircle } from "react-feather";
import ProductForm from "../../core/modals/products/productFormModel";

const withTimeout = (promise, ms, timeoutMessage = "Request timed out") => {
  let timeoutId;
  const timeoutPromise = new Promise((_, reject) => {
    timeoutId = setTimeout(() => reject(new Error(timeoutMessage)), ms);
  });

  return Promise.race([promise, timeoutPromise]).finally(() => {
    clearTimeout(timeoutId);
  });
};

const ProductPage = () => {
  const [listData, setListData] = useState([]);
  const [CategoryListData, setCategoryListData] = useState([]);
  const [typeListData, setTypeListData] = useState([]);
  const [unitListData, setUnitListData] = useState([]);
  const [showModel, setModelShow] = useState(false);
  const [selectedData, setSelectedData] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");
  const [currentPage, setCurrentPage] = useState(1);

  // ✅ Separate "sync" status vs "load" status
  const [syncing, setSyncing] = useState(false);
  const [syncStatus, setSyncStatus] = useState({ type: "idle", message: "" }); // idle|working|success|error
  const [loadStatus, setLoadStatus] = useState({ type: "idle", message: "" }); // idle|working|error

  const recordsPerPage = 10;
  const maxPageButtons = 5;
  const navigate = useNavigate();

  useEffect(() => {
    fetchRecords();
  }, []);

  useEffect(() => {
    setCurrentPage(1);
  }, [searchTerm]);

  const fetchRecords = async () => {
    setLoadStatus({ type: "working", message: "" });

    const results = await Promise.allSettled([
      getAllProducts(),
      getAllProductCategory(),
      getAllProductTypes(),
      getAllUnits(),
    ]);

    // NOTE: user’s original had bug here (productsRes missing),
    // keeping your structure but fixing safely.
    const [productsRes, catRes, typeRes, unitsRes] = results;

    const failures = [];
    const getMsg = (r) =>
      r?.reason?.message || r?.reason?.toString?.() || "Unknown error";

    if (productsRes.status === "fulfilled") {
      if (Array.isArray(productsRes.value)) setListData(productsRes.value);
      else
        failures.push(
          `Products: Expected a list but got (${typeof productsRes.value}). Check getAllProducts.`
        );
    } else {
      failures.push(`Products: ${getMsg(productsRes)}`);
    }

    if (catRes.status === "fulfilled") setCategoryListData(catRes.value);
    else failures.push(`Category: ${getMsg(catRes)}`);

    if (typeRes.status === "fulfilled") setTypeListData(typeRes.value);
    else failures.push(`Types: ${getMsg(typeRes)}`);

    if (unitsRes.status === "fulfilled") setUnitListData(unitsRes.value);
    else failures.push(`Units: ${getMsg(unitsRes)}`);

    if (failures.length) {
      console.error("FetchRecords partial failures:", failures);
      setLoadStatus({
        type: "error",
        message: `Some data failed to load: ${failures.join(" | ")}`,
      });
    } else {
      setLoadStatus({ type: "idle", message: "" });
    }
  };

  // ✅ Sync handler: true => SUCCESS, false/anything else => FAIL
  const handleSyncProducts = async () => {
    const SYNC_TIMEOUT_MS = 5 * 60 * 1000; // 5 minutes

    try {
      setSyncing(true);
      setSyncStatus({ type: "working", message: "Syncing products…" });

      const ok = await withTimeout(
        syncAllProducts(),
        SYNC_TIMEOUT_MS,
        "Sync is taking longer than expected. Please try again later."
      );

      if (ok !== true) {
        throw new Error("Sync did not complete (API did not return true).");
      }

      await fetchRecords();

      setSyncStatus({
        type: "success",
        message: "Products synced successfully.",
      });

      setTimeout(() => setSyncStatus({ type: "idle", message: "" }), 4000);
    } catch (err) {
      console.error("Sync failed:", err?.message || err);
      setSyncStatus({
        type: "error",
        message: err?.message || "Sync failed. Please try again.",
      });
    } finally {
      setSyncing(false);
    }
  };

  const filteredData = useMemo(() => {
    const term = searchTerm.toLowerCase();
    return listData.filter((item) =>
      Object.values(item).some(
        (value) =>
          typeof value === "string" && value.toLowerCase().includes(term)
      )
    );
  }, [listData, searchTerm]);

  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentData = filteredData.slice(indexOfFirstRecord, indexOfLastRecord);
  const totalPages = Math.ceil(filteredData.length / recordsPerPage);

  const goToPage = (page) => {
    if (page >= 1 && page <= totalPages) setCurrentPage(page);
  };

  let startPage = Math.max(1, currentPage - Math.floor(maxPageButtons / 2));
  let endPage = Math.min(totalPages, startPage + maxPageButtons - 1);
  if (endPage - startPage + 1 < maxPageButtons) {
    startPage = Math.max(1, endPage - maxPageButtons + 1);
  }

  const handleShow = () => {
    setSelectedData(null);
    setModelShow(true);
  };

  const handleClose = () => setModelShow(false);

  const handleAddProduct = async (data) => {
    try {
      if (data.POS_ProductID) await updateProduct(data);
      else await newProduct(data);

      await fetchRecords();
      setModelShow(false);
    } catch (err) {
      console.error("Error saving product:", err?.message || err);
    }
  };

  const handleEditProduct = (record) => {
    setSelectedData(record);
    setModelShow(true);
  };

  // ✅ Product actions use PRODUCT ID
  const handleRedirect = (e, item) => {
    const value = e.target.value;
    if (!value) return;
    navigate(`/${value}/${item.POS_ProductID}`);
    e.target.value = "";
  };

  const Pill = ({ type, message }) => {
    if (!message) return null;

    const base = "px-3 py-2 rounded d-inline-flex align-items-center gap-2";
    const map = {
      working: "bg-light text-dark border",
      success: "bg-success text-white",
      error: "bg-danger text-white",
    };

    return (
      <div className={`${base} ${map[type] || "bg-light"}`}>
        {type === "working" && (
          <span
            className="spinner-border spinner-border-sm"
            role="status"
            aria-hidden="true"
          />
        )}
        <span style={{ fontSize: 13 }}>{message}</span>
      </div>
    );
  };

  return (
    <div className="page-wrapper">
      <div className="content">
        <div className="page-header">
          <div className="add-item d-flex">
            <div className="page-title">
              <h4>Products</h4>
              <h6>Manage Your Product</h6>
            </div>
          </div>

          <div className="page-btn d-flex gap-2 align-items-center">
            {syncStatus.type !== "idle" ? (
              <Pill type={syncStatus.type} message={syncStatus.message} />
            ) : (
              <Pill type={loadStatus.type} message={loadStatus.message} />
            )}

            <Button
              variant="none"
              className="btn btn-secondary"
              onClick={handleSyncProducts}
              disabled={syncing}
              title="Sync all products from server"
            >
              <i className="feather-refresh-cw me-2"></i>
              {syncing ? "Syncing..." : "Sync Products"}
            </Button>

            <Button
              variant="none"
              className="btn btn-added"
              onClick={handleShow}
              disabled={syncing}
            >
              <PlusCircle className="me-2" />
              Add New Product
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
                    disabled={syncing}
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
                    <th>Description</th>
                    <th>Product Type</th>
                    <th>Unit</th>
                    <th>Default Unit</th>
                    <th>Category</th>
                    <th>Is Stock Tracked</th>
                    <th>SKU</th>
                    <th>Barcode</th>
                    <th>QrCode</th>
                    <th>Action</th>
                  </tr>
                </thead>
                <tbody>
                  {currentData.length > 0 ? (
                    currentData.map((item, index) => (
                      <tr key={index}>
                        <td>{item.Description || "N/A"}</td>
                        <td>{item.ProductType || "N/A"}</td>
                        <td>{item.Unit || "N/A"}</td>
                        <td>{item.DefaultUnit || "N/A"}</td>
                        <td>{item.ProductCategory || "N/A"}</td>
                        <td>{item.IsStockTracked ? "Yes" : "No"}</td>
                        <td>{item.SKU || "N/A"}</td>
                        <td>{item.Barcode || "N/A"}</td>
                        <td>{item.QrCode || "N/A"}</td>
                        <td>
                          <button
                            type="button"
                            onClick={() => handleEditProduct(item)}
                            className="btn btn-sm btn-primary me-2"
                            disabled={syncing}
                          >
                            <i className="feather-edit"></i>
                          </button>

                          {/* ✅ Only Product-level actions here */}
                          <select
                            className="form-select form-select-sm d-inline-block"
                            style={{ width: "140px" }}
                            onChange={(e) => handleRedirect(e, item)}
                            defaultValue=""
                            disabled={syncing}
                          >
                            <option value="" disabled>
                              Select Action
                            </option>
                            <option value="product-combination">Combination</option>
                            <option value="product-extra">Extra</option>
                          </select>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan="11" className="text-center">
                        No records found
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
                    {Array.from(
                      { length: endPage - startPage + 1 },
                      (_, i) => startPage + i
                    ).map((page) => (
                      <Button
                        key={page}
                        variant={currentPage === page ? "primary" : "light"}
                        size="sm"
                        className="mx-1"
                        onClick={() => goToPage(page)}
                        disabled={syncing}
                      >
                        {page}
                      </Button>
                    ))}
                  </div>

                  <div>
                    <Button
                      variant="secondary"
                      size="sm"
                      disabled={currentPage === 1 || syncing}
                      onClick={() => setCurrentPage(currentPage - 1)}
                    >
                      Previous
                    </Button>
                    <Button
                      variant="secondary"
                      size="sm"
                      className="ms-2"
                      disabled={currentPage === totalPages || syncing}
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

      {showModel && (
        <ProductForm
          onSubmit={handleAddProduct}
          showModel={showModel}
          handleClose={handleClose}
          data={selectedData}
          categoryList={CategoryListData}
          typeList={typeListData}
          unitList={unitListData}
        />
      )}
    </div>
  );
};

export default ProductPage;
