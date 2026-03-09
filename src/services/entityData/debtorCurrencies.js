import api from '../posAPI';

export const listLocationCurrencies = async (debtorId) => {
  const response = await apiService.post("/EntityData/list/location/currency", {
    FK_DebtorID: debtorId,
  });
  return response;
};

export const newLocationCurrency = async (data) => {
  const response = await apiService.post("/EntityData/add/location/currency", data);
  return response;
};

export const deleteLocationCurrency = async (data) => {
  const response = await apiService.post("/EntityData/remove/location/currency", data);
  return response;
};