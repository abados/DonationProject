import React, { useState } from "react";

export const UsersReport = ({ Userslist, result }) => {
  const [countForTable] = useState(1);
  console.log(Userslist);
  console.log(result);
  if (result === "Business users") {
    return (
      <div className="my-report-tbl">
        <table className="table table-striped">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">User Name</th>
              <th scope="col">User Email</th>
              <th scope="col">User Cellphone</th>
              <th scope="col">Company Name</th>
            </tr>
          </thead>
          <tbody>
            {Userslist &&
              Userslist.map((User, index) => {
                return (
                  <>
                    <tr>
                      <th scope="row">{countForTable + index}</th>
                      <td>{User.fullName}</td>
                      <td>{User.email}</td>
                      <td>{User.cellPhone}</td>
                      <td>{User.businessName}</td>
                    </tr>
                  </>
                );
              })}
          </tbody>
        </table>
      </div>
    );
  } else if (result === "Nonprofits users") {
    return (
      <div className="my-report-tbl">
        <table className="table table-striped">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">User Name</th>
              <th scope="col">User Email</th>
              <th scope="col">User Cellphone</th>
              <th scope="col">Organization Name</th>
              <th scope="col">Organization Url</th>
              <th scope="col">Organization Description</th>
            </tr>
          </thead>
          <tbody>
            {Userslist &&
              Userslist.map((User, index) => {
                return (
                  <>
                    <tr>
                      <th scope="row">{countForTable + index}</th>
                      <td>{User.fullName}</td>
                      <td>{User.email}</td>
                      <td>{User.cellPhone}</td>
                      <td>{User.organizationName}</td>
                      <td>{User.organizationUrl}</td>
                      <td>{User.organizationDescription}</td>
                    </tr>
                  </>
                );
              })}
          </tbody>
        </table>
      </div>
    );
  } else if (result === "Activists users") {
    return (
      <div className="my-report-tbl">
        <table className="table table-striped">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">User Name</th>
              <th scope="col">User Email</th>
              <th scope="col">User Cellphone</th>
              <th scope="col">Address</th>
              <th scope="col">Twitter Acount</th>
              <th scope="col">Earnings</th>
            </tr>
          </thead>
          <tbody>
            {Userslist &&
              Userslist.map((User, index) => {
                return (
                  <>
                    <tr>
                      <th scope="row">{countForTable + index}</th>
                      <td>{User.fullName}</td>
                      <td>{User.email}</td>
                      <td>{User.cellPhone}</td>
                      <td>{User.address}</td>
                      <td>{User.TwitterAcount}</td>
                      <td>{User.Earnings}</td>
                    </tr>
                  </>
                );
              })}
          </tbody>
        </table>
      </div>
    );
  } else {
    return <h1>The Money amount of all the users is:{Userslist}</h1>;
  }
};
