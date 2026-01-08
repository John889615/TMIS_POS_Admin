import api from '../posAPI';


export const getDebtorAddress = async (debtorId) => {
    debugger;
    try {
        const response = await api.post('/EntityData/list/all/entity/addresses', {EntityID : 8, EntityRecordID: debtorId });
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

export const getDebtorAddressType = async () => {
    try {
        const response = await api.post('/Debtor/list/address/types');
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

export const addDebtorAddress = async (data) => {
    try {
        const response = await api.post('/Debtor/add/debtor/address', data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateDebtorAddress = async (data) => {
    try {
        const response = await api.post('/Debtor/update/debtor/address', data);
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
