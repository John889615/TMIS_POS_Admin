import api from '../posAPI';


export const getAllStockRequestLine = async (stockid) => {
    try {
        const response = await api.post('/Stock/list/stock/request/lines', { StockRequestID: stockid });
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


export const newStockRequestLine = async (data) => {
    try {
        const response = await api.post('/Stock/add/stock/request/line', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
