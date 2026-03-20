import api from "../posAPI";

const buildClientErrorResponse = (message, error) => ({
  Success: false,
  Messages: [message],
  Data: null,
  Errors: [error?.message || "Unknown error occurred."],
  ErrorCode: "ClientError",
  StatusCode: 500,
  Meta: null,
});

export const getSettings = async () => {
  try {
    const response = await api.get("/EntityData/list/settings");
    return response.data;
  } catch (error) {
    const responseData = error?.response?.data;

    // Ignore "no settings yet" and let the page show blank inputs
    if (
      error?.response?.status === 404 &&
      responseData?.ErrorCode === "SettingsNotFound"
    ) {
      return {
        Success: true,
        Messages: [],
        Data: null,
        Errors: [],
        ErrorCode: null,
        StatusCode: 200,
        Meta: null,
      };
    }

    if (responseData) {
      return responseData;
    }

    return buildClientErrorResponse("Failed to fetch settings.", error);
  }
};

export const newSetting = async (rec) => {
  try {
    const payload = {
      Company: rec?.Company ?? "",
      Email: rec?.Email ?? "",
      HeadOfficeNo: rec?.HeadOfficeNo ?? "",
      FK_CurrencyID: rec?.FK_CurrencyID ?? 0,
    };

    const response = await api.post("/EntityData/add/setting", payload);
    return response.data;
  } catch (error) {
    if (error?.response?.data) {
      return error.response.data;
    }

    return buildClientErrorResponse("Failed to create setting.", error);
  }
};

export const updateSetting = async (rec) => {
  try {
    const payload = {
      SettingID: rec?.SettingID ?? 0,
      Company: rec?.Company ?? "",
      Email: rec?.Email ?? "",
      HeadOfficeNo: rec?.HeadOfficeNo ?? "",
      FK_CurrencyID: rec?.FK_CurrencyID ?? 0,
    };

    const response = await api.post("/EntityData/update/setting", payload);
    return response.data;
  } catch (error) {
    if (error?.response?.data) {
      return error.response.data;
    }

    return buildClientErrorResponse("Failed to update setting.", error);
  }
};

export const uploadCompanyLogo = async (settingId, file) => {
  try {
    const formData = new FormData();

    formData.append("SettingID", settingId);
    formData.append("CompanyLogo", file); // 🔥 field name (adjust if backend differs)

    const response = await api.post(
      "/EntityData/upload/company/logo",
      formData,
      {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      }
    );

    return response.data;
  } catch (error) {
    return error.response?.data;
  }
};

export const uploadCompanyIcon = async (settingId, file) => {
  try {
    const formData = new FormData();

    formData.append("SettingID", settingId);
    formData.append("CompanyIcon", file); // 🔥 separate field

    const response = await api.post(
      "/EntityData/upload/company/icon",
      formData,
      {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      }
    );

    return response.data;
  } catch (error) {
    return error.response?.data;
  }
};