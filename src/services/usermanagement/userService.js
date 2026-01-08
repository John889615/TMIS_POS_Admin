import api from '../api';


export const getAllUsers = async () => {
    const token = localStorage.getItem('token');
    console.log("Token For Bearer for User", token);
    try {
        const response = await api.get('/auth/list/users');
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

export const newUser = async (userData) => {
    try {
        const json = JSON.stringify(userData);
        console.log("User Adding Json", json);
        const response = await api.post('/auth/add/user', userData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const getUserRole = async (userId) => {
  try {
    const response = await api.post('/auth/list/user/role', {
      UserId: userId
    });
    console.log("response", response.data);
    return response.data;
  } catch (error) {
    console.error("Error fetching user role", error);
    return error?.response?.data || { error: "Unknown error occurred" };
  }
};


export const assignNewRole = async (roleData) => {
    try {
        const response = await api.post('/auth/add/user/role', roleData); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};

export const deleteUserRole = async (userRoleId) => {
  try {
    const response = await api.post('/auth/delete/user/role', {
      UserRoleID: userRoleId
    });
    console.log("response", response.data);
    return response.data;
  } catch (error) {
    console.error("Error fetching user role", error);
    return error?.response?.data || { error: "Unknown error occurred" };
  }
};