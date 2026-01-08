import api from '../api';


export const getAllRoles = async () => {
    try {
        const response = await api.get('/auth/list/roles'); // Adjust endpoint if needed
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


export const newRole = async (roleData) => {
    try {
        const response = await api.post('/auth/add/role', roleData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};

export const updateRole = async (roleData) => {
    try {
        const response = await api.post('/auth/update/role', roleData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};