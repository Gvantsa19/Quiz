import React, { useState } from "react";
import axios from "axios";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import LoginPage from "./Login";
import Config from "../../config.json";

const Register = () => {
  const [errorMessage, setErrorMessage] = useState("");
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
  });
  const [registered, setRegistered] = useState(false);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(
        Config.Settings.CommonApi_BaseUrl + "User/register",
        formData
      );
      console.log(response.data);
      if (response.status === 200) {
        setRegistered(true);
      } else {
        setErrorMessage("Registration failed. Please try again.");
      }
    } catch (error) {
      console.error("Error registering:", error);
      if (
        error.response &&
        error.response.data &&
        error.response.data.errors &&
        error.response.data.errors.length > 0
      ) {
        const errorMessage = error.response.data.errors[0].messages[0];
        setErrorMessage(errorMessage);
      } else {
        setErrorMessage("An unexpected error occurred.");
      }
    }
  };

  if (registered) {
    return (
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          marginTop: "10%",
        }}
      >
        <LoginPage />
      </div>
    );
  }

  return (
    <div>
      <h2 style={{ textAlign: "center" }}>Registration</h2>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "70vh",
          width: "100%",
        }}
      >
        <Form onSubmit={handleSubmit}>
          <Form.Group className="mb-3" controlId="formBasicFirstName">
            <Form.Label style={{ fontSize: "20px", fontWeight: "bold" }}>
              FirstName
            </Form.Label>
            <Form.Control
              style={{ width: "600px", padding: "15px" }}
              value={formData.firstName}
              onChange={handleChange}
              placeholder="Enter FirstName"
              required
              type="text"
              name="firstName"
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="formBasicLastName">
            <Form.Label style={{ fontSize: "20px", fontWeight: "bold" }}>
              LastName
            </Form.Label>
            <Form.Control
              style={{ width: "600px", padding: "15px" }}
              value={formData.lastName}
              onChange={handleChange}
              required
              name="lastName"
              type="text"
              placeholder="Enter LastName"
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="formBasicEmail">
            <Form.Label style={{ fontSize: "20px", fontWeight: "bold" }}>
              Email address
            </Form.Label>
            <Form.Control
              style={{ width: "600px", padding: "15px" }}
              value={formData.email}
              onChange={handleChange}
              required
              name="email"
              type="email"
              placeholder="Enter email"
            />
            <Form.Text className="text-muted">
              We'll never share your email with anyone else.
            </Form.Text>
          </Form.Group>

          <Form.Group className="mb-3" controlId="formBasicPassword">
            <Form.Label style={{ fontSize: "20px", fontWeight: "bold" }}>
              Password
            </Form.Label>
            <Form.Control
              style={{ width: "600px", padding: "15px" }}
              value={formData.password}
              onChange={handleChange}
              required
              name="password"
              type="password"
              placeholder="Password"
            />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formBasicCheckbox">
            <Form.Check type="checkbox" label="Check me out" />
          </Form.Group>
          {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
          <Button
            style={{ maxWidth: "200px", width: "100%", height: "60px" }}
            variant="primary"
            type="submit"
          >
            Submit
          </Button>
        </Form>
      </div>
    </div>
  );
};

export default Register;
