import api from '../posAPI';


export const getAllEntitiesAddress = async () => {
    try {
        const response = await api.get('/EntityData/list/entity/addresses');
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

export const newEntitiesAddress = async (addressData) => {
    try {
        const response = await api.post('/EntityData/add/entity/address', addressData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateEntitiesAddress = async (addressData) => {
    try {
        const response = await api.post('/EntityData/update/entity/address', addressData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};