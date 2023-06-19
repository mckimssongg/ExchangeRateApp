import React, { useState } from "react";
import { Link, Outlet, useLocation, useNavigate } from "react-router-dom";
import Sidebar from "./Sidebar/Sidebar";

const Navbar = () => {
  const { state } = useLocation();
  const navigate = useNavigate();
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);

  const onLogout = () => {
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("user");
    navigate("/login", {
      replace: true,
    });
  };

  const toggleSidebar = () => {
    setIsSidebarOpen(!isSidebarOpen);
  };

  if (sessionStorage.getItem("token") === null) {
    return (
      <div>
        <Outlet />
      </div>
    );
  }
  const getGreeting = () => {
    const timeOfDay = new Date().getHours();
  
    if (timeOfDay >= 6 && timeOfDay < 12) {
      return "☀️ Good morning";
    } else if (timeOfDay >= 12 && timeOfDay < 18) {
      return "🍂 Good afternoon";
    } else {
      return "🌙 Good evening";
    }
  };
  
  return (
    <div className="d-flex flex-column vh-100 ">
      <header
        className="d-flex px-0 pt-2"
        style={{ backgroundColor: "#a4bdbc" }}
      >
        <div className="menu-icon px-4 btn-hover" onClick={toggleSidebar}>
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="46"
            height="54"
            fill="currentColor"
            className="bi bi-list"
            viewBox="0 0 16 16"
          >
            <path
              fillRule="evenodd"
              d="M2.5 12a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5zm0-4a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5zm0-4a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5z"
            />
          </svg>
        </div>
        <h1>
          <Link
            to="home"
            style={{
              textDecoration: "none",
            }}
          >
            🐈
          </Link>
        </h1>
        {getGreeting()}
      </header>

      <div className="container-fluid px-0 flex-grow-1">
        <div className="row m-0 h-100">
          {isSidebarOpen && (
            <div
              className="col-12 col-md-2 px-0 pt-2"
              style={{ backgroundColor: "#becccb" }}
            >
              <Sidebar state={state} onLogout={onLogout} />
            </div>
          )}
          <div
            className={isSidebarOpen ? "col-12 col-md-10 px-0" : "col-12 px-0"}
          >
            <Outlet />
          </div>
        </div>
      </div>
    </div>
  );
};

export { Navbar };
