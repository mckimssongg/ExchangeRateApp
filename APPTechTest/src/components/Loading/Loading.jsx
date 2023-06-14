import React from "react";
import "./Loading.css";

function Loading() {
  return (
    <div className="loading-container">
      <div className="loading-spinner"></div>
      <span className="fs-3">Loading...</span>
    </div>
  );
}

export default Loading;
