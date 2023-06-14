import { Navbar } from "../components/Navbar";
import React, { Fragment } from "react";
import { Toaster } from "react-hot-toast";

function Layout() {
  return (
    <Fragment>
      <Navbar />
      <Toaster position="top-right" />
    </Fragment>
  );
}

export default Layout;
