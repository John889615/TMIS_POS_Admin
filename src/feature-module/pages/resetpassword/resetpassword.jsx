import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { resetPassword } from "../../../services/authService"; // 👈 Import service
import { all_routes } from "../../../Router/all_routes";
import ImageWithBasePath from "../../../core/img/imagewithbasebath";

const Resetpassword = () => {
  const route = all_routes;
  const location = useLocation();
  const navigate = useNavigate();

  const searchParams = new URLSearchParams(location.search);
  const token = searchParams.get("token");

  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [isPasswordVisible, setPasswordVisible] = useState(false);
  const [isConfirmPasswordVisible, setConfirmPasswordVisible] = useState(false);
  const handleSubmit = async (e) => {
    e.preventDefault();

    if (newPassword !== confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    try {
      const payload = {
        NewPassword: newPassword,
        ResetToken: token,
      };
      console.log("Reset Password Request JSON:", JSON.stringify(payload));
      await resetPassword(payload);
      setSuccess("Password reset successful!");
      setError("");
      setTimeout(() => {
        navigate(route.signin); // Redirect to login
      }, 2000);
    } catch (err) {
      setError(err.message || "Something went wrong.");
      setSuccess("");
    }
  };

  const togglePasswordVisibility = () => {
    setPasswordVisible((prevState) => !prevState);
  };

  const toggleConfirmPasswordVisibility = () => {
    setConfirmPasswordVisible((prevState) => !prevState);
  };

  return (
    <div className="main-wrapper">
      <div className="account-content">
        <div className="login-wrapper login-new">
          <div className="login-content user-login">
            <div className="login-logo">
              <ImageWithBasePath src="assets/img/logo.png" alt="img" />
              <a href={route.dashboard} className="login-logo logo-white">
                <ImageWithBasePath src="assets/img/logo-white.png" alt />
              </a>
            </div>
            <form onSubmit={handleSubmit}>
              <div className="login-userset">
                <div className="login-userheading">
                  <h3>Reset password?</h3>
                  <h4>Enter New Password &amp; Confirm Password to get inside</h4>
                </div>

                {error && <div className="alert alert-danger">{error}</div>}
                {success && <div className="alert alert-success">{success}</div>}

                <div className="form-login">
                  <label>New Password</label>
                  <div className="pass-group">
                    <input
                      type={isPasswordVisible ? "text" : "password"}
                      className="pass-inputa"
                      value={newPassword}
                      onChange={(e) => setNewPassword(e.target.value)}
                      required
                    />
                    <span
                      className={`fas toggle-password ${isPasswordVisible ? "fa-eye" : "fa-eye-slash"
                        }`}
                      onClick={togglePasswordVisibility}
                    ></span>
                  </div>
                </div>
                <div className="form-login">
                  <label>Confirm New Password</label>
                  <div className="pass-group">
                    <input
                      type={isConfirmPasswordVisible ? "text" : "password"}
                      className="pass-inputs"
                      value={confirmPassword}
                      onChange={(e) => setConfirmPassword(e.target.value)}
                      required
                    />
                    <span
                      className={`fas toggle-password ${isConfirmPasswordVisible ? "fa-eye" : "fa-eye-slash"
                        }`}
                      onClick={toggleConfirmPasswordVisibility}
                    ></span>
                  </div>
                </div>
                <div className="form-login">
                  <button type="submit" className="btn btn-login">
                    Change Password
                  </button>
                </div>
                <div className="signinform text-center">
                  <h4>
                    Return to{" "}
                    <a href={route.signin} className="hover-a">
                      login
                    </a>
                  </h4>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Resetpassword;
