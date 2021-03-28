import React, { useState, useEffect } from 'react'
import {Link} from 'react-router-dom'
import axios from 'axios'



function Table() {

const [datalst, setData] = useState([]);
useEffect(() => {
  axios
    .get("http://pchealth.azurewebsites.net/api/Base/GetDiagnosticData")
    .then((res) => {
      console.log(res);
      setData(res.data);
    })
    .catch((err) => console.log(err));
});
return (
  <table className="table">
    <thead className="thead-dark">
      <tr>
        <th scope="col">Username</th>
        <th scope="col">Operating System</th>
        <th scope="col">CPU Usage</th>
        <th scope="col">Total Disk Space</th>
        <th scope="col">Services</th>
      </tr>
    </thead>
    <tbody>
      {datalst.map((x) => (
        <tr key={x.PC_ID}>
        <td><Link to={'/'+x.PC_ID} target="_blank">
            {x.PC_ID}
          </Link></td>
          <td>{x.OS}</td>
          <td>{x.CpuUsage}</td>
          <td>{x.DiskTotalSpace}</td>
        </tr>
      ))}
    </tbody>
  </table>
);
}

export default Table



