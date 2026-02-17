import React, { useState, useEffect } from "react";
import { getAllProducts } from "../../services/product/product";
import {
  getAllCombinationById,
  newCombination,
  updateCombination,
} from "../../services/product/combination";
import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { PlusCircle } from "react-feather";
import CombinationForm from "../../core/modals/products/combinationFormModel";
import { useParams } from "react-router-dom";

const CombinationPage = () => {
  const { id } = useParams(); // ✅ parent PRODUCT id
  const [listData, setListData] = useState([]);
  const [productListData, setProductListData] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [showModel, setModelShow] = useState(false);
  const [selectedData, setSelectedData] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    fetchRecords();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const fetchRecords = async () => {
    try {
      if (id) {
        const data = await getAllCombinationById(id);
        setListData(Array.isArray(data) ? data : []);
      }
      const data = await getAllProducts();
      setProductListData(Array.isArray(data) ? data : []);
    } catch (err) {
      console.error("Failed to load combinations:", err?.message || err);
    }
  };

  const filteredData = listData.filter((item) =>
    Object.values(item).some(
      (value) =>
        typeof value === "string" &&
        value.toLowerCase().includes(searchTerm.toLowerCase())
    )
  );

  const handleShow = () => {
    setSelectedData(null);
    setModelShow(true);
  };

  const handleClose = () => setModelShow(false);

  const handleAddProduct = async (data) => {
    try {
      if (data.ProductCombinationID) await updateCombination(data);
      else await newCombination(data);

      await fetchRecords();
      setModelShow(false);
    } catch (err) {
      console.error("Error saving combination:", err?.message || err);
    }
  };

  const handleEditProduct = (record) => {
    setSelectedData(record);
    setModelShow(true);
  };

  // ✅ IMPORTANT: Preparation/Substitution must use the COMBINATION PRODUCT ID (child)
  const handleComboRedirect = (e, comboRow) => {
    const value = e.target.value;
    if (!value) return;

    // This MUST be the child product (the one selected in CombinationForm as FK_ProductItemID)
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
                        <td>{item.DisplayOrder || "N/A"}</td>
                        <td>
                          <button
                            type="button"
                            onClick={() => handleEditProduct(item)}
                            className="btn btn-sm btn-primary me-2"
                          >
                            <i className="feather-edit"></i>
                          </button>

                          {/* ✅ Combo-level actions use FK_ProductItemID (child product) */}
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
          id={id} // parent product id
        />
      )}
    </div>
  );
};

export default CombinationPage;
