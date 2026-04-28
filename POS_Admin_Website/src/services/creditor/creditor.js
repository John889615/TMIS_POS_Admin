import api from '../posAPI';


export const getAllCreditor = async () => {
    try {
        const response = await api.get('/Creditor/list/creditors');
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


export const newCreditor = async (data) => {
    try {
        const response = await api.post('/Creditor/add/creditor', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateCreditor = async (data) => {
    try {
        const response = await api.post('/Creditor/update/creditor', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};

export const getAllCreditorTypes = async () => {
    try {
        const response = await api.get('/Creditor/list/creditor/types');
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