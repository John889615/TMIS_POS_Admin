import api from '../posAPI';


export const getAllExtraById = async (prodId) => {
    try {
        const response = await api.post('/invetory/list/product/extras', { FK_ProductID: prodId });
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


export const newExtra = async (data) => {
    try {
        const response = await api.post('/invetory/add/product/extra', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateExtra = async (data) => {
    try {
        const response = await api.post('/invetory/update/product/extra', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
