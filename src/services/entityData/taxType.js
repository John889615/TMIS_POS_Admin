import api from '../posAPI';


export const getAllTaxType = async () => {
    try {
        const response = await api.get('/EntityData/list/tax/types');
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

export const newTaxType = async (rec) => {
    try {
        const response = await api.post('/EntityData/add/tax/types', rec); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateTaxType = async (rec) => {
    try {
        const response = await api.post('/EntityData/update/tax/types', rec); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};