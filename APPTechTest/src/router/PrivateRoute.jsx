import { Navigate, useLocation } from "react-router-dom";

export const PrivateRoute = ({ children }) => {
  const { state } = useLocation();

  const user = JSON.parse(sessionStorage.getItem("user"));

  if (user) {
    return children;
  } else {
    return <Navigate to="/login" />;
  }
};
