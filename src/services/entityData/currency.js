import api from "../posAPI";

export const getAllCurrency = async () => {
  try {
    const response = await api.get("/EntityData/list/currencies");
    if (response.data && Array.isArray(response.data.Data)) {
      return response.data.Data;
    } else {
      throw new Error("Unexpected response format");
    }
  } catch (error) {
    throw new Error("Failed to fetch users. Please try again.");
  }
};

export const newCurrency = async (addressData) => {
  try {
    const response = await api.post("/EntityData/add/currency", addressData);
    console.log("response", response.data);
    return response.data;
  } catch (error) {
    return error.response.data;
  }
};

export const updateCurrency = async (addressData) => {
  try {
    const response = await api.post("/EntityData/update/currency", addressData);
    console.log("response", response.data);
    return response.data;
  } catch (error) {
    return error.response.data;
  }
};

export const listLocationCurrencies = async (debtorId) => {
  try {
    const response = await api.post("/EntityData/list/location/currencies", {
      LocationID: debtorId,
    });
    return response.data;
  } catch (error) {
    return error.response?.data || {
      Success: false,
      Messages: ["Failed to fetch location currencies."],
    };
  }
};

export const newLocationCurrency = async (data) => {
  try {
    const response = await api.post("/EntityData/add/location/currency", data);
    return response.data;
  } catch (error) {
    return error.response?.data || {
      Success: false,
      Messages: ["Failed to add location currency."],
    };
  }
};

export const deleteLocationCurrency = async (data) => {
  try {
    const response = await api.post("/EntityData/remove/location/currency", data);
    return response.data;
  } catch (error) {
    return error.response?.data || {
      Success: false,
      Messages: ["Failed to remove location currency."],
    };
  }
};