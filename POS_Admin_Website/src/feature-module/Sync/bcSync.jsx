// src/feature-module/pages/Admin/Sync/BusinessCentralSyncPage.jsx
import React, { useMemo, useEffect, useState } from "react";
import FeatherIcon from "feather-icons-react";

import {
  bcSyncAll,
  bcSyncUnits,
  bcSyncLocations,
  bcSyncCategories,
  bcSyncPriceCodes,
  bcSyncProducts,
  bcSyncProductlocations,
  bcSyncProductLocationPrices,
} from "../../services/sync/syncService";

const STATUS = {
  idle: "idle",
  running: "running",
  success: "success",
  error: "error",
};

const statusPill = (s) => {
  const map = {
    idle: { label: "Idle", cls: "bg-light text-dark border" },
    running: { label: "Running", cls: "bg-warning text-dark" },
    success: { label: "Success", cls: "bg-success" },
    error: { label: "Error", cls: "bg-danger" },
  };
  const x = map[s] || map.idle;
  return <span className={`badge ${x.cls}`}>{x.label}</span>;
};

// Service may return boolean OR raw object (fallback).
const isOkResult = (result) => {
  if (typeof result === "boolean") return result;
  if (typeof result?.Success === "boolean") return result.Success;
  if (typeof result?.Data === "boolean") return result.Data;
  return true; // if it didn't throw and shape is unknown, assume ok
};

const extractFailureMessage = (result) =>
  result?.Message ||
  result?.message ||
  result?.Messages?.[0] ||
  result?.Errors?.[0] ||
  (typeof result === "string" ? result : null) ||
  "Unknown error";

const extractErrorMessage = (err) => err?.message || "Unexpected error";

export default function BusinessCentralSyncPage() {
  // -------------------------------------------------------
  // State
  // -------------------------------------------------------
  const [activeJobKey, setActiveJobKey] = useState(null);
  const [progress, setProgress] = useState(0);
  const [status, setStatus] = useState({});
  const [lastRun, setLastRun] = useState({});
  const [logLines, setLogLines] = useState([
    "Ready. Select a sync action to begin.",
  ]);

  // -------------------------------------------------------
  // ✅ ONLY your 8 jobs
  // -------------------------------------------------------
  const syncJobs = useMemo(
    () => [
      { key: "All", title: "Sync All", description: "Run full Business Central sync.", icon: "refresh-cw", group: "Business Central" },

      { key: "Units", title: "Units", description: "Sync units (terminals/units).", icon: "monitor", group: "Business Central" },
      { key: "Locations", title: "Locations", description: "Sync locations.", icon: "map-pin", group: "Business Central" },
      { key: "Categories", title: "Categories", description: "Sync product categories.", icon: "grid", group: "Business Central" },
      { key: "PriceCodes", title: "Price Codes", description: "Sync price codes.", icon: "hash", group: "Business Central" },

      { key: "Products", title: "Products", description: "Sync products/items.", icon: "package", group: "Business Central" },
      { key: "ProductLocations", title: "Product Locations", description: "Sync product availability by location.", icon: "layers", group: "Business Central" },
      { key: "ProductLocationPrices", title: "Product Location Prices", description: "Sync product/location prices.", icon: "tag", group: "Business Central" },
    ],
    []
  );

  // -------------------------------------------------------
  // ✅ Map UI keys to YOUR functions
  // -------------------------------------------------------
  const jobCallMap = useMemo(
    () => ({
      All: bcSyncAll,
      Units: bcSyncUnits,
      Locations: bcSyncLocations,
      Categories: bcSyncCategories,
      PriceCodes: bcSyncPriceCodes,
      Products: bcSyncProducts,
      ProductLocations: bcSyncProductlocations,
      ProductLocationPrices: bcSyncProductLocationPrices,
    }),
    []
  );

  // -------------------------------------------------------
  // Init status + lastRun defaults
  // -------------------------------------------------------
  useEffect(() => {
    setStatus((prev) => {
      const next = { ...prev };
      syncJobs.forEach((j) => {
        if (!next[j.key]) next[j.key] = STATUS.idle;
      });
      return next;
    });

    setLastRun((prev) => {
      const next = { ...prev };
      syncJobs.forEach((j) => {
        if (next[j.key] === undefined) next[j.key] = null;
      });
      return next;
    });
  }, [syncJobs]);

  // -------------------------------------------------------
  // Helpers
  // -------------------------------------------------------
  const pushLog = (msg) => {
    const stamp = new Date().toLocaleString();
    setLogLines((prev) => [`[${stamp}] ${msg}`, ...prev].slice(0, 300));
  };

  const canStart = (jobKey) => {
    if (activeJobKey) return false;
    if (status[jobKey] === STATUS.running) return false;
    return true;
  };

  const setJobRunning = (jobKey) =>
    setStatus((p) => ({ ...p, [jobKey]: STATUS.running }));

  const setJobSuccess = (jobKey) => {
    setStatus((p) => ({ ...p, [jobKey]: STATUS.success }));
    const stamp = new Date().toISOString().replace("T", " ").slice(0, 16);
    setLastRun((p) => ({ ...p, [jobKey]: stamp }));
  };

  const setJobError = (jobKey) =>
    setStatus((p) => ({ ...p, [jobKey]: STATUS.error }));

  // -------------------------------------------------------
  // Runner
  // -------------------------------------------------------
  const runJob = async (jobKey) => {
    if (!canStart(jobKey)) return;

    const fn = jobCallMap[jobKey];
    if (!fn) return;

    try {
      setActiveJobKey(jobKey);
      setProgress(0);

      setJobRunning(jobKey);
      pushLog(`Starting sync: ${jobKey}`);

      setProgress(20);
      const result = await fn(); // ✅ ONLY your bcSync* function
      setProgress(90);

      const ok = isOkResult(result);
      if (!ok) {
        throw new Error(extractFailureMessage(result));
      }

      setJobSuccess(jobKey);
      pushLog(`Completed sync: ${jobKey} (success)`);
    } catch (err) {
      setJobError(jobKey);
      pushLog(`Completed sync: ${jobKey} (error) - ${extractErrorMessage(err)}`);
    } finally {
      setProgress(0);
      setActiveJobKey(null);
    }
  };

  const handleClearLog = () => {
    setLogLines(["Ready. Select a sync action to begin."]);
    pushLog("Log cleared.");
  };

  const handleResetStatuses = () => {
    setStatus((prev) => {
      const next = { ...prev };
      Object.keys(next).forEach((k) => (next[k] = STATUS.idle));
      return next;
    });
    pushLog("Statuses reset to idle.");
  };

  // -------------------------------------------------------
  // Grouping (keeps same look)
  // -------------------------------------------------------
  const groupedJobs = useMemo(() => {
    const groups = {};
    syncJobs.forEach((j) => {
      groups[j.group] = groups[j.group] || [];
      groups[j.group].push(j);
    });
    return groups;
  }, [syncJobs]);

  const groupOrder = useMemo(() => ["Business Central"], []);

  // -------------------------------------------------------
  // UI
  // -------------------------------------------------------
  return (
    <div className="page-wrapper">
      <div className="content">
        {/* Header */}
        <div className="d-flex align-items-center justify-content-between flex-wrap gap-2 mb-3">
          <div>
            <h4 className="mb-1">Sync</h4>
            <div className="text-muted" style={{ fontSize: 13 }}>
              Business Central sync actions
            </div>
          </div>

          <div className="d-flex align-items-center gap-2">
            <button
              className="btn btn-outline-secondary"
              onClick={handleResetStatuses}
              disabled={!!activeJobKey}
            >
              <FeatherIcon icon="rotate-ccw" className="me-2" />
              Reset Status
            </button>

            <button
              className="btn btn-outline-danger"
              onClick={handleClearLog}
              disabled={!!activeJobKey}
            >
              <FeatherIcon icon="trash-2" className="me-2" />
              Clear Log
            </button>
          </div>
        </div>

        {/* Progress */}
        <div className="card mb-3">
          <div className="card-body">
            <div className="d-flex align-items-center justify-content-between mb-1">
              <div className="text-muted" style={{ fontSize: 13 }}>
                Active Job:{" "}
                <span className="fw-semibold">{activeJobKey || "—"}</span>
              </div>
              <div className="text-muted" style={{ fontSize: 13 }}>
                {progress ? `${progress}%` : ""}
              </div>
            </div>

            <div className="progress" style={{ height: 10 }}>
              <div
                className="progress-bar"
                role="progressbar"
                style={{ width: `${progress}%` }}
                aria-valuenow={progress}
                aria-valuemin="0"
                aria-valuemax="100"
              />
            </div>
          </div>
        </div>

        {/* Main layout */}
        <div className="row g-3">
          {/* Jobs */}
          <div className="col-12 col-lg-7">
            <div className="card h-100">
              <div className="card-header d-flex align-items-center justify-content-between">
                <div className="fw-semibold">
                  <FeatherIcon icon="list" className="me-2" />
                  Sync Types
                </div>
                <div className="text-muted" style={{ fontSize: 13 }}>
                  Individual sync actions
                </div>
              </div>

              <div className="card-body">
                {groupOrder
                  .filter((g) => groupedJobs[g]?.length)
                  .map((groupName) => (
                    <div key={groupName} className="mb-4">
                      <div className="d-flex align-items-center justify-content-between mb-2">
                        <div className="fw-bold">{groupName}</div>
                        <div className="text-muted" style={{ fontSize: 13 }}>
                          {groupedJobs[groupName].length} items
                        </div>
                      </div>

                      <div className="row g-3">
                        {groupedJobs[groupName].map((job) => (
                          <div className="col-12 col-md-6" key={job.key}>
                            <div className="border rounded-3 p-3 h-100 d-flex flex-column">
                              <div className="d-flex align-items-start justify-content-between">
                                <div className="d-flex align-items-start gap-2">
                                  <div
                                    className="rounded-3 border d-flex align-items-center justify-content-center"
                                    style={{ width: 40, height: 40 }}
                                  >
                                    <FeatherIcon icon={job.icon} />
                                  </div>

                                  <div>
                                    <div className="fw-bold">{job.title}</div>
                                    <div className="text-muted" style={{ fontSize: 13 }}>
                                      {job.description}
                                    </div>
                                  </div>
                                </div>

                                <div>{statusPill(status[job.key])}</div>
                              </div>

                              <div className="mt-2 text-muted" style={{ fontSize: 13 }}>
                                Last run:{" "}
                                <span className="fw-semibold">
                                  {lastRun[job.key] || "—"}
                                </span>
                              </div>

                              <div className="mt-auto pt-3 d-flex gap-2">
                                <button
                                  className="btn btn-outline-secondary w-100"
                                  disabled={!canStart(job.key)}
                                  onClick={() => runJob(job.key)}
                                  title={
                                    !canStart(job.key)
                                      ? "A sync is currently running."
                                      : `Sync ${job.key}`
                                  }
                                >
                                  <FeatherIcon icon="refresh-cw" className="me-2" />
                                  Sync
                                </button>

                                <button
                                  className="btn btn-outline-primary w-100"
                                  disabled
                                  title="Template: view counts/details later"
                                >
                                  <FeatherIcon icon="file-text" className="me-2" />
                                  Details
                                </button>
                              </div>
                            </div>
                          </div>
                        ))}
                      </div>
                    </div>
                  ))}
              </div>
            </div>
          </div>

          {/* Log */}
          <div className="col-12 col-lg-5">
            <div className="card h-100">
              <div className="card-header d-flex align-items-center justify-content-between">
                <div className="fw-semibold">
                  <FeatherIcon icon="terminal" className="me-2" />
                  Sync Log
                </div>
                <div className="text-muted" style={{ fontSize: 13 }}>
                  Most recent first
                </div>
              </div>

              <div className="card-body">
                <div
                  className="border rounded-3 p-3"
                  style={{
                    height: 640,
                    overflowY: "auto",
                    background: "#fff",
                    fontFamily:
                      'ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, "Liberation Mono", "Courier New", monospace',
                    fontSize: 12,
                  }}
                >
                  {logLines.map((line, idx) => (
                    <div key={idx} className="mb-1">
                      {line}
                    </div>
                  ))}
                </div>

                <div className="mt-3 d-flex justify-content-end gap-2">
                  <button
                    className="btn btn-outline-secondary"
                    onClick={() => pushLog("Test log line (template).")}
                    disabled={!!activeJobKey}
                  >
                    <FeatherIcon icon="plus-circle" className="me-2" />
                    Add Test Line
                  </button>
                </div>

                <div className="mt-3 text-muted" style={{ fontSize: 13 }}>
                  Hook counts/details later if your backend returns them.
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="py-3" />
      </div>
    </div>
  );
}
