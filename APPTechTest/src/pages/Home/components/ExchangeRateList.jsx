import React from "react";
import ExchangeRateBox from "./ExchangeRateBox";
import "../styles/ExchangeRateList.css";

const ExchangeRateList = ({ exchangeRates }) => {
  return (
    <div className="exchange-rate-list-container">
      <div className="exchange-rate-list">
        {exchangeRates.map((rate, index) => (
          <ExchangeRateBox key={index} rate={rate} />
        ))}
      </div>
    </div>
  );
};

export default ExchangeRateList;
