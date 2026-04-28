import React from "react";
import { Navigate, Outlet } from "react-router-dom";

function isTokenExpired(token) {
  try {
    const payload = JSON.parse(atob(token.split(".")[1]));
    const expMs = payload.exp * 1000;
    return Date.now() >= expMs;
  } catch {
    return true; // malformed token = treat as expired
  }
}

const RequireAuth = () => {
  const token = localStorage.getItem("token");

  // No token or expired token -> kick to login
  if (!token || isTokenExpired(token)) {
    localStorage.removeItem("token");
    localStorage.removeItem("refreshToken"); // if you use one
    localStorage.removeItem("user");         // if you store user
    return <Navigate to="/signin" replace />;
  }

  // Token ok -> allow route
  return <Outlet />;
};

export default RequireAuth;