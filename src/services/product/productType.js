import api from '../posAPI';


export const getAllProductTypes = async () => {
    try {
        const response = await api.get('/invetory/list/product/types');
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


export const newProductType = async (data) => {
    try {
        const response = await api.post('/invetory/add/product/type', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateProductType = async (data) => {
    try {
        const response = await api.post('/invetory/update/product/type', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
