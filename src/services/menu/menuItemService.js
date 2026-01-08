import api from '../posAPI';


export const getAllMenuItem = async (menuId) => {
    try {
        const response = await api.post('/Menu/list/menu/items', { MenuID: menuId });
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


export const newMenuItem = async (data) => {
    try {
        const response = await api.post('/Menu/add/menu/item', data, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        }); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateMenuItem = async (data) => {
    try {
        const response = await api.post('/Menu/update/menu/item', data, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        }); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
