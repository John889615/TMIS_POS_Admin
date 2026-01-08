import api from '../posAPI';


export const getAllPriceCode = async () => {
    try {
        const response = await api.post('/Stock/list/price/codes');
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


export const newPriceCode = async (data) => {
    try {
        const response = await api.post('/Stock/add/price/code', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updatePriceCode = async (data) => {
    try {
        const response = await api.post('/Stock/update/price/code', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
