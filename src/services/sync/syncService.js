import api from '../posAPI';

export const bcSyncAll = async () => {
  try {
    const response = await api.get("/bc/all", {
    timeout: 10 * 60 * 1000, // ✅ 10 minutes just for this call
  });

    // If backend returns boolean true/false
    if (typeof response.data === "boolean") {
      return response.data; // true/false
    }

    // If backend returns { Data: true } or { data: true }
    if (typeof response.data?.Data === "boolean") {
      return response.data.Data;
    }

    // If backend returns ApiResponse style { Success: true, Message: "...", Data: ... }
    if (typeof response.data?.Success === "boolean") {
      return response.data.Success;
    }

    // fallback: return raw data so UI can inspect it
    return response.data;
  } catch (error) {
    // show real server error if present
    const apiMsg =
      error?.response?.data?.message ||
      error?.response?.data?.error?.message ||
      error?.response?.data ||
      error?.message;

    throw new Error(
      typeof apiMsg === "string" ? apiMsg : JSON.stringify(apiMsg)
    );
  }
};

export const bcSyncUnits = async () => {
  try {
    const response = await api.get("/bc/units");

    // If backend returns boolean true/false
    if (typeof response.data === "boolean") {
      return response.data; // true/false
    }

    // If backend returns { Data: true } or { data: true }
    if (typeof response.data?.Data === "boolean") {
      return response.data.Data;
    }

    // If backend returns ApiResponse style { Success: true, Message: "...", Data: ... }
    if (typeof response.data?.Success === "boolean") {
      return response.data.Success;
    }

    // fallback: return raw data so UI can inspect it
    return response.data;
  } catch (error) {
    // show real server error if present
    const apiMsg =
      error?.response?.data?.message ||
      error?.response?.data?.error?.message ||
      error?.response?.data ||
      error?.message;

    throw new Error(
      typeof apiMsg === "string" ? apiMsg : JSON.stringify(apiMsg)
    );
  }
};

export const bcSyncLocations = async () => {
  try {
    const response = await api.get("/bc/locations");

    // If backend returns boolean true/false
    if (typeof response.data === "boolean") {
      return response.data; // true/false
    }

    // If backend returns { Data: true } or { data: true }
    if (typeof response.data?.Data === "boolean") {
      return response.data.Data;
    }

    // If backend returns ApiResponse style { Success: true, Message: "...", Data: ... }
    if (typeof response.data?.Success === "boolean") {
      return response.data.Success;
    }

    // fallback: return raw data so UI can inspect it
    return response.data;
  } catch (error) {
    // show real server error if present
    const apiMsg =
      error?.response?.data?.message ||
      error?.response?.data?.error?.message ||
      error?.response?.data ||
      error?.message;

    throw new Error(
      typeof apiMsg === "string" ? apiMsg : JSON.stringify(apiMsg)
    );
  }
};

export const bcSyncCategories = async () => {
  try {
    const response = await api.get("/bc/product/categories");

    // If backend returns boolean true/false
    if (typeof response.data === "boolean") {
      return response.data; // true/false
    }

    // If backend returns { Data: true } or { data: true }
    if (typeof response.data?.Data === "boolean") {
      return response.data.Data;
    }

    // If backend returns ApiResponse style { Success: true, Message: "...", Data: ... }
    if (typeof response.data?.Success === "boolean") {
      return response.data.Success;
    }

    // fallback: return raw data so UI can inspect it
    return response.data;
  } catch (error) {
    // show real server error if present
    const apiMsg =
      error?.response?.data?.message ||
      error?.response?.data?.error?.message ||
      error?.response?.data ||
      error?.message;

    throw new Error(
      typeof apiMsg === "string" ? apiMsg : JSON.stringify(apiMsg)
    );
  }
};

export const bcSyncPriceCodes = async () => {
  try {
    const response = await api.get("/bc/price/codes");

    // If backend returns boolean true/false
    if (typeof response.data === "boolean") {
      return response.data; // true/false
    }

    // If backend returns { Data: true } or { data: true }
    if (typeof response.data?.Data === "boolean") {
      return response.data.Data;
    }

    // If backend returns ApiResponse style { Success: true, Message: "...", Data: ... }
    if (typeof response.data?.Success === "boolean") {
      return response.data.Success;
    }

    // fallback: return raw data so UI can inspect it
    return response.data;
  } catch (error) {
    // show real server error if present
    const apiMsg =
      error?.response?.data?.message ||
      error?.response?.data?.error?.message ||
      error?.response?.data ||
      error?.message;

    throw new Error(
      typeof apiMsg === "string" ? apiMsg : JSON.stringify(apiMsg)
    );
  }
};

export const bcSyncProducts = async () => {
  try {
    const response = await api.get("/bc/products");

    // If backend returns boolean true/false
    if (typeof response.data === "boolean") {
      return response.data; // true/false
    }

    // If backend returns { Data: true } or { data: true }
    if (typeof response.data?.Data === "boolean") {
      return response.data.Data;
    }

    // If backend returns ApiResponse style { Success: true, Message: "...", Data: ... }
    if (typeof response.data?.Success === "boolean") {
      return response.data.Success;
    }

    // fallback: return raw data so UI can inspect it
    return response.data;
  } catch (error) {
    // show real server error if present
    const apiMsg =
      error?.response?.data?.message ||
      error?.response?.data?.error?.message ||
      error?.response?.data ||
      error?.message;

    throw new Error(
      typeof apiMsg === "string" ? apiMsg : JSON.stringify(apiMsg)
    );
  }
};

export const bcSyncProductlocations = async () => {
  try {
    const response = await api.get("/bc/product/locations", {
    timeout: 10 * 60 * 1000, // ✅ 10 minutes just for this call
  });

    // If backend returns boolean true/false
    if (typeof response.data === "boolean") {
      return response.data; // true/false
    }

    // If backend returns { Data: true } or { data: true }
    if (typeof response.data?.Data === "boolean") {
      return response.data.Data;
    }

    // If backend returns ApiResponse style { Success: true, Message: "...", Data: ... }
    if (typeof response.data?.Success === "boolean") {
      return response.data.Success;
    }

    // fallback: return raw data so UI can inspect it
    return response.data;
  } catch (error) {
    // show real server error if present
    const apiMsg =
      error?.response?.data?.message ||
      error?.response?.data?.error?.message ||
      error?.response?.data ||
      error?.message;

    throw new Error(
      typeof apiMsg === "string" ? apiMsg : JSON.stringify(apiMsg)
    );
  }
};

export const bcSyncProductLocationPrices = async () => {
  try {
    const response = await api.get("/bc/product/location/prices", {
    timeout: 10 * 60 * 1000, // ✅ 10 minutes just for this call
  });

    // If backend returns boolean true/false
    if (typeof response.data === "boolean") {
      return response.data; // true/false
    }

    // If backend returns { Data: true } or { data: true }
    if (typeof response.data?.Data === "boolean") {
      return response.data.Data;
    }

    // If backend returns ApiResponse style { Success: true, Message: "...", Data: ... }
    if (typeof response.data?.Success === "boolean") {
      return response.data.Success;
    }

    // fallback: return raw data so UI can inspect it
    return response.data;
  } catch (error) {
    // show real server error if present
    const apiMsg =
      error?.response?.data?.message ||
      error?.response?.data?.error?.message ||
      error?.response?.data ||
      error?.message;

    throw new Error(
      typeof apiMsg === "string" ? apiMsg : JSON.stringify(apiMsg)
    );
  }
};