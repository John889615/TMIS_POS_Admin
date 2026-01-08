import api from '../posAPI';


export const getAllCurrency = async () => {
    try {
        const response = await api.get('/EntityData/list/currencies');
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

export const newCurrency = async (addressData) => {
    try {
        const response = await api.post('/EntityData/add/currency', addressData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateCurrency = async (addressData) => {
    try {
        const response = await api.post('/EntityData/update/currency', addressData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};