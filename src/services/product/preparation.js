import api from '../posAPI';


export const getAllPreparation = async (prodId) => {
    try {
        const response = await api.post('/inventory/list/product/preparation', { FK_ProductID: prodId });
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


export const newPreparation = async (data) => {
    try {
        const response = await api.post('/inventory/add/product/preparation', data); // Use POST
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updatePreparation = async (data) => {
    try {
        const response = await api.post('/inventory/update/product/preparation', data); // Use POST
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};

export const removePreparation = async (productPreparationId) => {
  try {
    const payload = { ProductPreparationID: Number(productPreparationId) };

    const response = await api.post(
      "/inventory/remove/product/preparation",
      payload
    );

    return response.data;
  } catch (error) {
    // ✅ don’t hide the real error
    console.error("removePreparation error:", error);
    throw error;
  }
};
