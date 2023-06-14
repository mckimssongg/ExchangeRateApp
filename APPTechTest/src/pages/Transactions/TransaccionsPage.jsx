import React, { useState, useEffect } from "react";
import Transaction from "./components/TransactionLog/TransactionLog";
import API from "../../utils/Api";
import Loading from "../../components/Loading/Loading";
import "./TransactionPage.css";

export const TransactionPage = () => {
  const [transactions, setTransactions] = useState([]);
  const [countTransactions, setCountTransactions] = useState(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Obtener las transacciones desde la API
    setLoading(true);
    API.get("/Transaction")
      .then((response) => {
        if (response.status) {
          setTransactions(response.value);
          setCountTransactions(response.value.length);
        }
      })
      .catch((error) => {
        setError(error);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  return (
    <div>
      <div className="mt-4 d-flex justify-content-around ps-5">
        <h2 className="m-0">Transaction Logs</h2>
        <h3 className="m-0 align-text-bottom pt-2">
          Transaction Count: {countTransactions}
        </h3>
      </div>
      {!error ? (
        !loading ? (
          <div className="container">
            <TransactionList transactions={transactions} />
          </div>
        ) : (
          <div className="text-center mt-5">
            <Loading />
          </div>
        )
      ) : (
        <div className="text-center mt-3">
          <h2>There was an error loading the transactions</h2>
        </div>
      )}
    </div>
  );
};

const TransactionList = ({ transactions }) => {
  if (transactions && transactions.length === 0) {
    return (
      <div className="text-center mt-5">
        <h2>There are no transactions to show</h2>
      </div>
    );
  } else {
    return (
      <ul className="list-container">
        {transactions.map((transaction, index) => (
          <Transaction key={index} transaction={transaction} />
        ))}
      </ul>
    );
  }
};
