import api from '../posAPI';


export const getAllAddressRegion = async () => {
    try {
        const response = await api.get('/EntityData/list/address/regions');
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

export const newAddressRegion = async (addressData) => {
    try {
        const response = await api.post('/EntityData/Add/address/regions', addressData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateAddressRegion = async (addressData) => {
    try {
        const response = await api.post('/EntityData/update/address/regions', addressData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};