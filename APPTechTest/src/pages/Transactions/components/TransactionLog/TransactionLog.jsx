import React from "react";
import "./TransactionLog.css";

const formatDate = (date) => {
  const dateObj = new Date(date);
  const options = {
    month: "2-digit",
    day: "2-digit",
    year: "numeric",
    timeZone: "UTC",
  };
  return dateObj.toLocaleDateString("en-US", options);
};

const Transaction = ({ transaction }) => {
  const {
    userName,
    sourceCurrency,
    destinationCurrency,
    exchangeRate,
    destinationExchangeRate,
    amount,
    createdDate,
  } = transaction;

  return (
    <li className="transaction-item">
      <div className="transaction-field">
        <span className="transaction-label">Username</span>
        <strong>{userName}</strong>
      </div>
      <div className="transaction-field">
        <span className="transaction-label">Source Currency:</span>
        <strong>{sourceCurrency}</strong>
      </div>
      <div className="transaction-field">
        <span className="transaction-label">Destination Currency:</span>
        <strong>{destinationCurrency}</strong>
      </div>
      <div className="transaction-field">
        <span className="transaction-label">Exchange Rate:</span>
        <strong>{exchangeRate}</strong>
      </div>
      <div className="transaction-field">
        <span className="transaction-label">Destination Exchange Rate:</span>
        <strong>{destinationExchangeRate}</strong>
      </div>
      <div className="transaction-field">
        <span className="transaction-label">Amount:</span>
        <strong>{amount}</strong>
      </div>
      <div className="transaction-field">
        <span className="transaction-label">Created Date:</span>
        <strong>{formatDate(createdDate)}</strong>
      </div>
    </li>
  );
};

export default Transaction;
