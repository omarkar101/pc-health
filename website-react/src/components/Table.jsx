import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

function Table() {
  const [datalst, setData] = useState([]);

  const FetchData = async () => {
    //Function to fetch the data from the Server
    axios
      .get("https://pchealth.azurewebsites.net/api/Base/GetDiagnosticData")
      .then((res) => {
        console.log(res);
        setData(res.data);
      })
      .catch((err) => console.log(err));
  };

  useEffect(() => {
    FetchData();
  }, []);

  useEffect(() => {
    const UpdateCycle = setInterval(() => {
      FetchData();
    }, 3000); 
    return () => {
      clearInterval(UpdateCycle);
    };
  });
  return (
    <table className="table">
      <thead className="thead-dark">
        <tr className="header">
          <th scope="col">Username</th>
          <th scope="col">Operating System</th>
          <th scope="col">CPU Usage</th>
          <th scope="col">Total Disk Space</th>
          <th scope="col">Free Disk Space</th>
          <th>Memory Usage</th>
          <th>AVG. Network Bytes Sent</th>
          <th>AVG. Network Bytes Received</th>
          <th>Firewall Status</th>
          <th scope="col">Services</th>
        </tr>
      </thead>
      <tbody>
        {datalst.map((x) => (
          <tr key={x.PcId}>
            <td>
              <Link to={"/" + x.PcId} target="_blank">
                {x.PcId}
              </Link>
            </td>
            <td>{x.Os}</td>
            <td>{x.CpuUsage.toFixed(2)}</td>
            <td>{x.DiskTotalSpace.toFixed(2)}</td>
            <td>{x.TotalFreeDiskSpace.toFixed(2)}</td>
            <td>{x.MemoryUsage.toFixed(2)}</td>
            <td>{x.AvgNetworkBytesSent}</td>
            <td>{x.AvgNetworkBytesSent}</td>
            <td>{x.FirewallStatus}</td>
            <td>
              <Link to={"/" + x.PcId} target="_blank">
                Details
              </Link>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default Table;
