import api from '../posAPI';


export const getAllStockRequest = async (debtorID) => {
    try {
        const response = await api.post('/Stock/list/stock/requests', { ToDebtorID: debtorID });
        if (response.data && Array.isArray(response.data.Data)) {
            return response.data.Data;
        } else {
            throw new Error("Unexpected response format");
        }
    } catch (error) {
        if (error.response) {
            // You can use a toast or console.log here for user-friendly error reporting
        }
        throw new Error('Failed to fetch users. Please try again.');
    }
};


export const newStockRequest = async (data) => {
    try {
        const response = await api.post('/Stock/add/stock/request', data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateStockRequest = async (data) => {
    try {
        const response = await api.post('/Stock/update/stock/request', data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const submitStockRequest = async (data) => {
    try {
        const response = await api.post('/Stock/submit/stock/request', data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const approveStockRequest = async (data) => {
    try {
        const response = await api.post('/Stock/approve/stock/request', data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
