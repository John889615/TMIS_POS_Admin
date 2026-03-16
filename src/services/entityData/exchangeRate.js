import api from "../posAPI";

export const listExchangeRates = async () => {
  try {
    const response = await api.get("/EntityData/list/exchange/rates");
    return response.data;
  } catch (error) {
    if (error.response && error.response.data) {
      return error.response.data;
    }

    return {
      Success: false,
      Messages: ["Failed to fetch exchange rates."],
      Data: [],
      Errors: [error.message || "Unknown error occurred."],
      ErrorCode: "ClientError",
      StatusCode: 500,
      Meta: null,
    };
  }
};

export const newExchangeRate = async (rec) => {
  try {
    const response = await api.post("/EntityData/add/exchange/rate", rec);
    return response.data;
  } catch (error) {
    if (error.response && error.response.data) {
      return error.response.data;
    }

    return {
      Success: false,
      Messages: ["Failed to add exchange rate."],
      Data: null,
      Errors: [error.message || "Unknown error occurred."],
      ErrorCode: "ClientError",
      StatusCode: 500,
      Meta: null,
    };
  }
};

export const updateExchangeRate = async (rec) => {
  try {
    const response = await api.post("/EntityData/update/exchange/rate", rec);
    return response.data;
  } catch (error) {
    if (error.response && error.response.data) {
      return error.response.data;
    }

    return {
      Success: false,
      Messages: ["Failed to update exchange rate."],
      Data: null,
      Errors: [error.message || "Unknown error occurred."],
      ErrorCode: "ClientError",
      StatusCode: 500,
      Meta: null,
    };
  }
};