import api from '../posAPI';


export const getAllSubmittedPurchaseOrder = async () => {
    try {
        const response = await api.post('/Stock/list/submitted/purchase/orders');
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


export const newPurchaseOrderSubmitted = async (data) => {
    try {
        const response = await api.post('/Stock/update/submitted/purchase/order/status', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};