import React, { useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { uploadProduct } from "../../../../../services/businessService";
import { useAuth0 } from "@auth0/auth0-react";
import "./addProduct.css";

export const AddProductPage = () => {
  const [productName, setProductName] = useState("");
  const [price, setPrice] = useState("");
  const { user } = useAuth0();
  //const [image, setImage] = useState(null);

  const navigate = useNavigate();
  const location = useLocation();
  const { campaing } = location.state;
  console.log(campaing);

  const handleSubmit = async (event) => {
    event.preventDefault();
    //const formData = new FormData();
    //formData.append("image", image, image.name);
    const userEmail = user.email;
    const defaultVariableIS = false;
    const defaultActivist = 0;
    uploadProduct(
      userEmail,
      campaing,
      productName,
      price,
      defaultVariableIS,
      defaultVariableIS,
      defaultActivist
    );

    navigate("/MyDonates");
  };

  return (
    <div className="formContainer">
      <form onSubmit={handleSubmit} className="formNInfo">
        <h1>Add new Product</h1>
        <div className="form-group">
          <label className="formLbl" htmlFor="name">
            Product Name:
          </label>
          <input
            type="name"
            className="form-control"
            id="name"
            value={productName}
            onChange={(event) => setProductName(event.target.value)}
          />
        </div>

        <div className="form-group">
          <label className="formLbl" htmlFor="price">
            Please choose the Product Price:
          </label>
          <input
            type="price"
            className="form-control"
            id="price"
            value={price}
            onChange={(event) => setPrice(event.target.value)}
          />
        </div>

        {/* <div className="form-group">
          <label className="formLbl" htmlFor="file">
            Please choose the Product photo:
          </label>
          <input
            type="file"
            className="form-control"
            id="file"
            value={price}
            onChange={(event) => setImage(event.target.files[0])}
          />
        </div> */}

        <button type="submit" className="btn btn-primary">
          Submit
        </button>
      </form>
    </div>
  );
};
