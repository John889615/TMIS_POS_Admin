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

const buildSettingFormData = (rec = {}, isUpdate = false) => {
  const formData = new FormData();

  if (isUpdate) {
    formData.append("SettingID", String(rec?.SettingID ?? ""));
  }

  formData.append("Company", rec?.Company ?? "");
  formData.append("Email", rec?.Email ?? "");
  formData.append("HeadOfficeNo", rec?.HeadOfficeNo ?? "");
  formData.append("FK_CurrencyID", String(rec?.FK_CurrencyID ?? ""));

  if (rec?.ImageFile && rec.ImageFile instanceof File) {
    formData.append("ImageFile", rec.ImageFile, rec.ImageFile.name);
  }

  return formData;
};

export const getSettings = async () => {
  try {
    const response = await api.get("/EntityData/list/settings");
    return response.data;
  } catch (error) {
    const responseData = error?.response?.data;

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
    const formData = buildSettingFormData(rec, false);

    const response = await api.post("/EntityData/add/setting", formData, {
      transformRequest: [(data) => data],
      headers: {
        Accept: "*/*",
      },
    });

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
    const formData = buildSettingFormData(rec, true);

    const response = await api.post("/EntityData/update/setting", formData, {
      transformRequest: [(data) => data],
      headers: {
        Accept: "*/*",
      },
    });

    return response.data;
  } catch (error) {
    if (error?.response?.data) {
      return error.response.data;
    }

    return buildClientErrorResponse("Failed to update setting.", error);
  }
};