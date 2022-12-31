import React, { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { addActivistUser } from "../../../services/activistService";
import { FormContext } from "../../../context/context";
import "./css/addingAInfo.css";

export const ActivistUserForm = ({ onFormSubmit }) => {
  const [fullName, setName] = useState("");
  const [email, setEmail] = useState("");
  const [cellPhone, setPhone] = useState("");
  const [address, setAddress] = useState("");
  const [TwitterAcount, setTwitter] = useState("");
  const { setFormSubmitted } = useContext(FormContext);

  const navigate = useNavigate();

  const handleSubmit = (event) => {
    event.preventDefault();
    const newOwnerToAdd = {
      fullName,
      address,
      cellPhone,
      email,
      TwitterAcount,
    };
    if (
      fullName === "" ||
      address === "" ||
      cellPhone === "" ||
      email === "" ||
      TwitterAcount === ""
    ) {
      alert("Please enter all required");
    } else {
      addActivistUser(newOwnerToAdd);
      setFormSubmitted(true);
      //navigate("/");

      //alert("we recive your comment, thanks for it");

      // setName("");
      // setAddress("");
      // setPhone("");
      // setEmail("");
      // setTwitter("");
    }
  };

  return (
    <form onSubmit={handleSubmit} className="formAInfo">
      <h1>Please fill in you details</h1>
      <div className="form-group">
        <label className="formLbl" htmlFor="name">
          Activist full name:
        </label>
        <input
          type="text"
          className="form-control"
          id="name"
          value={fullName}
          onChange={(event) => setName(event.target.value)}
        />
      </div>
      <div className="form-group">
        <label className="formLbl" htmlFor="email">
          Email Address:
        </label>
        <input
          type="email"
          className="form-control"
          id="email"
          value={email}
          onChange={(event) => setEmail(event.target.value)}
        />
      </div>
      <div className="form-group">
        <label className="formLbl" htmlFor="tel">
          cellPhone:
        </label>
        <input
          type="tel"
          className="form-control"
          id="phone"
          value={cellPhone}
          onChange={(event) => setPhone(event.target.value)}
        />
      </div>

      <div className="form-group">
        <label className="formLbl" htmlFor="phone">
          Address:
        </label>
        <input
          type="tel"
          className="form-control"
          id="message"
          value={address}
          onChange={(event) => setAddress(event.target.value)}
        />
      </div>
      <div className="form-group">
        <label className="formLbl" htmlFor="name">
          Twitter Acount:
        </label>
        <input
          type="name"
          className="form-control"
          id="name"
          value={TwitterAcount}
          onChange={(event) => setTwitter(event.target.value)}
        />
      </div>

      <button type="submit" className="btn btn-primary">
        Submit
      </button>
      <h2>All fields are necessary</h2>
    </form>
  );
};
