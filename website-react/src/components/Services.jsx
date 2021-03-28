import React, { useState, useEffect } from "react";
import axios from "axios";



function Services({match}) {
  const [x, setData] = useState({});
    useEffect(() => {
        axios
            .get(`http://pchealth.azurewebsites.net/api/Base/GetDiagnosticData/${match.params.id}`)
      .then((res) => {
        console.log(res);
        setData(res.data);
      })
          .catch((err) => console.log(err));
      console.log(match.params.id)
  },[]);
  return (
    <table className="table">
      <thead className="thead-dark">
        <tr>
          <th scope="col">Username</th>
          <th scope = 'col'>Operating System</th>
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
