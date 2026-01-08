import api from '../posAPI';


export const getAllCostCenterProduct = async (costId) => {
    try {
        const response = await api.post('/Stock/list/cost/center/products', { CostCenterId: costId });
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

export const newCostCenterProduct = async (data) => {
    debugger;
    try {
        const response = await api.post('/Stock/add/cost/center/product', data); // Use POST
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateCostCenterProduct = async (data) => {
    try {
        const response = await api.post('/Stock/update/cost/center/product', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};