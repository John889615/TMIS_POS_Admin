import api from '../posAPI';


export const getAllDebtorProduct = async (debtorId) => {
    try {
        const response = await api.post('/Stock/list/debtor/products', { DebtorID: debtorId });
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

export const newDebtorProduct = async (data) => {
    debugger;
    try {
        const response = await api.post('/Stock/add/debtor/product', data); // Use POST
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateDebtorProduct = async (data) => {
    try {
        const response = await api.post('/Stock/update/debtor/product', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};