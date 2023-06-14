class API {
  static baseUrl = "http://localhost:5272/api"; // URL base

  static getToken() {
    return sessionStorage.getItem("token");
  }

  static request(url, options) {
    const headers = {
      "Content-Type": "application/json",
      Authorization: `Bearer ${API.getToken()}`,
    };

    return fetch(`${API.baseUrl}${url}`, { ...options, headers })
      .then((response) => response.json())
      .catch((error) => {
        console.error("Error:", error);
        throw error;
      });
  }

  static get(url) {
    return API.request(url, { method: "GET" });
  }

  static post(url, data) {
    return API.request(url, {
      method: "POST",
      body: JSON.stringify(data),
    });
  }

  static put(url, data) {
    return API.request(url, {
      method: "PUT",
      body: JSON.stringify(data),
    });
  }

  static delete(url) {
    return API.request(url, { method: "DELETE" });
  }
}

export default API;
