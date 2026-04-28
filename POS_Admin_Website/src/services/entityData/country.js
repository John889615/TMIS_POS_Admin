import api from '../posAPI';


export const getAllCountry = async () => {
    try {
        const response = await api.get('/EntityData/list/countries');
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

export const newAddress = async (addressData) => {
    try {
        const response = await api.post('/EntityData/add/Country', addressData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateAddress = async (addressData) => {
    try {
        const response = await api.post('/EntityData/update/Country', addressData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};