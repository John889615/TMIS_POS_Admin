import React, { useEffect, useMemo, useState } from "react";
import { Button } from "react-bootstrap";
import { PlusCircle } from "react-feather";
import Swal from "sweetalert2";
import TenantSettingsForm from "../../../core/modals/entityData/tenantSettings/tenantSettingsModal";

import {
  getSettings,
  newSetting,
  updateSetting,
} from "../../../services/entityData/tenantSettings";

const TenantSettings = () => {
  const [showModal, setShowModal] = useState(false);
  const [selectedSetting, setSelectedSetting] = useState(null);
  const [listData, setListData] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetchRecord();
  }, []);

  const fetchRecord = async () => {
    try {
      setLoading(true);

      const response = await getSettings();

      // Case 1: API returns wrapped response
      if (response && typeof response === "object" && "Success" in response) {
        if (
          response.Success === false &&
          (response.ErrorCode === "SettingsNotFound" ||
            response.StatusCode === 404 ||
            response.Data == null)
        ) {
          setListData([]);
          setSelectedSetting(null);
          setShowModal(true);
          return;
        }

        if (response.Success && Array.isArray(response.Data)) {
          setListData(response.Data);
          return;
        }

        if (response.Success && response.Data && !Array.isArray(response.Data)) {
          setListData([response.Data]);
          return;
        }

        setListData([]);
        return;
      }

      // Case 2: service already returns raw array
      if (Array.isArray(response)) {
        setListData(response);
        if (response.length === 0) {
          setSelectedSetting(null);
          setShowModal(true);
        }
        return;
      }

      // Case 3: null/empty fallback
      setListData([]);
      setSelectedSetting(null);
      setShowModal(true);
    } catch (err) {
      console.error("Failed to load settings:", err);

      const apiResponse = err?.response?.data;

      if (
        apiResponse?.ErrorCode === "SettingsNotFound" ||
        apiResponse?.StatusCode === 404
      ) {
        setListData([]);
        setSelectedSetting(null);
        setShowModal(true);
        return;
      }

      Swal.fire({
        icon: "error",
        title: "Error",
        text:
          apiResponse?.Messages?.[0] ||
          err?.message ||
          "Failed to load settings",
      });
    } finally {
      setLoading(false);
    }
  };

  const handleShow = () => {
    setSelectedSetting(null);
    setShowModal(true);
  };

  const handleClose = () => {
    // If there is no setting yet, keep modal open unless user explicitly wants to leave
    if (listData.length === 0) {
      return;
    }
    setShowModal(false);
  };

  const handleSubmit = async (data) => {
    try {
      if (data.SettingID) {
        await updateSetting(data);

        Swal.fire({
          icon: "success",
          title: "Success",
          text: "Settings updated successfully.",
        });
      } else {
        await newSetting(data);

        Swal.fire({
          icon: "success",
          title: "Success",
          text: "Settings added successfully.",
        });
      }

      await fetchRecord();
      setShowModal(false);
    } catch (err) {
      console.error("Error saving settings:", err);

      const apiResponse = err?.response?.data;

      Swal.fire({
        icon: "error",
        title: "Error",
        text:
          apiResponse?.Messages?.[0] ||
          err?.message ||
          "Failed to save settings.",
      });
    }
  };

  const hasSetting = useMemo(() => listData.length > 0, [listData]);

  return (
    <div className="page-wrapper">
      <div className="content">
        <div className="page-header">
          <div className="add-item d-flex">
            <div className="page-title">
              <h4>Tenant Settings</h4>
              <h6>Manage your company settings</h6>
            </div>
          </div>

          {!hasSetting && (
            <div className="page-btn">
              <Button
                variant="none"
                className="btn btn-added"
                onClick={handleShow}
              >
                <PlusCircle className="me-2" />
                Add Settings
              </Button>
            </div>
          )}
        </div>

        <div className="card table-list-card">
          <div className="card-body">
            <div className="table-responsive">
              <table className="table table-bordered table-striped">
                <thead>
                  <tr>
                    <th>Company</th>
                    <th>Currency</th>
                    <th style={{ width: "120px" }}>Action</th>
                  </tr>
                </thead>
                <tbody>
                  {loading ? (
                    <tr>
                      <td colSpan="3" className="text-center">
                        Loading...
                      </td>
                    </tr>
                  ) : listData.length > 0 ? (
                    listData.map((item) => (
                      <tr key={item.SettingID}>
                        <td>{item.Company || "N/A"}</td>
                        <td>{item.Currency || "N/A"}</td>
                        <td>
                          <button
                            onClick={() => {
                              setSelectedSetting(item);
                              setShowModal(true);
                            }}
                            className="btn btn-sm btn-primary"
                          >
                            <i className="feather-edit" /> Edit
                          </button>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan="3" className="text-center">
                        No settings found. Please add settings.
                      </td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          </div>
        </div>

        {showModal && (
          <TenantSettingsForm
            onSubmit={handleSubmit}
            showModel={showModal}
            handleClose={handleClose}
            data={selectedSetting}
            isFirstSetup={!hasSetting}
          />
        )}
      </div>
    </div>
  );
};

export default TenantSettings;