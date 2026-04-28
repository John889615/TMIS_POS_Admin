import api from "../posAPI";

export const getAllCostCenter = async () => {
    try {
        const response = await api.get("/Debtor/list/cost/center");
        if (response.data && Array.isArray(response.data.Data)) {
            return response.data.Data;
        } else {
            throw new Error("Unexpected response format");
        }
    } catch (error) {
        throw new Error("Failed to fetch users. Please try again.");
    }
};

export const newCostCenter = async (data) => {
    try {
        const response = await api.post("/Debtor/add/cost/center", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response?.data;
    }
};

export const updateCostCenter = async (data) => {
    try {
        const response = await api.post("/Debtor/update/cost/center", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response?.data;
    }
};

export const getAllCostCenterTypes = async () => {
    try {
        const response = await api.get("/Debtor/list/cost/center/types");
        if (response.data && Array.isArray(response.data.Data)) {
            return response.data.Data;
        } else {
            throw new Error("Unexpected response format");
        }
    } catch (error) {
        throw new Error("Failed to fetch users. Please try again.");
    }
};

export const getCostCenterPrinters = async (data) => {
    try {
        const response = await api.post("/Debtor/list/cost/center/printers", data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response?.data;
    }
};

export const newCostCenterPrinter = async (data) => {
    try {
        const response = await api.post("/Debtor/add/cost/center/printer", data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response?.data;
    }
};

export const updateCostCenterPrinter = async (data) => {
    try {
        const response = await api.post("/Debtor/update/cost/center/printer", data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response?.data;
    }
};

export const toggleCostCenterPrinter = async (data) => {
    try {
        const response = await api.post("/Debtor/cost/center/printer/toggle", data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response?.data;
    }
};