import React, { createContext, useContext, useState, useEffect } from 'react';
import { jwtDecode } from 'jwt-decode';
import PropTypes from 'prop-types';
const AuthContext = createContext(undefined);

export function AuthProvider({ children }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decoded = jwtDecode(token);
        setUser({
          role: decoded.Role,
          username: decoded.Name,
          userId: decoded.UserId,
          email: decoded.email,
        });
        setIsAuthenticated(true);
      } catch (error) {
        localStorage.removeItem('token');
      }
    }
  }, []);

  const login = (data) => {

    localStorage.setItem('token', data.AccessToken);
    const decoded = jwtDecode(data.AccessToken);
    setUser({
      username: decoded.Name,
      userId: data.UserId,
      email: data.Email,
      tenantId: decoded.TenantID
    });
    setIsAuthenticated(true);
  };

  const logout = () => {
    localStorage.removeItem('token');
    setUser(null);
    setIsAuthenticated(false);
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

AuthProvider.propTypes = {
  children: PropTypes.node.isRequired,
};

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
}
