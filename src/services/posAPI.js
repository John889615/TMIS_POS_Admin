// src/api.js or api.ts if using TypeScript
import axios from 'axios';

const api = axios.create({
  //baseURL: "https://tmis.co.za/TMIS_POS/Portal_Api/api",
  baseURL: "https://localhost:44392/api",
  timeout: 50000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// 🔐 Add interceptor to inject token
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export default api;
