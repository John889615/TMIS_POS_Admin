import api from '../posAPI';

const throwIfFailed = (payload, fallbackMsg) => {
  if (payload?.Success === false) {
    const msg =
      (Array.isArray(payload?.Messages) && payload.Messages.join("\n")) ||
      (Array.isArray(payload?.Errors) && payload.Errors.join("\n")) ||
      payload?.ErrorCode ||
      fallbackMsg ||
      "Request failed.";

    const err = new Error(msg);
    err.response = { data: payload };
    throw err;
  }
  return payload;
};

export const getAllProductCategory = async () => {
    try {
        const response = await api.get('/inventory/list/product/categories');
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


export const newProductCategory = async (data) => {
  try {
    const response = await api.post("/inventory/add/product/category", data);
    return throwIfFailed(response?.data, "Category add failed.");
  } catch (error) {
    const payload = error?.response?.data;
    if (payload) return throwIfFailed(payload, "Category add failed.");
    throw error;
  }
};

export const updateProductCategory = async (data) => {
  try {
    const response = await api.post("/inventory/update/product/category", data);
    return throwIfFailed(response?.data, "Category update failed.");
  } catch (error) {
    const payload = error?.response?.data;
    if (payload) return throwIfFailed(payload, "Category update failed.");
    throw error;
  }
};
