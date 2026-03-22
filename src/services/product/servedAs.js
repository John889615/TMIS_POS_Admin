import api from "../posAPI";

export const getAllServedAs = async () => {
    try {
        const response = await api.get("/inventory/list/servedas");

        if (response?.data && Array.isArray(response.data.Data)) {
            return response.data.Data;
        }

        return [];
    } catch (error) {
        console.error("getAllServedAs failed:", error?.response || error);
        throw new Error("Failed to fetch served as records. Please try again.");
    }
};

export const newServedAs = async (data) => {
    try {
        const payload = {
            ServedAsType: data?.ServedAsType?.trim() || "",
            Name: data?.Name?.trim() || "",
            IsDefault: Boolean(data?.IsDefault),
        };

        const response = await api.post("/inventory/add/servedas", payload);
        return response.data;
    } catch (error) {
        console.error("newServedAs failed:", error?.response || error);
        return error?.response?.data || {
            Success: false,
            Messages: ["Failed to add served as."],
            Data: null,
            Errors: [error?.message || "Unknown error"],
        };
    }
};

export const updateServedAs = async (data) => {
    try {
        const payload = {
            ServedAsID: Number(data?.ServedAsID) || 0,
            ServedAsType: data?.ServedAsType?.trim() || "",
            Name: data?.Name?.trim() || "",
            IsDefault: Boolean(data?.IsDefault),
        };

        const response = await api.post("/inventory/update/servedas", payload);
        return response.data;
    } catch (error) {
        console.error("updateServedAs failed:", error?.response || error);
        return error?.response?.data || {
            Success: false,
            Messages: ["Failed to update served as."],
            Data: null,
            Errors: [error?.message || "Unknown error"],
        };
    }
};

export const getAllServedAsProductsById = async (productId) => {
    try {
        const response = await api.post("/inventory/list/servedas/products", {
            ProductID: Number(productId) || 0,
        });

        if (response?.data && Array.isArray(response.data.Data)) {
            return response.data.Data;
        }

        return [];
    } catch (error) {
        console.error("getAllServedAsProductsById failed:", error?.response || error);
        throw new Error("Failed to fetch served as products.");
    }
};

export const newServedAsProduct = async (data) => {
    try {
        const payload = {
            ProductID: Number(data?.ProductID) || 0,
            ServedAsID: Number(data?.ServedAsID) || 0,
            IsQuantified: Boolean(data?.IsQuantified),
            Quantity: Number(data?.Quantity) || 0,
            IsDefault: Boolean(data?.IsDefault),
        };

        const response = await api.post("/inventory/add/servedas/product", payload);
        return response.data;
    } catch (error) {
        console.error("newServedAsProduct failed:", error?.response || error);
        return error?.response?.data || {
            Success: false,
            Messages: ["Failed to add served as product."],
            Data: null,
            Errors: [error?.message || "Unknown error"],
        };
    }
};

export const removeServedAsProduct = async (servedAsProductId) => {
    try {
        const response = await api.post("/inventory/remove/servedas/product", {
            ServedAsProductID: Number(servedAsProductId) || 0,
        });

        return response.data;
    } catch (error) {
        console.error("removeServedAsProduct failed:", error?.response || error);
        return error?.response?.data || {
            Success: false,
            Messages: ["Failed to remove served as product."],
            Data: null,
            Errors: [error?.message || "Unknown error"],
        };
    }
};

export const updateServedAsProduct = async (data) => {
    try {
        const payload = {
            ServedAsProductID: Number(data?.ServedAsProductID) || 0,
            ProductID: Number(data?.ProductID) || 0,
            ServedAsID: Number(data?.ServedAsID) || 0,
            IsQuantified: Boolean(data?.IsQuantified),
            Quantity: Number(data?.Quantity) || 0,
            IsDefault: Boolean(data?.IsDefault),
        };

        const response = await api.post("/inventory/update/servedas/product", payload);
        return response.data;
    } catch (error) {
        console.error("updateServedAsProduct failed:", error?.response || error);
        return error?.response?.data || {
            Success: false,
            Messages: ["Failed to update served as product."],
            Data: null,
            Errors: [error?.message || "Unknown error"],
        };
    }
};