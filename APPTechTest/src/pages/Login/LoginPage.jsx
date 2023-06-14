import React, { Fragment, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useForm } from "../../hook/useForm";
import API from "../../utils/Api";
import toast from "react-hot-toast";

const getGreeting = () => {
  const timeOfDay = new Date().getHours();

  if (timeOfDay >= 6 && timeOfDay < 12) {
    return "☀️ Good morning,";
  } else if (timeOfDay >= 12 && timeOfDay < 18) {
    return "🍂 Good afternoon,";
  } else {
    return "🌙 Good evening,";
  }
};

export const LoginPage = () => {
  const navigate = useNavigate();
  const [errors, setErrors] = useState({});

  const { email, password, formState, onInputChange, onResetForm } = useForm({
    email: "",
    password: "",
  });

  const validate = (values) => {
    const errors = {};

    // Check if the email is valid.
    if (!/\b@dto.com\b/.test(values.email)) {
      errors.email = "Invalid email address.";
    }

    // Check if the password is valid.
    if (values.password.length < 8) {
      errors.password = "Password must be at least 8 characters long.";
    }
    return errors;
  };

  useEffect(() => {
    setErrors(validate(formState));
  }, [formState]);

  const onLogin = (e) => {
    e.preventDefault();

    if (Object.keys(errors).length > 0) {
      return;
    }

    const fetchData = API.post("/User/login", formState);
    toast.promise(
      fetchData,
      {
        loading: "Loading",
        success: (response) => {
          if (response.status) {
            sessionStorage.removeItem("token");
            sessionStorage.removeItem("user");
            sessionStorage.setItem("token", response.value.token);
            sessionStorage.setItem("user", JSON.stringify(response.value.user));
            onResetForm();
            navigate("/home", {
              replace: true,
              state: {
                logged: true,
              },
            });
            return `${getGreeting()} ${response.value.user.name}`;
          } else {
            return response.message;
          }
        },
        error: (error) => {
          return "Request Error";
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
    <main
      style={{ height: "600px" }}
      className="container d-flex align-items-center text-center"
    >
      <form
        onSubmit={onLogin}
        className="form-signin m-auto"
        style={{ width: "60%", maxWidth: "560px" }}
      >
        <h1 className="h3 mb-3 fw-normal">Please sign in</h1>
        <div className="form-floating">
          <input
            type="email"
            className="form-control rounded-2"
            id="email"
            name="email"
            value={email}
            onChange={onInputChange}
            required
            autoComplete="off"
          />
          {errors.email && (
            <div className="text-end text-danger pt-1">
              Please enter a valid email address.
            </div>
          )}
          <label htmlFor="email">Email address</label>
        </div>
        <div className="form-floating">
          <input
            type="password"
            name="password"
            id="password"
            className="form-control rounded-2"
            value={password}
            onChange={onInputChange}
            required
            autoComplete="off"
          />
          {errors.password && (
            <div className="text-end text-danger pt-1">
              Please enter a valid password.
            </div>
          )}
          <label htmlFor="floatingPassword">Password</label>
        </div>
        <button className="w-100 btn btn-lg btn-primary mt-5" type="submit">
          Sign in
        </button>
      </form>
    </main>
  );
};
