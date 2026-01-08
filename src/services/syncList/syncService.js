import api from '../posAPI';


export const getAllUnits = async () => {
    try {
        const response = await api.get('/Sync/list/units');
        if (response.data && Array.isArray(response.data.Data)) {
            return response.data.Data;
        } else {
            throw new Error("Unexpected response format");
        }
    } catch (error) {
        if (error.response) {
        }
        throw new Error('Failed to fetch users. Please try again.');
    }
};

export const getAllTaxType = async () => {
    try {
        const response = await api.get('/Sync/list/tax/types');
        if (response.data && Array.isArray(response.data.Data)) {
            return response.data.Data;
        } else {
            throw new Error("Unexpected response format");
        }
    } catch (error) {
        if (error.response) {
        }
        throw new Error('Failed to fetch users. Please try again.');
    }
};