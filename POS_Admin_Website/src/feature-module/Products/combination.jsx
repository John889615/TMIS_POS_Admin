import React, { useState, useEffect, useRef } from "react";
import Swal from "sweetalert2";

import { getAllProducts } from "../../services/product/product";
import {
  getAllCombinationById,
  newCombination,
  updateCombination,
  removeCombination,
} from "../../services/product/combination";

import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { PlusCircle, Trash2 } from "react-feather";
import CombinationForm from "../../core/modals/products/combinationFormModel";
import { useParams } from "react-router-dom";

const CombinationPage = () => {
  const { id } = useParams(); // parent PRODUCT id
  const [listData, setListData] = useState([]);
  const [productListData, setProductListData] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [showModel, setModelShow] = useState(false);
  const [selectedData, setSelectedData] = useState(null);
  const navigate = useNavigate();

  // ✅ Parent holds reset function from modal
  const comboFormResetRef = useRef(null);

  useEffect(() => {
    fetchRecords();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  const fetchRecords = async () => {
    try {
      if (id) {
        const combos = await getAllCombinationById(id);
        setListData(Array.isArray(combos) ? combos : []);
      } else {
        setListData([]);
      }

      const products = await getAllProducts();
      setProductListData(Array.isArray(products) ? products : []);
    } catch (err) {
      console.error("Failed to load combinations:", err?.message || err);
      setListData([]);
      setProductListData([]);
    }
  };

  const filteredData = (listData || []).filter((item) =>
    Object.values(item || {}).some(
      (value) =>
        typeof value === "string" &&
        value.toLowerCase().includes(searchTerm.toLowerCase())
    )
  );

  const handleShow = () => {
    setSelectedData(null);
    setModelShow(true);
  };

  const handleClose = () => {
    setModelShow(false);
    setSelectedData(null);
  };

  const showApiError = async (title, response, fallback) => {
    const msg =
      response?.Messages?.[0] ||
      response?.Errors?.[0] ||
      response?.Message ||
      fallback ||
      "Something went wrong.";

    await Swal.fire({
      icon: "error",
      title: title || "Error",
      text: msg,
      confirmButtonText: "OK",
      allowOutsideClick: false,
    });
  };

  const handleAddProduct = async (data) => {
    try {
      let response;

      if (data?.ProductCombinationID) response = await updateCombination(data);
      else response = await newCombination(data);

      // ✅ Your services return an error payload (doesn't throw),
      // so treat Success === false as error.
      if (response?.Success === false) {
        await showApiError("Could not save combination", response, "Combination could not be saved.");

        // ✅ Keep modal open + clear
        setSelectedData(null);
        if (comboFormResetRef.current) comboFormResetRef.current();
        return;
      }

      // ✅ Success
      await fetchRecords();
      setModelShow(false);
      setSelectedData(null);
    } catch (err) {
      console.error("Error saving combination:", err?.message || err);

      await Swal.fire({
        icon: "error",
        title: "Unexpected error",
        text: err?.message || "Something went wrong.",
        confirmButtonText: "OK",
        allowOutsideClick: false,
      });

      // ✅ Keep modal open + clear
      setSelectedData(null);
      if (comboFormResetRef.current) comboFormResetRef.current();
    }
  };

  const handleEditProduct = (record) => {
    setSelectedData(record);
    setModelShow(true);
  };

  const handleDeleteCombination = async (row) => {
    const comboId = Number(row?.ProductCombinationID) || 0;
    if (!comboId) {
      await Swal.fire({
        icon: "error",
        title: "Delete failed",
        text: "Missing ProductCombinationID.",
        confirmButtonText: "OK",
      });
      return;
    }

    const confirm = await Swal.fire({
      icon: "warning",
      title: "Delete this combination?",
      text: "This action cannot be undone.",
      showCancelButton: true,
      confirmButtonText: "Yes, delete",
      cancelButtonText: "Cancel",
      allowOutsideClick: false,
    });

    if (!confirm.isConfirmed) return;

    try {
      const response = await removeCombination(comboId);

      if (response?.Success === false) {
        await showApiError("Delete failed", response, "Failed to delete combination.");
        return;
      }

      await fetchRecords();
    } catch (err) {
      await Swal.fire({
        icon: "error",
        title: "Delete failed",
        text: err?.response?.data?.Message || err?.message || "Failed to delete combination.",
        confirmButtonText: "OK",
        allowOutsideClick: false,
      });
    }
  };

  // Preparation/Substitution must use the COMBINATION PRODUCT ID (child)
  const handleComboRedirect = (e, comboRow) => {
    const value = e.target.value;
    if (!value) return;

    const comboProductId = Number(comboRow?.FK_ProductItemID) || 0;

    if (!comboProductId) {
      console.error("FK_ProductItemID missing on combination row:", comboRow);
      e.target.value = "";
      return;
    }

    navigate(`/${value}/${comboProductId}`);
    e.target.value = "";
  };

  return (
    <div className="page-wrapper">
      <div className="content">
        <div className="page-header">
          <div className="add-item d-flex">
            <div className="page-title">
              <h4>Combinations</h4>
              <h6>Manage Your Combination</h6>
            </div>
          </div>

          <div className="page-btn">
            <Button variant="none" className="btn btn-added" onClick={handleShow}>
              <PlusCircle className="me-2" />
              Add New Combination
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
                    <th>Product Item Name</th>
                    <th>Is Quantified</th>
                    <th>Quantity</th>
                    <th>Is Optional</th>
                    <th>Is ExtraCharge</th>
                    <th>Display Order</th>
                    <th>Action</th>
                  </tr>
                </thead>

                <tbody>
                  {filteredData.length > 0 ? (
                    filteredData.map((item, index) => (
                      <tr key={index}>
                        <td>{item.ProductItemName || "N/A"}</td>
                        <td>{item.IsQuantified ? "Yes" : "No"}</td>
                        <td>{item.Quantity}</td>
                        <td>{item.IsOptional ? "Yes" : "No"}</td>
                        <td>{item.IsExtraCharge ? "Yes" : "No"}</td>
                        <td>{item.DisplayOrder ?? "N/A"}</td>

                        <td className="text-nowrap">
                          {/* Edit */}
                          <button
                            type="button"
                            onClick={() => handleEditProduct(item)}
                            className="btn btn-sm btn-primary me-2"
                            title="Edit"
                          >
                            <i className="feather-edit"></i>
                          </button>

                          {/* Delete */}
                          <button
                            type="button"
                            onClick={() => handleDeleteCombination(item)}
                            className="btn btn-sm btn-light me-2"
                            title="Delete"
                            style={{ border: "1px solid #f1c0c0" }}
                          >
                            <Trash2 size={16} color="red" />
                          </button>

                          {/* Actions */}
                          <select
                            className="form-select form-select-sm d-inline-block"
                            style={{ width: "140px" }}
                            onChange={(e) => handleComboRedirect(e, item)}
                            defaultValue=""
                          >
                            <option value="" disabled>
                              Select Action
                            </option>
                            <option value="product-preparation">Preparation</option>
                            <option value="product-substitution">Substitution</option>
                          </select>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan="7" className="text-center">
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

      {showModel && (
        <CombinationForm
          onSubmit={handleAddProduct}
          showModel={showModel}
          handleClose={handleClose}
          data={selectedData}
          productList={productListData}
          id={id}
          onRegisterReset={(resetFn) => (comboFormResetRef.current = resetFn)}
        />
      )}
    </div>
  );
};

export default CombinationPage;