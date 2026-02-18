import api from '../posAPI';


export const getAllCombinationById = async (prodId) => {
    try {
        const response = await api.post('/invetory/list/product/combinations', { FK_ProductID: prodId });

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


export const newCombination = async (data) => {
    try {
        const response = await api.post('/invetory/add/product/combination', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const removeCombination = async (productCombinationId) => {
  try {
    const payload = { ProductCombinationID: Number(productCombinationId) };

    const response = await api.post(
      "/invetory/remove/product/combination", // ✅ fix spelling if needed
      payload
    );

    return response.data;
  } catch (error) {
    // ✅ don’t hide the real error
    console.error("removeCombination error:", error);
    throw error;
  }
};



export const updateCombination = async (data) => {
    try {
        debugger;
        const response = await api.post('/invetory/update/product/combination', data); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        debugger;
        return error.response.data;
    }
};
