import React from "react";
import { Link } from "react-router-dom";
import Login from "./auth/Login";

const Home = () => {
  return (
    <div style={{ display: "flex", justifyContent: "center", alignItems:"center", height:"100vh", width:"100%" }}>
      <div>
        <h2>Welcome to Famous Quotes Quiz</h2>
        <Login />
        <p style={{fontSize:"20px"}}>
          Don't have an account? <Link to="/register">Register here</Link>.
        </p>
      </div>
    </div>
  );
};

export default Home;
