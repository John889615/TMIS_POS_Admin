import api from '../posAPI';


export const getAllSlipPrinter = async () => {
    try {
        const response = await api.get('/EntityData/list/slip/printers');
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

export const newSlipPrinter = async (slipPrinterData) => {
    try {
        const response = await api.post('/EntityData/add/slip/printer', slipPrinterData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateSlipPrinter = async (slipPrinterData) => {
    try {
        const response = await api.post('/EntityData/update/slip/printer', slipPrinterData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};