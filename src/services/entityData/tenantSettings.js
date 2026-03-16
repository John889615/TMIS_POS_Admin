import api from '../posAPI';


export const getSettings = async () => {
  try {
    const response = await api.get("/EntityData/list/settings");
    return response.data;
  } catch (error) {
    if (error.response && error.response.data) {
      return error.response.data;
    }

    return {
      Success: false,
      Messages: ["Failed to fetch settings."],
      Data: null,
      Errors: [error.message || "Unknown error occurred."],
      ErrorCode: "ClientError",
      StatusCode: 500,
      Meta: null,
    };
  }
};

export const newSetting = async (rec) => {
    try {
        const response = await api.post('/EntityData/add/setting', rec); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};


export const updateSetting = async (rec) => {
    try {
        const response = await api.post('/EntityData/update/setting', rec); // Use POST
        console.log("response", response.data);
        return response.data;
    } catch (error) {
        return error.response.data;
    }
};