import React, { useEffect, useMemo, useRef, useState } from "react";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import Select from "react-select";
import { useDispatch, useSelector } from "react-redux";
import {
  ChevronUp,
  RotateCcw,
  Upload,
  X,
  Image as ImageIcon,
  Home,
} from "feather-icons-react/build/IconComponents";
import Swal from "sweetalert2";
import { setToogleHeader } from "../../../core/redux/action";
import {
  getSettings,
  newSetting,
  updateSetting,
} from "../../../services/entityData/tenantSettings";
import { getAllCurrency } from "../../../services/entityData/currency";

const emptyForm = {
  Company: "",
  Email: "",
  HeadOfficeNo: "",
  FK_CurrencyID: 0,
};

const imageFields = [
  {
    key: "CompanyLogo",
    title: "Company Logo",
    subtitle: "Main logo used across the portal",
  },
  {
    key: "CompanyIcon",
    title: "Company Icon",
    subtitle: "Small icon for interface branding",
  },
];

const getApiMessage = (result, fallback) => {
  if (Array.isArray(result?.Messages) && result.Messages.length) {
    return result.Messages.join("\n");
  }

  if (Array.isArray(result?.Errors) && result.Errors.length) {
    return result.Errors.join("\n");
  }

  if (typeof result?.ErrorCode === "string" && result.ErrorCode.trim()) {
    return result.ErrorCode;
  }

  return fallback;
};

const CompanySettings = () => {
  const dispatch = useDispatch();
  const isHeaderCollapsed = useSelector((state) => state.toggle_header);

  const [form, setForm] = useState(emptyForm);
  const [initialForm, setInitialForm] = useState(emptyForm);
  const [currentRecord, setCurrentRecord] = useState(null);

  const [currencyList, setCurrencyList] = useState([]);
  const [loadingCurrencies, setLoadingCurrencies] = useState(false);

  const [imageFiles, setImageFiles] = useState({
    CompanyLogo: null,
    CompanyIcon: null,
  });

  const [imagePreviews, setImagePreviews] = useState({
    CompanyLogo: "",
    CompanyIcon: "",
  });

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [loadError, setLoadError] = useState("");
  const [saveError, setSaveError] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const fileRefs = {
    CompanyLogo: useRef(null),
    CompanyIcon: useRef(null),
  };

  const renderRefreshTooltip = (props) => (
    <Tooltip id="refresh-tooltip" {...props}>
      Refresh
    </Tooltip>
  );

  const renderCollapseTooltip = (props) => (
    <Tooltip id="collapse-tooltip" {...props}>
      Collapse
    </Tooltip>
  );

  const currencyOptions = useMemo(() => {
    return (currencyList || []).map((item) => ({
      value: item.CurrencyID,
      label: `${item.Currency} - ${item.Name}`,
      currency: item.Currency,
      name: item.Name,
    }));
  }, [currencyList]);

  const selectedCurrency = useMemo(() => {
    if (!form.FK_CurrencyID) return null;

    return (
      currencyOptions.find(
        (item) => Number(item.value) === Number(form.FK_CurrencyID)
      ) || null
    );
  }, [currencyOptions, form.FK_CurrencyID]);

  const hasChanges = useMemo(() => {
    const formChanged = JSON.stringify(form) !== JSON.stringify(initialForm);
    const filesChanged = Object.values(imageFiles).some((x) => !!x);
    return formChanged || filesChanged;
  }, [form, initialForm, imageFiles]);

  const setField = (field, value) => {
    setForm((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const resetFileInputs = () => {
    Object.values(fileRefs).forEach((ref) => {
      if (ref.current) {
        ref.current.value = "";
      }
    });
  };

  const normaliseSettingsRecord = (record) => {
    const safeRecord = record && typeof record === "object" ? record : {};

    return {
      Company: safeRecord.Company ?? "",
      Email: safeRecord.Email ?? "",
      HeadOfficeNo: safeRecord.HeadOfficeNo ?? "",
      FK_CurrencyID: safeRecord.FK_CurrencyID ?? 0,
    };
  };

  const resetFormState = (record) => {
    const nextForm = normaliseSettingsRecord(record);

    setForm(nextForm);
    setInitialForm(nextForm);
    setCurrentRecord(record && typeof record === "object" ? record : null);

    setImageFiles({
      CompanyLogo: null,
      CompanyIcon: null,
    });

    setImagePreviews({
      CompanyLogo: "",
      CompanyIcon: "",
    });

    setLoadError("");
    setSaveError("");
    setSuccessMessage("");
    resetFileInputs();
  };

  const fetchCurrencies = async () => {
    try {
      setLoadingCurrencies(true);

      const response = await getAllCurrency();

      if (response?.Success && Array.isArray(response.Data)) {
        setCurrencyList(response.Data);
        return;
      }

      if (Array.isArray(response)) {
        setCurrencyList(response);
        return;
      }

      setCurrencyList([]);
    } catch (err) {
      console.error("Failed to load currencies:", err);
      setCurrencyList([]);
    } finally {
      setLoadingCurrencies(false);
    }
  };

  const loadCompanySettings = async () => {
    setLoading(true);
    setLoadError("");
    setSaveError("");
    setSuccessMessage("");

    try {
      const result = await getSettings();

      if (!result?.Success) {
        throw new Error(getApiMessage(result, "Failed to load company settings."));
      }

      const data = result?.Data;
      const record = Array.isArray(data) ? data[0] || null : data || null;

      resetFormState(record);
    } catch (err) {
      console.error("loadCompanySettings failed", err);
      setLoadError(err?.message || "Failed to load company settings.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCurrencies();
    loadCompanySettings();
  }, []);

  useEffect(() => {
    if (!successMessage) return;
    const timeout = setTimeout(() => setSuccessMessage(""), 2500);
    return () => clearTimeout(timeout);
  }, [successMessage]);

  const validateForm = () => {
    if (!form.Company.trim()) {
      return "Company is required.";
    }

    if (form.Email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.Email.trim())) {
      return "Email is invalid.";
    }

    if (!Number(form.FK_CurrencyID)) {
      return "Please select a currency.";
    }

    return "";
  };

  const handleImageChange = (key, file) => {
    if (!file) return;

    const objectUrl = URL.createObjectURL(file);

    setImageFiles((prev) => ({
      ...prev,
      [key]: file,
    }));

    setImagePreviews((prev) => ({
      ...prev,
      [key]: objectUrl,
    }));
  };

  const removeImage = (key) => {
    setImageFiles((prev) => ({
      ...prev,
      [key]: null,
    }));

    setImagePreviews((prev) => ({
      ...prev,
      [key]: "",
    }));

    if (fileRefs[key]?.current) {
      fileRefs[key].current.value = "";
    }
  };

  const buildPayload = () => {
    return {
      Company: form.Company.trim(),
      Email: form.Email.trim(),
      HeadOfficeNo: form.HeadOfficeNo.trim(),
      FK_CurrencyID: Number(form.FK_CurrencyID) || 0,
      ...(currentRecord?.SettingID ? { SettingID: currentRecord.SettingID } : {}),
    };
  };

  const handleSave = async (e) => {
    e.preventDefault();

    const validationError = validateForm();
    if (validationError) {
      setSaveError(validationError);
      setSuccessMessage("");
      return;
    }

    setSaving(true);
    setSaveError("");
    setSuccessMessage("");

    try {
      const payload = buildPayload();

      const isUpdate = !!currentRecord?.SettingID;
      const result = isUpdate ? await updateSetting(payload) : await newSetting(payload);

      if (!result?.Success) {
        throw new Error(getApiMessage(result, "Failed to save company settings."));
      }

      const returnedData = result?.Data;
      const updatedRecord = Array.isArray(returnedData)
        ? returnedData[0] || payload
        : returnedData || payload;

      resetFormState(updatedRecord);
      setSuccessMessage("Company settings saved.");

      Swal.fire({
        icon: "success",
        title: "Saved",
        text: "Company settings saved successfully.",
        confirmButtonText: "OK",
      });
    } catch (err) {
      console.error("saveCompanySettings failed", err);

      const message = err?.message || "Failed to save company settings.";
      setSaveError(message);

      Swal.fire({
        icon: "error",
        title: "Could not save company settings",
        text: message,
        confirmButtonText: "OK",
      });
    } finally {
      setSaving(false);
    }
  };

  const handleCancel = () => {
    setForm(initialForm);
    setImageFiles({
      CompanyLogo: null,
      CompanyIcon: null,
    });
    setImagePreviews({
      CompanyLogo: "",
      CompanyIcon: "",
    });
    resetFileInputs();
    setSaveError("");
    setSuccessMessage("");
  };

  return (
    <div className="page-wrapper">
      <div className="content settings-content">
        <div className="d-flex align-items-center justify-content-between flex-wrap gap-3 mb-4">
          <div>
            <h4 className="mb-1">Company Settings</h4>
            <div className="text-muted small">
              Manage your company details and branding
            </div>
          </div>

          <div className="d-flex align-items-center gap-2">
            <OverlayTrigger placement="top" overlay={renderRefreshTooltip}>
              <button
                type="button"
                className="btn btn-light border"
                onClick={loadCompanySettings}
                disabled={loading || saving}
              >
                <RotateCcw size={16} />
              </button>
            </OverlayTrigger>

            <OverlayTrigger placement="top" overlay={renderCollapseTooltip}>
              <button
                type="button"
                className={`btn btn-light border ${isHeaderCollapsed ? "active" : ""}`}
                onClick={() => dispatch(setToogleHeader(!isHeaderCollapsed))}
              >
                <ChevronUp size={16} />
              </button>
            </OverlayTrigger>
          </div>
        </div>

        <div className="d-flex justify-content-end gap-2 mb-4">
          <button
            type="button"
            className="btn btn-light border"
            onClick={handleCancel}
            disabled={saving || loading || !hasChanges}
          >
            Cancel
          </button>

          <button
            type="button"
            className="btn btn-primary"
            onClick={handleSave}
            disabled={saving || loading || !hasChanges}
          >
            {saving ? "Saving..." : currentRecord?.SettingID ? "Update" : "Create"}
          </button>
        </div>

        {loadError ? <div className="alert alert-danger py-2">{loadError}</div> : null}
        {saveError ? <div className="alert alert-danger py-2">{saveError}</div> : null}
        {successMessage ? <div className="alert alert-success py-2">{successMessage}</div> : null}

        <form onSubmit={handleSave}>
          <div className="card border-0 shadow-sm mb-4">
            <div className="card-body">
              <div className="d-flex align-items-center gap-2 mb-4">
                <Home size={18} />
                <h5 className="mb-0">Company Information</h5>
              </div>

              <div className="row g-3">
                <div className="col-xl-6 col-md-6">
                  <label className="form-label">Company</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.Company}
                    onChange={(e) => setField("Company", e.target.value)}
                    disabled={loading || saving}
                  />
                </div>

                <div className="col-xl-6 col-md-6">
                  <label className="form-label">Email</label>
                  <input
                    type="email"
                    className="form-control"
                    value={form.Email}
                    onChange={(e) => setField("Email", e.target.value)}
                    disabled={loading || saving}
                  />
                </div>

                <div className="col-xl-6 col-md-6">
                  <label className="form-label">Head Office No</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.HeadOfficeNo}
                    onChange={(e) => setField("HeadOfficeNo", e.target.value)}
                    disabled={loading || saving}
                  />
                </div>

                <div className="col-xl-6 col-md-6">
                  <label className="form-label">Currency</label>
                  <Select
                    options={currencyOptions}
                    value={selectedCurrency}
                    onChange={(selected) =>
                      setField("FK_CurrencyID", selected?.value || 0)
                    }
                    isSearchable
                    isDisabled={loading || saving || loadingCurrencies}
                    placeholder={
                      loadingCurrencies ? "Loading currencies..." : "Select currency"
                    }
                    noOptionsMessage={() => "No currencies found"}
                    classNamePrefix="react-select"
                  />
                </div>
              </div>
            </div>
          </div>

          <div className="card border-0 shadow-sm mb-4">
            <div className="card-body">
              <div className="d-flex align-items-center gap-2 mb-4">
                <ImageIcon size={18} />
                <h5 className="mb-0">Brand Assets</h5>
              </div>

              <div className="row g-3">
                {imageFields.map((item) => {
                  const preview = imagePreviews[item.key];
                  const hasImage = !!preview;

                  return (
                    <div className="col-xl-3 col-md-6" key={item.key}>
                      <div
                        className="border rounded-3 p-3 h-100"
                        style={{ background: "#fff" }}
                      >
                        <div className="mb-3">
                          <div className="fw-semibold">{item.title}</div>
                          <div className="text-muted small">{item.subtitle}</div>
                        </div>

                        <div
                          className="border rounded-3 d-flex align-items-center justify-content-center mb-3"
                          style={{
                            height: 120,
                            background: "#f8f9fa",
                            overflow: "hidden",
                          }}
                        >
                          {hasImage ? (
                            <img
                              src={preview}
                              alt={item.title}
                              style={{
                                maxWidth: "100%",
                                maxHeight: "100%",
                                objectFit: "contain",
                              }}
                            />
                          ) : (
                            <div className="text-center text-muted">
                              <Upload size={20} />
                              <div className="small mt-2">No image</div>
                            </div>
                          )}
                        </div>

                        <input
                          ref={fileRefs[item.key]}
                          type="file"
                          accept="image/*"
                          className="d-none"
                          onChange={(e) => handleImageChange(item.key, e.target.files?.[0] || null)}
                        />

                        <div className="d-flex gap-2">
                          <button
                            type="button"
                            className="btn btn-light border w-100"
                            onClick={() => fileRefs[item.key]?.current?.click()}
                            disabled={loading || saving}
                          >
                            Upload
                          </button>

                          {hasImage ? (
                            <button
                              type="button"
                              className="btn btn-light border"
                              onClick={() => removeImage(item.key)}
                              disabled={loading || saving}
                            >
                              <X size={16} />
                            </button>
                          ) : null}
                        </div>
                      </div>
                    </div>
                  );
                })}
              </div>

              <div className="text-muted small mt-3">
                Images are kept on the page for now and will be wired into upload calls next.
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
};

export default CompanySettings;