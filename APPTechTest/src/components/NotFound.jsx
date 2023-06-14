import React from "react";
import { NavLink } from "react-router-dom";

export function NotFound() {
  const user = JSON.parse(sessionStorage.getItem("user"));

  if (user) {
    return (
      <div
        style={{ width: "100%", height: "400px" }}
        className="d-flex justify-content-center align-items-center m-0 fs-1"
      >
        <div className="d-flex flex-column">
          <p>Not Found Page</p>
          <NavLink to="/home" className="btn btn-primary ms-3">
            Go to home
          </NavLink>
        </div>
      </div>
    );
  } else {
    return (
      <div
        style={{ width: "100%", height: "400px" }}
        className="d-flex justify-content-center align-items-center m-0 fs-1"
      >
        <div className="d-flex flex-column">
          <p>Not Found Page</p>
          <NavLink to="/login" className="btn btn-primary ms-3">
            Go to Login
          </NavLink>
        </div>
      </div>
    );
  }
}
