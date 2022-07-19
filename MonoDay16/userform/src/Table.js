import React, { useState } from 'react'
import './App.css';
import Form from './Form';
import jsonData from './data.json';

function Table() {
  const [userData, setUserData] = useState(jsonData);
  
  const tableRows = userData.map((info) => {
      return (
      <tr>
          <td>{info.id}</td>
          <td>{info.username}</td>
          <td>{info.password}</td>
          <td>{info.email}</td>
          <td>{info.county}</td>
      </tr>
    );
  });

  const addRows = (data) => {
    const totalUsers = userData.length;
    data.id = totalUsers + 1;
    const updatedUserData = [...userData];
    updatedUserData.push(data);
    setUserData(updatedUserData);
  };

  return(
    <div>
      <div className="cBody">
        <Form func={addRows} />
      </div>
      <div className="cFooter">
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Username</th>
            <th>Password</th>
            <th>Email</th>
            <th>County</th>
          </tr>
        </thead>
        <tbody>
          {tableRows}
        </tbody>
      </table>
      </div>
    </div>
  );
}

export default Table