import api from './api';

/**
 * @param {{ userName: string, password: string }} credentials
 * @param {string} email
 * @param {{ newPassword: string, token: string }} resetPassword
 * @returns {Promise<Object>} AuthResponse (usually contains a token and user info)
*/

export const loginUser = async (credentials) => {
    try {
        const response = await api.post('/auth/authenticate', {
            Username: credentials.userName,
            Password: credentials.password,
        });
        return response.data;
    } catch (error) {
        if (error.response) {
            //toast.error("Username or password is incorrect. Please try again.");
        }
        throw new Error('Username or password is incorrect. Please try again.');
    }
};


export const forgetPassword = async (email) => {
    try {
        const response = await api.post('/auth/request/password/reset', {
            email: email,
        });
        return response.data;
    } catch (error) {
        if (error.response) {
            // toast.error("Failed to send password reset email.");
        }
        throw new Error('Failed to send password reset email. Please try again.');
    }
};

export const resetPassword = async (resetPassword) => {
    console.log("Reset Password:", resetPassword);
    try {
        const response = await api.post('/auth/password/reset', {
            NewPassword: resetPassword.NewPassword,
            ResetToken: resetPassword.ResetToken,
        });
        return response.data;   
    } catch (error) {
        console.log("Password Reset Error", error);
        if (error.response) {
            //toast.error("Username or password is incorrect. Please try again.");
        }
        throw new Error('Username or password is incorrect. Please try again.');
    }
};