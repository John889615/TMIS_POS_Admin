import api from '../posAPI';


export const getAllSubstitutionById = async (prodId) => {
    try {
        const response = await api.post('/inventory/list/product/substitutions', { FK_ProductID: prodId });

        // API returns 400 inside response.data
        if (response.data?.StatusCode === 400) {
            return []; // ✅ return empty array
        }

        if (Array.isArray(response.data?.Data)) {
            return response.data.Data;
        }

        return []; // fallback
    } catch (error) {
        // If server actually throws a 400/500 HTTP error
        if (error.response?.status === 400) {
            return []; // ✅ return empty array here too
        }

        // Other unexpected errors
        console.error("Unexpected API error:", error);
        return []; // still safe to return empty list
    }
};


export const newSubstitution = async (data) => {
    try {
        const response = await api.post('/inventory/add/product/substitution', data); // Use POST
        debugger;
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateSubstitution = async (data) => {
    try {
        const response = await api.post('/inventory/update/product/substitution', data); // Use POST
        debugger;

        return response.data;
    } catch (error) {
        debugger;
        return error.response.data;
    }
};

export const removeSubstitution = async (productSubstitutionId) => {
  try {
    const payload = { ProductSubstitutionID: Number(productSubstitutionId) };

    const response = await api.post(
      "/inventory/remove/product/substitution", // using your API path as given
      payload
    );

    return response.data;
  } catch (error) {
    console.error("removeSubstitution error:", error?.response?.data || error);
    throw error;
  }
};
