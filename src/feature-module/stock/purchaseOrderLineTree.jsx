import React, { useState, useEffect } from "react";
import { getAllProducts } from "../../services/product/product";
import { getAllPurchaseOrder, newPurchaseOrder, updatePurchaseOrder } from "../../services/stock/purchaseOrderService";
import { getAllPurchaseOrderLine, newPurchaseOrderLine } from "../../services/stock/purchaseOrderLineService";
import { useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { DndContext, useDraggable, useDroppable, DragOverlay } from "@dnd-kit/core";
import { Modal, Button, Form } from "react-bootstrap"
import {
    PlusCircle,
} from "react-feather";
import { getAllCostCenter } from "../../services/debtors/costCenter";
import PurchaseOrderForm from "../../core/modals/stocks/purchaseOrderFormModel";


const PurchaseOrderLineTree = () => {
    const { id } = useParams();
    const [purchaseOrdersWithProducts, setPurchaseOrdersWithProducts] = useState([]);
    const [productList, setProductList] = useState([]);
    const [openOrders, setOpenOrders] = useState({});
    const [activeProduct, setActiveProduct] = useState(null); // store picked product
    const debtorId = useSelector((state) => state.selectedDebtorStore);
    const [showModal, setShowModal] = useState(false);
    const [selectedProductId, setSelectedProductId] = useState(null);
    const [selectedOrder, setSelectedOrder] = useState(null);
    const [quantity, setQuantity] = useState(1);
    const [notes, setNotes] = useState("");
    const [modelShow, setModelShow] = useState(false);
    const [selectedData, setSelectedData] = useState(null);
    const [costCenterList, setCostCenterList] = useState([]);
    useEffect(() => {
        fetchPurchaseOrder();
        fetchRecords();
    }, [debtorId]);

    const fetchPurchaseOrder = async () => {
        try {
            const type = await getAllProducts();
            setProductList(type);

            const orders = await getAllPurchaseOrder(debtorId ?? 1);
            const ordersWithLines = await Promise.all(
                orders.map(async (order) => {
                    const lines = await getAllPurchaseOrderLine(order.POS_PurchaseOrderID);
                    return { ...order, Products: lines };
                })
            );
            setPurchaseOrdersWithProducts(ordersWithLines);

            // all open by default
            const collapseState = {};
            ordersWithLines.forEach((o) => {
                collapseState[o.POS_PurchaseOrderID] = true;
            });
            setOpenOrders(collapseState);
        } catch (err) {
            console.error("Failed to load purchase orders:", err.message);
        }
    };

    const fetchRecords = async () => {
        try {
            const type = await getAllCostCenter();
            setCostCenterList(type);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    };

    const toggleOrder = (orderId) => {
        setOpenOrders((prev) => ({
            ...prev,
            [orderId]: !prev[orderId],
        }));
    };

    // --- Draggable Product ---
    function DraggableProduct({ product }) {
        const { attributes, listeners, setNodeRef, isDragging } = useDraggable({
            id: `product-${product.POS_ProductID}`,
            data: product,
        });

        return (
            <li
                ref={setNodeRef}
                {...attributes}
                {...listeners}
                className={`list-group-item d-flex justify-content-between align-items-center ${isDragging ? "opacity-50" : ""
                    }`}
                style={{ cursor: "grab" }}
            >
                {product.ProductName || "Unnamed"}
            </li>
        );
    }

    // --- Droppable Order ---
    function DroppableOrder({ order, children }) {
        const { setNodeRef, isOver } = useDroppable({
            id: `order-${order.POS_PurchaseOrderID}`,
        });

        return (
            <div
                ref={setNodeRef}
                className={`p-2 border rounded ${isOver ? " bg-info bg-opacity-50 border-primary shadow" : ""
                    }`}
                style={{
                    transition: "background 0.2s, border 0.2s",
                    minHeight: "100px"
                }}
            >
                {children}
            </div>
        );
    }

    const handleDragStart = (event) => {
        if (event.active.id.startsWith("product-")) {
            setActiveProduct(event.active.data.current);
        }
    };

    const handleDragEnd = async (event) => {
        const { active, over } = event;
        setActiveProduct(null); // clear overlay

        if (
            active &&
            over &&
            active.id.startsWith("product-") &&
            over.id.startsWith("order-")
        ) {
            const productId = active.data.current.POS_ProductID;
            const orderId = parseInt(over.id.replace("order-", ""));
            const product = productList.find((p) => p.POS_ProductID === productId);
            try {
                setSelectedProductId(productId);
                setSelectedOrder(orderId);
                setQuantity(1);
                setNotes("");
                setShowModal(true);
                // TODO: call newPurchaseOrderLine API here if needed
            } catch (err) {
                console.error("Failed to add product to order:", err.message);
            }
        }
    };

    const handleSave = async () => {
        try {
            var data = {
                POS_PurchaseOrderLineID: null,
                FK_ProductID: selectedProductId,
                FK_PurchaseOrderID: selectedOrder,
                Quantity: quantity,
                Notes: notes,
            };
            const payload = {
                PurchaseOrderLines: [data], // ✅ Wrap in array and key
            };
            var response = await newPurchaseOrderLine(payload);
            setPurchaseOrdersWithProducts([]);
            fetchPurchaseOrder();
            setShowModal(false);
        } catch (err) {
            console.error("Failed to save order line:", err.message);
        }
    };

    const handleClose = () => setModelShow(false);
    const handleAddProduct = async (data) => {
        console.log("Data : ", data);
        try {
            if (data.POS_PurchaseOrderID) {
                await updatePurchaseOrder(data);
            }
            else {
                await newPurchaseOrder(data);
            }
            await fetchPurchaseOrder();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleShow = () => {
        setSelectedData(null);
        setModelShow(true)
    };

    const handleEditOrder = (record) => {
        setSelectedData(record);
        setModelShow(true);
    };


    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Purchase Orders</h4>
                        </div>
                    </div>
                    <div className="page-btn">
                        <Button variant="none" className="btn btn-added" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Order
                        </Button>
                    </div>
                </div>

                <DndContext
                    onDragStart={handleDragStart}
                    onDragEnd={handleDragEnd}
                >
                    <div className="row">
                        {/* Left: Collapsible Orders */}
                        <div className="col-lg-8">
                            {purchaseOrdersWithProducts.map((order) => (
                                <div className="card mb-3" key={order.POS_PurchaseOrderID}>
                                    {/* Header (click to toggle) */}
                                    <div
                                        className="card-header d-flex justify-content-between align-items-center"
                                        style={{ cursor: "pointer" }}
                                        onClick={() => toggleOrder(order.POS_PurchaseOrderID)}
                                    >
                                        <div className="d-flex align-items-center">
                                            <h6 className="mb-0 me-2">{order.OrderNumber}</h6>
                                            {/* Edit button next to order name */}
                                            <button
                                                type="button"
                                                className="btn btn-sm btn-outline-primary"
                                                onClick={(e) => {
                                                    e.stopPropagation(); // prevent toggle when clicking edit
                                                    handleEditOrder(order); // your edit function
                                                }}
                                            >
                                                <i className="feather-edit"></i>
                                            </button>
                                        </div>
                                        <span>
                                            {openOrders[order.POS_PurchaseOrderID] ? "−" : "+"}
                                        </span>
                                    </div>

                                    {/* Collapsible body */}
                                    {openOrders[order.POS_PurchaseOrderID] && (
                                        <DroppableOrder order={order}>
                                            <ul className="list-group list-group-flush">
                                                {order.Products.length > 0 ? (
                                                    order.Products.map((p) => (
                                                        <li
                                                            key={p.ProductID}
                                                            className="list-group-item d-flex justify-content-between"
                                                        >
                                                            <span>
                                                                {p.ProductName} - Qty: {p.Quantity}
                                                            </span>
                                                            <span className="text-muted">
                                                                {p.UnitCostExcl}
                                                            </span>
                                                        </li>
                                                    ))
                                                ) : (
                                                    <li className="list-group-item text-muted">
                                                        No products yet
                                                    </li>
                                                )}
                                            </ul>
                                        </DroppableOrder>
                                    )}
                                </div>
                            ))}
                        </div>

                        {/* Right: Products */}
                        <div className="col-lg-4">
                            <div className="card">
                                <div className="card-header">
                                    <h5 className="mb-0">Products (Drag to Order)</h5>
                                </div>
                                <div
                                    className="card-body"
                                    style={{ maxHeight: "600px", overflowY: "auto" }}
                                >
                                    {productList.length > 0 ? (
                                        <ul className="list-group">
                                            {productList.map((product) => (
                                                <DraggableProduct
                                                    key={product.POS_ProductID}
                                                    product={product}
                                                />
                                            ))}
                                        </ul>
                                    ) : (
                                        <p className="text-muted text-center">
                                            No products available
                                        </p>
                                    )}
                                </div>
                            </div>
                        </div>
                    </div>

                    {/* Overlay for dragged product */}
                    <DragOverlay>
                        {activeProduct ? (
                            <div className="list-group-item d-flex justify-content-between align-items-center bg-light shadow rounded">
                                {activeProduct.ProductName}
                                <span className="badge bg-primary rounded-pill">
                                    {activeProduct.Price || "N/A"}
                                </span>
                            </div>
                        ) : null}
                    </DragOverlay>
                </DndContext>

                <Modal key={1} show={showModal} onHide={() => setShowModal(false)} centered>
                    <Modal.Header closeButton>
                        <Modal.Title>Add Product to Order</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group className="mb-3">
                                <Form.Label>Quantity</Form.Label>
                                <Form.Control
                                    type="number"
                                    min="1"
                                    value={quantity}
                                    onChange={(e) => setQuantity(Number(e.target.value))}
                                />
                            </Form.Group>

                            <Form.Group className="mb-3">
                                <Form.Label>Notes</Form.Label>
                                <Form.Control
                                    as="textarea"
                                    rows={3}
                                    value={notes}
                                    onChange={(e) => setNotes(e.target.value)}
                                />
                            </Form.Group>
                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={() => setShowModal(false)}>
                            Cancel
                        </Button>
                        <Button variant="primary" onClick={handleSave}>
                            Save
                        </Button>
                    </Modal.Footer>
                </Modal>

                {modelShow && (
                    <PurchaseOrderForm key={2}
                        onSubmit={handleAddProduct}
                        showModel={modelShow}
                        handleClose={handleClose}
                        data={selectedData}
                        costCenterList={costCenterList}
                    />
                )}
            </div>
        </div>
    );
};

export default PurchaseOrderLineTree;
