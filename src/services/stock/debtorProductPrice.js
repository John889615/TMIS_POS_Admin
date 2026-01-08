import api from '../posAPI';


export const getAllProductPriceById = async (prodId) => {
    try {
        const response = await api.post('/Stock/list/debtor/product/prices', { DebtorProductID: prodId });

        // API returns 400 inside response.data
        if (response.data?.StatusCode === 400) {
            return []; // ✅ return empty array
        }

        if (Array.isArray(response.data?.Data)) {
            return response.data.Data;
        }

        return []; // fallback
    } catch (error) {
        // If server actually throws a 400/500 HTTP error
        if (error.response?.status === 400) {
            return []; // ✅ return empty array here too
        }

        // Other unexpected errors
        console.error("Unexpected API error:", error);
        return []; // still safe to return empty list
    }
};


export const newProductPrice = async (data) => {
    try {
        const response = await api.post('/Stock/add/debtor/product/price', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateProductPrice = async (data) => {
    try {
        debugger;
        const response = await api.post('/Stock/update/debtor/product/price', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        debugger;
        return error.response.data;
    }
};
