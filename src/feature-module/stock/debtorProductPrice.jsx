import React, { useState, useEffect } from "react";
import {
  getAllProductPriceById,
  newProductPrice,
  updateProductPrice,
} from "../../services/stock/debtorProductPrice";
import { getAllPriceCode } from "../../services/stock/priceCode";
import { getAllTaxType } from "../../services/syncList/syncService";
import { Button } from "react-bootstrap";
import { PlusCircle } from "react-feather";
import DebtorProductPriceForm from "../../core/modals/stocks/debtorProductPriceModel";
import { useParams } from "react-router-dom";

const DebtorProductPagePage = () => {
  const { id } = useParams();

  const [listData, setListData] = useState([]);
  const [priceCodeListData, setPriceCodeListData] = useState([]);
  const [taxList, setTaxList] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [showModel, setModelShow] = useState(false);
  const [selectedData, setSelectedData] = useState(null);

  useEffect(() => {
    fetchRecords();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  const fetchRecords = async () => {
    try {
      if (id) {
        const res = await getAllProductPriceById(id);
        const rows = Array.isArray(res) ? res : res?.data ?? [];
        setListData(rows);
      } else {
        setListData([]);
      }

      const pc = await getAllPriceCode();
      setPriceCodeListData(Array.isArray(pc) ? pc : pc?.data ?? []);

      const tax = await getAllTaxType();
      setTaxList(Array.isArray(tax) ? tax : tax?.data ?? []);
    } catch (err) {
      console.error("Failed to load:", err);
      setListData([]);
    }
  };

  const filteredData = (Array.isArray(listData) ? listData : []).filter((item) =>
    Object.values(item || {}).some((v) =>
      String(v ?? "").toLowerCase().includes(searchTerm.toLowerCase())
    )
  );

  const handleShow = () => {
    setSelectedData(null);
    setModelShow(true);
  };

  const handleClose = () => setModelShow(false);

  const handleAddOrUpdate = async (payload) => {
    try {
      // ✅ Update if ID exists, else insert
      if (payload?.POS_DebtorProductPriceID) {
        await updateProductPrice(payload);
      } else {
        await newProductPrice(payload);
      }

      await fetchRecords();
      setModelShow(false);
    } catch (err) {
      console.error("Error saving debtor product price:", err);
    }
  };

  const handleEdit = (record) => {
    setSelectedData(record);
    setModelShow(true);
  };

  const formatMoney = (v) =>
    v == null ? "0.00" : Number(v).toFixed(2);

  return (
    <div className="page-wrapper">
      <div className="content">
        <div className="page-header">
          <div className="add-item d-flex">
            <div className="page-title">
              <h4>Product Prices</h4>
              <h6>Manage Your Price</h6>
            </div>
          </div>
          <div className="page-btn">
            <Button variant="none" className="btn btn-added" onClick={handleShow}>
              <PlusCircle className="me-2" />
              Add New Price
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
                  {/* ✅ Don't use broken Link */}
                  <button type="button" className="btn btn-searchset">
                    <i data-feather="search" className="feather-search" />
                  </button>
                </div>
              </div>
            </div>

            <div className="table-responsive">
              <table className="table table-bordered table-striped">
                <thead>
                  <tr>
                    <th>Price Code</th>
                    <th>Item Price</th>
                    <th>Inclusive</th>
                    <th>VAT</th>
                    <th>Start</th>
                    <th>End</th>
                    <th>Active</th>
                    <th>Action</th>
                  </tr>
                </thead>

                <tbody>
                  {filteredData.length > 0 ? (
                    filteredData.map((item, index) => (
                      <tr key={item?.POS_DebtorProductPriceID ?? index}>
                        <td>{item?.PriceCode ?? "N/A"}</td>
                        <td>{formatMoney(item?.ItemPrice)}</td>
                        <td>{item?.Inclusive ? "Yes" : "No"}</td>
                        <td>{formatMoney(item?.Vat)}</td>
                        <td>{item?.StartDate ? new Date(item.StartDate).toLocaleString() : "N/A"}</td>
                        <td>{item?.EndDate ? new Date(item.EndDate).toLocaleString() : "N/A"}</td>
                        <td>{item?.IsActive ? "Yes" : "No"}</td>
                        <td>
                          <button
                            type="button"
                            onClick={() => handleEdit(item)}
                            className="btn btn-sm btn-primary me-2"
                          >
                            <i className="feather-edit"></i>
                          </button>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan="8" className="text-center">
                        No records found
                      </td>
                    </tr>
                  )}
                </tbody>
              </table>

              <div className="mt-2 text-muted" style={{ fontSize: 12 }}>
                Debug: DebtorProductID = {String(id ?? "")} | Rows = {filteredData.length}
              </div>
            </div>
          </div>
        </div>
      </div>

      {showModel && (
        <DebtorProductPriceForm
          onSubmit={handleAddOrUpdate}
          showModel={showModel}
          handleClose={handleClose}
          data={selectedData}
          priceCodeList={priceCodeListData}
          taxList={taxList}
          id={id}
        />
      )}
    </div>
  );
};

export default DebtorProductPagePage;
