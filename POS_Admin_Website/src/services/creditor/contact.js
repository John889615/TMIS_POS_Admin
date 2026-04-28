import api from '../posAPI';


export const getCreditorContacts = async (creditorId) => {
    try {
        const response = await api.post('/EntityData/list/all/entity/contacts', {EntityID : 1, EntityRecordID: creditorId });
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


export const addCreditorContact = async (data) => {
    try {
        const response = await api.post('/Creditor/add/creditor/contact', data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateCreditorContact = async (data) => {
    try {
        const response = await api.post('/Creditor/update/creditor/contact', data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
