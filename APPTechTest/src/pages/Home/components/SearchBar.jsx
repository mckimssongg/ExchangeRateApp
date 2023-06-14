import React from "react";

const SearchBar = ({ onChange }) => {
  const handleInputChange = (e) => {
    const value = e.target.value;
    onChange(value);
  };

  return (
    <div>
      <input
        type="text"
        placeholder="Search by currency code"
        onChange={handleInputChange}
      />
    </div>
  );
};

export default SearchBar;
