import api from '../posAPI';


export const getAllMenu = async (debtorId) => {
    try {
        const response = await api.post('/Menu/list/menus', { DebtorID: debtorId });
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

export const getMenuWithItem = async () => {
    try {
        const response = await api.post('/Menu/list/all/menu/tree');
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

export const newMenu = async (data) => {
    try {
        const response = await api.post('/Menu/add/menu', data, {
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


export const updateMenu = async (data) => {
    try {
        const response = await api.post('/Menu/update/menu', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const getMenuTree = async (menuId) => {
    try {
        const response = await api.post('/Menu/list/menu/tree', { MenuID: menuId });
        if (response.data.Success) {
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

export const getCampMenuTree = async (menuId) => {
    try {
        const response = await api.post('/Menu/list/debtor/menu/tree', { DebtorMenuID: menuId });
        if (response.data.Success) {
            return response.data.Data;
        } else {
            throw new Error("Unexpected response format");
        }
    } catch (error) {
        if (error.response) {
            // You can use a toast or console.log here for user-friendly error reporting
        }
        throw new Error('Failed to fetch debtor/menu/tree. Please try again.');
    }
};


export const copyMenu = async (data) => {
    try {
        const response = await api.post('/Menu/copy/menu', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
