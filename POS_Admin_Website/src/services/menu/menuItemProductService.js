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

export const newDebtorMenuItemProduct = async (data) => {
    try {
        const response = await api.post('/Menu/add/debtor/menu/item/product', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};

export const reorderMenuItemProducts = async (fkMenuItemId, orderedIds) => {
    try {
        const response = await api.post('/Menu/reorder/menu/item/products', {
            FK_MenuItemID: fkMenuItemId,
            OrderedIDs: orderedIds,
        });
        return response.data;
    } catch (error) {
        return error.response?.data || {
            Success: false,
            Messages: ["Failed to reorder menu item products."],
        };
    }
};

export const reorderDebtorMenuItemProducts = async (fkDebtorMenuItemId, orderedIds) => {
    try {
        const response = await api.post('/Menu/reorder/debtor/menu/item/products', {
            FK_DebtorMenuItemID: fkDebtorMenuItemId,
            OrderedIDs: orderedIds,
        });
        return response.data;
    } catch (error) {
        return error.response?.data || {
            Success: false,
            Messages: ["Failed to reorder debtor menu item products."],
        };
    }
};

export const deleteDebtorMenuItemProduct = async (id) => {
    console.log("deleteDebtorMenuItemProduct ID:", id);

    try {
        const response = await api.post("/Menu/remove/debtor/menu/item/product", {
            POS_MenuItemProductID: Number(id),
        });

        console.log("deleteDebtorMenuItemProduct response", response.data);
        return response.data;
    } catch (error) {
        console.error("deleteDebtorMenuItemProduct error", error);
        return error.response?.data || {
            Success: false,
            Messages: ["Failed to delete menu item product."],
        };
    }
};