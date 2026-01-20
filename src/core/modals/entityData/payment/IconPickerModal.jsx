import React, { useMemo, useState } from "react";
import PropTypes from "prop-types";
import { Modal } from "react-bootstrap";

const IconPickerModal = ({ show, onClose, iconList, onPick, selectedIconId }) => {
  const [search, setSearch] = useState("");
  const [category, setCategory] = useState("");

  const categories = useMemo(() => {
    const cats = [...new Set((iconList || []).map(x => x.Category).filter(Boolean))];
    return cats.sort();
  }, [iconList]);

  const filtered = useMemo(() => {
    const s = search.trim().toLowerCase();
    return (iconList || []).filter(x => {
      const matchesSearch =
        !s ||
        (x.IconPath || "").toLowerCase().includes(s) ||
        (x.Category || "").toLowerCase().includes(s);

      const matchesCat = !category || x.Category === category;
      return matchesSearch && matchesCat;
    });
  }, [iconList, search, category]);

  return (
    <Modal show={show} onHide={onClose} centered size="lg">
      <Modal.Header closeButton>
        <Modal.Title>Select an Icon</Modal.Title>
      </Modal.Header>

      <Modal.Body>
        <div className="d-flex gap-2 mb-3">
          <input
            className="form-control"
            placeholder="Search icon class or category..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
          />
          <select
            className="form-select"
            value={category}
            onChange={(e) => setCategory(e.target.value)}
            style={{ maxWidth: 220 }}
          >
            <option value="">All categories</option>
            {categories.map((c) => (
              <option key={c} value={c}>{c}</option>
            ))}
          </select>
        </div>

        <div
          style={{
            display: "grid",
            gridTemplateColumns: "repeat(auto-fill, minmax(90px, 1fr))",
            gap: "12px",
          }}
        >
          {filtered.map((item) => {
            const active = item.PaymentTypeIconID === selectedIconId;

            return (
              <button
                type="button"
                key={item.PaymentTypeIconID}
                onClick={() => onPick(item)}
                className={`btn ${active ? "btn-primary" : "btn-outline-secondary"}`}
                style={{
                  height: 90,
                  display: "flex",
                  flexDirection: "column",
                  justifyContent: "center",
                  alignItems: "center",
                  gap: 6,
                  borderWidth: active ? 2 : 1,
                }}
                title={item.IconPath}
              >
                {/* IMPORTANT: this is where the icon renders */}
                <i className={item.IconPath} style={{ fontSize: 24 }} />
                <div style={{ fontSize: 10, opacity: 0.8, textAlign: "center" }}>
                  {item.Category || "Icon"}
                </div>
              </button>
            );
          })}
        </div>

        {filtered.length === 0 && (
          <div className="text-muted text-center py-4">No icons found.</div>
        )}
      </Modal.Body>

      <Modal.Footer>
        <button type="button" className="btn btn-secondary" onClick={onClose}>
          Close
        </button>
      </Modal.Footer>
    </Modal>
  );
};

export default IconPickerModal;

IconPickerModal.propTypes = {
  show: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired,
  iconList: PropTypes.array.isRequired,
  onPick: PropTypes.func.isRequired,
  selectedIconId: PropTypes.number,
};
