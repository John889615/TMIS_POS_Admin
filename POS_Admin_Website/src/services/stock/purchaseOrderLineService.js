import api from '../posAPI';


export const getAllPurchaseOrderLine = async (purchaseOrderId) => {
    try {
        const response = await api.post('/Stock/list/purchase/order/lines', { PurchaseOrderID: purchaseOrderId });
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

export const newPurchaseOrderLine = async (data) => {
    debugger;
    try {
        const response = await api.post('/Stock/add/purchase/order/line', data); // Use POST
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
