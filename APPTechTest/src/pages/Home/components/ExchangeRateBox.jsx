import React from "react";

function formatDateString(dateString) {
  const date = new Date(dateString);

  const options = {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
    timeZone: "UTC",
  };

  return date.toLocaleString("en-US", options);
}

const ExchangeRateBox = ({ rate }) => {
  const { baseCurrency, destinationCurrency, exchangeRate, lastUpdate } = rate;

  return (
    <div className="exchange-rate-box d-flex flex-column justify-content-between">
      <h4>
        {baseCurrency} to {destinationCurrency}
      </h4>
      <div>
        <hr />
      </div>
      <p className="d-flex flex-column">
        Exchange Rate:
        <span>
          <strong>{exchangeRate}</strong>
        </span>
      </p>
      <div>
        <hr />
      </div>
      <p>Last Update: {formatDateString(lastUpdate)}</p>
    </div>
  );
};

export default ExchangeRateBox;
