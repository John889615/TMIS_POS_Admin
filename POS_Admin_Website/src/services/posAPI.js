// src/api.js or api.ts if using TypeScript
import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_POS_API_URL,
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
