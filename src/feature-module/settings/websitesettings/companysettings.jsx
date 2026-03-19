import React, { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  ChevronUp,
  RotateCcw,
  Upload,
  X,
  Image as ImageIcon,
  Home,
  MapPin,
} from "feather-icons-react/build/IconComponents";
import { setToogleHeader } from "../../../core/redux/action";

const emptyForm = {
  CompanyName: "",
  CompanyEmailAddress: "",
  PhoneNumber: "",
  Fax: "",
  Website: "",
  Address: "",
  Country: "",
  StateProvince: "",
  City: "",
  PostalCode: "",
  CompanyLogoUrl: "",
  CompanyIconUrl: "",
  FaviconUrl: "",
  CompanyDarkLogoUrl: "",
};

const imageFields = [
  {
    key: "CompanyLogo",
    title: "Company Logo",
    subtitle: "Main logo used across the portal",
    urlField: "CompanyLogoUrl",
  },
  {
    key: "CompanyIcon",
    title: "Company Icon",
    subtitle: "Small icon for interface branding",
    urlField: "CompanyIconUrl",
  },
  {
    key: "Favicon",
    title: "Favicon",
    subtitle: "Browser tab icon",
    urlField: "FaviconUrl",
  },
  {
    key: "CompanyDarkLogo",
    title: "Dark Logo",
    subtitle: "Logo for dark backgrounds",
    urlField: "CompanyDarkLogoUrl",
  },
];

const CompanySettings = () => {
  const dispatch = useDispatch();
  const isHeaderCollapsed = useSelector((state) => state.toggle_header);

  const [form, setForm] = useState(emptyForm);
  const [initialForm, setInitialForm] = useState(emptyForm);

  const [imageFiles, setImageFiles] = useState({
    CompanyLogo: null,
    CompanyIcon: null,
    Favicon: null,
    CompanyDarkLogo: null,
  });

  const [imagePreviews, setImagePreviews] = useState({
    CompanyLogo: "",
    CompanyIcon: "",
    Favicon: "",
    CompanyDarkLogo: "",
  });

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [loadError, setLoadError] = useState("");
  const [saveError, setSaveError] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const fileRefs = {
    CompanyLogo: useRef(null),
    CompanyIcon: useRef(null),
    Favicon: useRef(null),
    CompanyDarkLogo: useRef(null),
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

  const resetFormState = useCallback((payload) => {
    const next = {
      ...emptyForm,
      ...payload,
    };

    setForm(next);
    setInitialForm(next);
    setImageFiles({
      CompanyLogo: null,
      CompanyIcon: null,
      Favicon: null,
      CompanyDarkLogo: null,
    });
    setImagePreviews({
      CompanyLogo: next.CompanyLogoUrl || "",
      CompanyIcon: next.CompanyIconUrl || "",
      Favicon: next.FaviconUrl || "",
      CompanyDarkLogo: next.CompanyDarkLogoUrl || "",
    });
    setLoadError("");
    setSaveError("");
    setSuccessMessage("");

    Object.values(fileRefs).forEach((ref) => {
      if (ref.current) ref.current.value = "";
    });
  }, []);

  const loadCompanySettings = useCallback(async () => {
    setLoading(true);
    setLoadError("");
    setSaveError("");
    setSuccessMessage("");

    try {
      // TODO: replace with API call later
      const data = {
        CompanyName: "",
        CompanyEmailAddress: "",
        PhoneNumber: "",
        Fax: "",
        Website: "",
        Address: "",
        Country: "",
        StateProvince: "",
        City: "",
        PostalCode: "",
        CompanyLogoUrl: "",
        CompanyIconUrl: "",
        FaviconUrl: "",
        CompanyDarkLogoUrl: "",
      };

      resetFormState(data);
    } catch (err) {
      console.error("loadCompanySettings failed", err);
      setLoadError("Failed to load company settings.");
    } finally {
      setLoading(false);
    }
  }, [resetFormState]);

  useEffect(() => {
    loadCompanySettings();
  }, [loadCompanySettings]);

  useEffect(() => {
    if (!successMessage) return;
    const t = setTimeout(() => setSuccessMessage(""), 2500);
    return () => clearTimeout(t);
  }, [successMessage]);

  const validateForm = () => {
    if (!form.CompanyName.trim()) return "Company Name is required.";
    if (
      form.CompanyEmailAddress &&
      !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.CompanyEmailAddress.trim())
    ) {
      return "Company Email Address is invalid.";
    }
    return "";
  };

  const handleSave = async (e) => {
    e.preventDefault();

    const err = validateForm();
    if (err) {
      setSaveError(err);
      setSuccessMessage("");
      return;
    }

    setSaving(true);
    setSaveError("");
    setSuccessMessage("");

    try {
      const payload = { ...form };

      // TODO: wire API here later
      console.log("SAVE PAYLOAD", payload);
      console.log("SAVE FILES", imageFiles);

      setInitialForm(form);
      setSuccessMessage("Company settings saved.");
    } catch (err) {
      console.error("saveCompanySettings failed", err);
      setSaveError("Failed to save company settings.");
    } finally {
      setSaving(false);
    }
  };

  const handleCancel = () => {
    resetFormState(initialForm);
  };

  const handleImageChange = (key, urlField, file) => {
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

    setForm((prev) => ({
      ...prev,
      [urlField]: objectUrl,
    }));
  };

  const removeImage = (key, urlField) => {
    setImageFiles((prev) => ({
      ...prev,
      [key]: null,
    }));

    setImagePreviews((prev) => ({
      ...prev,
      [key]: "",
    }));

    setForm((prev) => ({
      ...prev,
      [urlField]: "",
    }));

    if (fileRefs[key]?.current) {
      fileRefs[key].current.value = "";
    }
  };

  return (
    <div className="page-wrapper">
      <div className="content settings-content">
        <div className="d-flex align-items-center justify-content-between flex-wrap gap-3 mb-4">
          <div>
            <h4 className="mb-1">Company Settings</h4>
            <div className="text-muted small">
              Manage your company details, branding, and address information
            </div>
          </div>

          <div className="d-flex align-items-center gap-2">
            <OverlayTrigger placement="top" overlay={renderRefreshTooltip}>
              <button
                type="button"
                className="btn btn-light border"
                onClick={loadCompanySettings}
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
                <div className="col-xl-4 col-md-6">
                  <label className="form-label">Company Name</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.CompanyName}
                    onChange={(e) => setField("CompanyName", e.target.value)}
                  />
                </div>

                <div className="col-xl-4 col-md-6">
                  <label className="form-label">Email Address</label>
                  <input
                    type="email"
                    className="form-control"
                    value={form.CompanyEmailAddress}
                    onChange={(e) => setField("CompanyEmailAddress", e.target.value)}
                  />
                </div>

                <div className="col-xl-4 col-md-6">
                  <label className="form-label">Phone Number</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.PhoneNumber}
                    onChange={(e) => setField("PhoneNumber", e.target.value)}
                  />
                </div>

                <div className="col-xl-4 col-md-6">
                  <label className="form-label">Fax</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.Fax}
                    onChange={(e) => setField("Fax", e.target.value)}
                  />
                </div>

                <div className="col-xl-4 col-md-6">
                  <label className="form-label">Website</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.Website}
                    onChange={(e) => setField("Website", e.target.value)}
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
                          onChange={(e) =>
                            handleImageChange(
                              item.key,
                              item.urlField,
                              e.target.files?.[0] || null
                            )
                          }
                        />

                        <div className="d-flex gap-2">
                          <button
                            type="button"
                            className="btn btn-light border w-100"
                            onClick={() => fileRefs[item.key]?.current?.click()}
                          >
                            Upload
                          </button>

                          {hasImage ? (
                            <button
                              type="button"
                              className="btn btn-light border"
                              onClick={() => removeImage(item.key, item.urlField)}
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
            </div>
          </div>

          <div className="card border-0 shadow-sm mb-4">
            <div className="card-body">
              <div className="d-flex align-items-center gap-2 mb-4">
                <MapPin size={18} />
                <h5 className="mb-0">Address</h5>
              </div>

              <div className="row g-3">
                <div className="col-12">
                  <label className="form-label">Address</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.Address}
                    onChange={(e) => setField("Address", e.target.value)}
                  />
                </div>

                <div className="col-xl-3 col-md-6">
                  <label className="form-label">Country</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.Country}
                    onChange={(e) => setField("Country", e.target.value)}
                  />
                </div>

                <div className="col-xl-3 col-md-6">
                  <label className="form-label">State / Province</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.StateProvince}
                    onChange={(e) => setField("StateProvince", e.target.value)}
                  />
                </div>

                <div className="col-xl-3 col-md-6">
                  <label className="form-label">City</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.City}
                    onChange={(e) => setField("City", e.target.value)}
                  />
                </div>

                <div className="col-xl-3 col-md-6">
                  <label className="form-label">Postal Code</label>
                  <input
                    type="text"
                    className="form-control"
                    value={form.PostalCode}
                    onChange={(e) => setField("PostalCode", e.target.value)}
                  />
                </div>
              </div>
            </div>
          </div>

          <div className="d-flex justify-content-end gap-2">
            <button
              type="button"
              className="btn btn-light border"
              onClick={handleCancel}
              disabled={saving}
            >
              Cancel
            </button>

            <button
              type="submit"
              className="btn btn-primary"
              disabled={saving || loading}
            >
              {saving ? "Saving..." : "Save Changes"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default CompanySettings;