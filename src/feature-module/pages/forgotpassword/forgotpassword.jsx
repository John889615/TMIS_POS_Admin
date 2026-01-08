import React, { useState } from "react";
import ImageWithBasePath from "../../../core/img/imagewithbasebath";
import { Link, useNavigate } from "react-router-dom";
import { all_routes } from "../../../Router/all_routes";
import { forgetPassword } from "../../../services/authService";

const Forgotpassword = () => {
  const route = all_routes;
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    setError("");

    if (!email) {
      setError("Email is required.");
      return;
    }

    try {
      await forgetPassword(email);
      setMessage("Password reset instructions have been sent to your email.");
      setEmail("");

      // Redirect to login after 2 seconds
      setTimeout(() => {
        navigate(route.signin);
      }, 2000);
    } catch (err) {
      setError(err.message || "Something went wrong.");
    }
  };

  return (
    <>
     <div className="main-wrapper">
        <div className="account-content">
          <div className="login-wrapper login-new">
            <div className="container">
              <div className="login-content user-login">
                <div className="login-logo">
                  <ImageWithBasePath src="assets/img/logo.png" alt="img" />
                  <Link to={route.dashboard} className="login-logo logo-white">
                    <ImageWithBasePath src="assets/img/logo-white.png" alt />
                  </Link>
                </div>
                <form onSubmit={handleSubmit}>
                  <div className="login-userset">
                    <div className="login-userheading">
                      <h3>Forgot password?</h3>
                      <h4>
                        If you forgot your password, we’ll email you instructions to reset it.
                      </h4>
                    </div>

                    <div className="form-login">
                      <label>Email</label>
                      <div className="form-addons">
                        <input
                          type="email"
                          className="form-control"
                          value={email}
                          onChange={(e) => setEmail(e.target.value)}
                          required
                        />
                        <ImageWithBasePath
                          src="assets/img/icons/mail.svg"
                          alt="icon"
                        />
                      </div>
                      {error && <p className="text-danger mt-2">{error}</p>}
                      {message && <p className="text-success mt-2">{message}</p>}
                    </div>

                    <div className="form-login">
                      <button type="submit" className="btn btn-login">
                        Submit
                      </button>
                    </div>
                    <div className="signinform text-center">
                      <h4>
                        Return to
                        <Link to={route.signin} className="hover-a">
                          {" "}
                          login{" "}
                        </Link>
                      </h4>
                    </div>
                  </div>
                </form>
              </div>
              <div className="my-4 d-flex justify-content-center align-items-center copyright-text">
                <p>Copyright © 2023 DreamsPOS. All rights reserved</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Forgotpassword;
