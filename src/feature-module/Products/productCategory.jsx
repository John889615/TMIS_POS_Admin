import React, { useState, useEffect } from "react";
import Swal from "sweetalert2";
import {
  getAllProductCategory,
  newProductCategory,
  updateProductCategory,
} from "../../services/product/productCategory";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";
import ProductCategoryForm from "../../core/modals/products/productCategoryFormModel";

const getApiErrorMessage = (err) => {
  const data = err?.response?.data;

  // ✅ Your API format: { Success, Messages:[], Errors:[], ErrorCode, ... }
  if (data) {
    if (Array.isArray(data.Messages) && data.Messages.length)
      return data.Messages.join("\n");

    if (Array.isArray(data.Errors) && data.Errors.length)
      return data.Errors.join("\n");

    if (typeof data.ErrorCode === "string" && data.ErrorCode.trim())
      return data.ErrorCode;

    if (typeof data === "string" && data.trim()) return data;
  }

  // Axios common fallbacks
  const axiosMsg =
    err?.response?.data?.message ||
    err?.response?.data?.Message ||
    err?.response?.data?.error ||
    err?.response?.data?.title;

  if (typeof axiosMsg === "string" && axiosMsg.trim()) return axiosMsg;

  return err?.message || err?.toString?.() || "Unknown error occurred.";
};

const ProductCategory = () => {
  const [listData, setListData] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [showModel, setModelShow] = useState(false);
  const [selectedData, setSelectedData] = useState(null);

  // Pagination
  const [currentPage, setCurrentPage] = useState(1);
  const recordsPerPage = 10;
  const maxPageButtons = 5;

  useEffect(() => {
    fetchRecords();
  }, []);

  const fetchRecords = async () => {
    try {
      const data = await getAllProductCategory();
      setListData(data || []);
    } catch (err) {
      console.error("Failed to load categories:", err?.message || err);
      Swal.fire({
        icon: "error",
        title: "Failed to load categories",
        text: getApiErrorMessage(err),
      });
    }
  };

  const filteredData = listData.filter((item) =>
    Object.values(item).some(
      (value) =>
        typeof value === "string" &&
        value.toLowerCase().includes(searchTerm.toLowerCase())
    )
  );

  // Pagination calculations
  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentData = filteredData.slice(indexOfFirstRecord, indexOfLastRecord);
  const totalPages = Math.ceil(filteredData.length / recordsPerPage);

  const goToPage = (page) => {
    if (page >= 1 && page <= totalPages) setCurrentPage(page);
  };

  // 5-button window logic
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

  const handleAddCategory = async (data) => {
    try {
      if (data.POS_ProductCategoryID) {
        await updateProductCategory(data);

        Swal.fire({
          icon: "success",
          title: "Updated",
          text: "Category updated successfully.",
          timer: 1500,
          showConfirmButton: false,
        });
      } else {
        await newProductCategory(data);

        Swal.fire({
          icon: "success",
          title: "Saved",
          text: "Category added successfully.",
          timer: 1500,
          showConfirmButton: false,
        });
      }

      await fetchRecords();
      setModelShow(false);
    } catch (err) {
      const msg = getApiErrorMessage(err);
      console.error("Error saving category:", msg, err);

      Swal.fire({
        icon: "error",
        title: "Could not save category",
        text: msg,
        confirmButtonText: "OK",
      });
    }
  };

  const handleEditCategory = (record) => {
    setSelectedData(record);
    setModelShow(true);
  };

  return (
    <div className="page-wrapper">
      <div className="content">
        <div className="page-header">
          <div className="add-item d-flex">
            <div className="page-title">
              <h4>Categories</h4>
              <h6>Manage Your Category</h6>
            </div>
          </div>

          <div className="page-btn">
            <Button variant="none" className="btn btn-added" onClick={handleShow}>
              <PlusCircle className="me-2" />
              Add New Category
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
                    onChange={(e) => {
                      setSearchTerm(e.target.value);
                      setCurrentPage(1);
                    }}
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
                    <th>Category Master</th>
                    <th>Is Master Category</th>
                    <th>Action</th>
                  </tr>
                </thead>

                <tbody>
                  {currentData.length > 0 ? (
                    currentData.map((item, index) => (
                      <tr key={index}>
                        <td>{item.CategoryName || "N/A"}</td>
                        <td>{item.CategoryMaster || "N/A"}</td>
                        <td>{item.IsMaster ? "Yes" : "No"}</td>
                        <td>
                          <button
                            type="button"
                            onClick={() => handleEditCategory(item)}
                            className="btn btn-sm btn-primary me-2"
                          >
                            <i className="feather-edit"></i>
                          </button>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan="4" className="text-center">
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
                      >
                        {page}
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

      {showModel && (
        <ProductCategoryForm
          onSubmit={handleAddCategory}
          showModel={showModel}
          handleClose={handleClose}
          data={selectedData}
          categoryList={listData}
        />
      )}
    </div>
  );
};

export default ProductCategory;