import api from '../posAPI';

// Build a real FormData from a plain object so axios sends the correct
// multipart/form-data body (with boundary) and ASP.NET's [FromForm] binder
// can read each field — including the File. Setting Content-Type manually
// breaks this; let axios derive it from the FormData instance.
const toFormData = (obj) => {
  const fd = new FormData();
  Object.entries(obj).forEach(([key, value]) => {
    if (value === null || value === undefined) return;
    if (value instanceof File || value instanceof Blob) {
      fd.append(key, value);
    } else if (typeof value === 'boolean') {
      fd.append(key, value ? 'true' : 'false');
    } else {
      fd.append(key, String(value));
    }
  });
  return fd;
};


export const getAllProducts = async () => {
    try {
        const response = await api.get('/inventory/list/products');
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
    // Explicit multipart header overrides posAPI's instance-level
    // 'application/json' default. axios sees the FormData body and
    // appends the correct boundary to this Content-Type automatically.
    const response = await api.post("/inventory/add/product", toFormData(data), {
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
        const response = await api.post('/inventory/update/product', toFormData(data), {
            headers: { "Content-Type": "multipart/form-data" },
        });
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};
