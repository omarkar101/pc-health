import React, { useState, useEffect } from "react";
import axios from "axios";



function Services({match}) {
  const [x, setData] = useState({});
  const FetchData = () => {
    axios
      .get(
        `http://pchealth.azurewebsites.net/api/Base/GetDiagnosticData/${match.params.id}`
      )
      .then((res) => {
        setData(res.data);
      })
      .catch((err) => console.log(err));
  }
    useEffect(() => {
      FetchData()
  },);
  return (
    <table className="table">
      <thead className="thead-dark">
        <tr className = 'header'>
          <th scope="col">Service Name</th>
          <th scope = 'col'>Service Status</th>
        </tr>
      </thead>
      <tbody>
          <tr key={x.PC_ID}>
            <td>{x.OS}</td>
            <td>{x.CpuUsage}</td>
          </tr>
      </tbody>
    </table>
  );
}

export default Services
