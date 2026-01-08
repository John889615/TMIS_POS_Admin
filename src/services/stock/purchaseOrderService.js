import api from '../posAPI';


export const getAllPurchaseOrder = async (debtorID) => {
    try {
        const response = await api.post('/Stock/list/purchase/orders', { DebtorID: debtorID });
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


export const newPurchaseOrder = async (data) => {
    try {
        const response = await api.post('/Stock/add/purchase/order', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updatePurchaseOrder = async (data) => {
    try {
        const response = await api.post('/Stock/update/purchase/order', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
