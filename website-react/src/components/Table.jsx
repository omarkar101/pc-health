import React, { useState, useEffect,SyntheticEvent } from "react";
import { Link } from "react-router-dom";
import axios from "axios";
// import { i } from './App'


function Table(props) {
  const [search, setSearch] = useState('');
  const [datalst, setData] = useState([]);
  const [FilteredData, setFilteredData] = useState([]);
  console.log(localStorage.getItem("token"))
  const FetchData = async (e) => {
    // e.preventDefault();
    //Function to fetch the data from the Server
    const response = await fetch(
      "http://omarkar1011-001-site1.dtempurl.com/api/Get/GetDiagnosticData",
      {
        headers: {
          Accept: "application/json",
          authorization: "Bearer " + localStorage.getItem("token"),
        },
        // authorization: localStorage.getItem("token"),
      }
    ); 
  };
  // useEffect(() => {
  //   (
  //     async () => {
  //       const FetchData = await fetch(
  //         "http://pchealth.somee.com/api/Get/GetDiagnosticData",
  //         {
  //           // authorization: "Bearer Token",
  //           headers: { "Content-Type": "application/json" },
  //           // credentials: "include",
  //         }
  //       );
  //     }
  //   )()
  // })
  useEffect(() => {
    FetchData();
  }, []);

  useEffect(() => {
    const UpdateCycle = setInterval(() => {
      FetchData();
      // console.log(props.interval);
      console.log(props.i.interval)
    }, (props.i.interval * 1000));
    return () => {
      clearInterval(UpdateCycle);
    };
  });

  useEffect(() => {
    setFilteredData(
      datalst.filter((username) => username.PcId.toLowerCase().includes(search.toLowerCase()))
    )
  }, [search, datalst])
  return (
    <>
      <input type="text" placeholder="search username..." onChange={(e) => {
        setSearch(e.target.value)
      }} />
      <table className="table">
        <thead className="thead-dark">
          <tr className="header">
            <th>Username</th>
            <th>Operating System</th>
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
          {FilteredData.length === 0 ? <div style={{ color: "red" }}> No results found </div> :
            FilteredData.map((x) => (
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
                <td>{x.AvgNetworkBytesReceived}</td>
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
    </>
  );
}
// FilteredData.length === 0 ? <div style={{ color: "red" }}> No results found </div> :

export default Table;
