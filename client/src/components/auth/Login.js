import React, { useState } from "react";
import axios from "axios";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";

const Login = () => {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });
  const [errorMessage, setErrorMessage] = useState("");

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(
        "https://localhost:7264/api/User/login",
        formData
      );

      if (response.status !== 200) {
        setErrorMessage("Incorrect email or password");
      } else {
        const { token } = response.data.tokenString;
        localStorage.setItem("token", response.data.tokenString);
        window.location.href = "/mainPage";
      }
    } catch (error) {
      console.error("Error logging in:", error);
      // Handle other errors if needed
    }
  };

  return (
    <div>
      <h2>Login</h2>
      {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label style={{ fontSize: "20px", fontWeight: "bold" }}>
            Email address
          </Form.Label>
          <Form.Control
            style={{ width: "600px", padding: "15px" }}
            type="email"
            placeholder="Enter email"
            name="email"
            value={formData.email}
            onChange={handleChange}
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
            type="password"
            style={{ width: "600px", padding: "15px" }}
            placeholder="Password"
            name="password"
            value={formData.password}
            onChange={handleChange}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicCheckbox">
          <Form.Check type="checkbox" label="Check me out" />
        </Form.Group>
        <Button
          style={{ maxWidth: "200px", width: "100%", height: "60px" }}
          variant="primary"
          type="submit"
        >
          Sign in
        </Button>
      </Form>
    </div>
  );
};

export default Login;
