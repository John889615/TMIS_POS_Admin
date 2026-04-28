import api from '../posAPI';


export const getDebtorContacts = async (debtorId) => {
    try {
        const response = await api.post('/EntityData/list/all/entity/contacts', {EntityID : 8, EntityRecordID: debtorId });
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


export const addDebtorContact = async (data) => {
    try {
        const response = await api.post('/Debtor/add/debtor/contact', data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateDebtorContact = async (data) => {
    try {
        const response = await api.post('/Debtor/update/debtor/contact', data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
