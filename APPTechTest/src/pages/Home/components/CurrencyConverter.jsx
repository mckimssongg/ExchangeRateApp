import React, { useState } from "react";
import "../styles/CurrencyConverter.css";
import API from "../../../utils/Api";
import { toast } from "react-hot-toast";

const CurrencyConverter = ({ exchangeRates }) => {
  const [amount, setAmount] = useState(0);
  const [sourceCurrency, setSourceCurrency] = useState("USD");
  const [targetCurrency, setTargetCurrency] = useState("EUR");
  const [convertedAmount, setConvertedAmount] = useState(0);
  const user = JSON.parse(sessionStorage.getItem("user"));

  const handleAmountChange = (event) => {
    setAmount(parseFloat(event.target.value));
  };

  const handleSourceCurrencyChange = (event) => {
    setSourceCurrency(event.target.value);
  };

  const handleTargetCurrencyChange = (event) => {
    setTargetCurrency(event.target.value);
  };

  const convertCurrency = () => {
    const sourceRate = exchangeRates.find(
      (rate) => rate.destinationCurrency === sourceCurrency
    );
    const targetRate = exchangeRates.find(
      (rate) => rate.destinationCurrency === targetCurrency
    );

    if (sourceRate && targetRate) {
      const convertedValue =
        (amount / sourceRate.exchangeRate) * targetRate.exchangeRate;
      setConvertedAmount(convertedValue.toFixed(2));
    } else {
      setConvertedAmount(0);
    }

    const fetchData = API.post("/Transaction", {
      userRefId: user.userID,
      sourceCurrency: sourceCurrency,
      destinationCurrency: targetCurrency,
      exchangeRate: sourceRate.exchangeRate,
      destinationExchangeRate: targetRate.exchangeRate,
      amount: amount,
      createdDate: new Date(),
    });
    toast.promise(
      fetchData,
      {
        loading: "Loading",
        success: (response) => {
          if (response.status) {
            return response.value;
          }
        },
        error: (error) => {
          return "Server Error";
        },
      },
      {
        success: {
          duration: 3000,
        },
      }
    );
  };

  return (
    <div className="currency-converter">
      <h2>Currency Converter</h2>
      <div className="input-group">
        <label htmlFor="amount">Amount:</label>
        <input
          type="number"
          id="amount"
          value={amount}
          onChange={handleAmountChange}
        />
      </div>
      <div className="input-group">
        <label htmlFor="sourceCurrency">Source Currency:</label>
        <select
          id="sourceCurrency"
          value={sourceCurrency}
          onChange={handleSourceCurrencyChange}
        >
          {exchangeRates.map((rate) => (
            <option
              key={rate.destinationCurrency}
              value={rate.destinationCurrency}
            >
              {rate.destinationCurrency}
            </option>
          ))}
        </select>
      </div>
      <div className="input-group">
        <label htmlFor="targetCurrency">Target Currency:</label>
        <select
          id="targetCurrency"
          value={targetCurrency}
          onChange={handleTargetCurrencyChange}
        >
          {exchangeRates.map((rate) => (
            <option
              key={rate.destinationCurrency}
              value={rate.destinationCurrency}
            >
              {rate.destinationCurrency}
            </option>
          ))}
        </select>
      </div>
      <button className="btn btn-dark btn-sm" onClick={convertCurrency}>
        Convert
      </button>
      <p className="converted-amount">Converted Amount: {convertedAmount}</p>
    </div>
  );
};

export default CurrencyConverter;
