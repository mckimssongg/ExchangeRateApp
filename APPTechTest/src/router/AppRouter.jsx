import { Route, Routes } from "react-router-dom";
import { HomePage, LoginPage, NotFound, TransactionPage } from "../pages";
import { PrivateRoute } from "./PrivateRoute";
import Layout from "../layout/Layout";

export const AppRouter = () => {
  return (
    <>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="/" element={<LoginPage />} />
          <Route path="login" element={<LoginPage />} />
          <Route
            path="home"
            element={
              <PrivateRoute>
                <HomePage />
              </PrivateRoute>
            }
          />
          <Route
            path="transactions"
            element={
              <PrivateRoute>
                <TransactionPage />
              </PrivateRoute>
            }
          />
          <Route path="*" element={<NotFound />} />
        </Route>
      </Routes>
    </>
  );
};
