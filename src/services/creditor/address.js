import api from '../posAPI';


export const getCreditorAddress = async (creditorId) => {
    debugger;
    try {
        const response = await api.post('/EntityData/list/all/entity/addresses', {EntityID : 1, EntityRecordID: creditorId });
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

export const addCreditorAddress = async (data) => {
    try {
        const response = await api.post('/Creditor/add/creditor/address', data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateCreditorAddress = async (data) => {
    try {
        const response = await api.post('/Creditor/update/creditor/address', data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
