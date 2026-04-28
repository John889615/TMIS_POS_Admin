import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_PORTAL_API_URL,
  timeout: 50000,
  headers: {
    "Content-Type": "application/json",
  },
});

// 🔐 Add interceptor to inject token
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// ✅ Auto-logout on 401/403
api.interceptors.response.use(
  (response) => response,
  (error) => {
    const status = error?.response?.status;

    if (status === 401 || status === 403) {
      localStorage.removeItem("token");
      localStorage.removeItem("refreshToken");
      localStorage.removeItem("user");

      // Force redirect to login
      window.location.href = "/signin";
    }

    return Promise.reject(error);
  }
);

export default api;