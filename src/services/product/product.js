import api from '../posAPI';


export const getAllProducts = async () => {
    try {
        const response = await api.get('/invetory/list/products');
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

export const syncAllProducts = async () => {
  const res = await api.get('/bc/products'); // returns true
  return res.data === true;
};


export const newProduct = async (data) => {
  try {
    const response = await api.post("/invetory/add/product", data, {
      headers: { "Content-Type": "multipart/form-data" },
    });

    const payload = response?.data;

    // ✅ Your API uses { Success, Messages, Errors, ... }
    if (payload?.Success === false) {
      const msg =
        (Array.isArray(payload?.Messages) && payload.Messages.join("\n")) ||
        (Array.isArray(payload?.Errors) && payload.Errors.join("\n")) ||
        payload?.ErrorCode ||
        "Product save failed.";

      // attach payload so UI can read it
      const err = new Error(msg);
      err.response = { data: payload };
      throw err;
    }

    return payload;
  } catch (error) {
    // If axios throws, error.response.data is your payload
    const payload = error?.response?.data;

    if (payload) {
      const msg =
        (Array.isArray(payload?.Messages) && payload.Messages.join("\n")) ||
        (Array.isArray(payload?.Errors) && payload.Errors.join("\n")) ||
        payload?.ErrorCode ||
        "Product save failed.";

      const err = new Error(msg);
      err.response = { data: payload };
      throw err;
    }

    // fallback
    throw error;
  }
};


export const updateProduct = async (data) => {
    try {
        const response = await api.post('/invetory/update/product', data, {
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
