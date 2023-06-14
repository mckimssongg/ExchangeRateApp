import React, { useState, useEffect, Fragment } from "react";
import ExchangeRateList from "./components/ExchangeRateList";
import SearchBar from "./components/SearchBar";
import CurrencyConverter from "./components/CurrencyConverter";
import "./styles/HomePage.css";
import Loading from "../../components/Loading/Loading";

export const HomePage = () => {
  const [exchangeRates, setExchangeRates] = useState([]);
  const [filteredExchangeRates, setFilteredExchangeRates] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchQuery, setSearchQuery] = useState("");

  useEffect(() => {
    const fetchExchangeRates = async () => {
      try {
        // Realizar la solicitud a la API
        const response = await fetch(
          "https://v6.exchangerate-api.com/v6/d3be19980fdfb9982011e7df/latest/USD"
        );
        const data = await response.json();
        const rates = data.conversion_rates;
        const exchangeRateList = Object.entries(rates).map(
          ([currency, rate]) => ({
            destinationCurrency: currency,
            exchangeRate: rate,
            baseCurrency: data.base_code,
            lastUpdate: data.time_last_update_utc,
          })
        );
        setExchangeRates(exchangeRateList);
        setFilteredExchangeRates(exchangeRateList);
      } catch (error) {
        // console.log("Error fetching exchange rates:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchExchangeRates();
  }, []);

  const handleSearch = (query) => {
    setSearchQuery(query);
    const filtered = exchangeRates.filter((rate) =>
      rate.destinationCurrency.toLowerCase().includes(query.toLowerCase())
    );
    setFilteredExchangeRates(filtered);
  };

  return (
    <div className="home-page mt-3">
      <div className="row m-0">
        <div className="col-12 col-md-8">
          <h1>Welcome to the Exchange Rate App</h1>
          {!loading ? (
            <Fragment>
              <SearchBar onChange={handleSearch} />
              <ExchangeRateList exchangeRates={filteredExchangeRates} />
            </Fragment>
          ) : (
            <div className="d-flex w-100 justify-content-center aling-items-center h-100">
              <Loading />
            </div>
          )}
        </div>
        <div className="col-12 col-md-4">
          <CurrencyConverter exchangeRates={exchangeRates} />
        </div>
      </div>
    </div>
  );
};
