// ImagePreviewModal.jsx ✅ (REUSABLE)
// Put this somewhere like:
// src/core/modals/common/ImagePreviewModal.jsx
// Then import it wherever you need it.

import React, { useEffect, useState } from "react";
import { Modal, Button } from "react-bootstrap";

const ImagePreviewModal = ({
  show,
  onHide,
  src,
  title = "Image Preview",
  alt,
  footer = true, // show/hide footer buttons
}) => {
  const [failed, setFailed] = useState(false);

  useEffect(() => {
    // reset error state each time new image loads / modal opens
    setFailed(false);
  }, [src, show]);

  return (
    <Modal show={show} onHide={onHide} centered size="lg">
      <Modal.Header closeButton>
        <Modal.Title style={{ fontSize: 16 }}>{title}</Modal.Title>
      </Modal.Header>

      <Modal.Body className="text-center">
        {!src ? (
          <div className="text-muted">No image</div>
        ) : failed ? (
          <div className="text-muted">
            Could not load image.
            <div style={{ fontSize: 12, marginTop: 6 }}>{src}</div>
          </div>
        ) : (
          <img
            src={src}
            alt={alt || title}
            style={{
              maxWidth: "100%",
              maxHeight: "70vh",
              objectFit: "contain",
              borderRadius: 10,
            }}
            onError={() => setFailed(true)}
          />
        )}
      </Modal.Body>

      {footer && (
        <Modal.Footer>
          <Button variant="secondary" onClick={onHide}>
            Close
          </Button>
        </Modal.Footer>
      )}
    </Modal>
  );
};

export default ImagePreviewModal;