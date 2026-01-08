import api from '../posAPI';


export const getAllMenuItemProducts = async (menuId) => {
    try {
        const response = await api.post('/Menu/list/menu/item/products', { FK_MenuItemID: menuId });
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

export const newMenuItemProduct = async (data) => {
    try {
        const response = await api.post('/Menu/add/menu/item/product', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};

export const deleteMenuItemProduct = async (id) => {
    try {
        const response = await api.post('/Menu/remove/menu/item/product', { POS_MenuItemProductID: id }); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};