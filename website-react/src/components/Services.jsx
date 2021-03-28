import React, { useState, useEffect } from "react";
import axios from "axios";


//  const [D, setData] = useState([]);
//   const FetchData = () => {
//     axios
//       .get(
//         `http://pchealth.azurewebsites.net/api/Base/GetDiagnosticData`
//       )
//       .then((res) => {
//         setData(res.data.filter((d) => d.PC_ID == match.params.id ));
//       })
//let F = [{ "CpuUsage": 19.008905, "TotalFreeDiskSpace": 793.6478, "DiskTotalSpace": 998.3052, "MemoryUsage": 53.84960778216368, "AvgNetworkBytesSent": 303719330, "AvgNetworkBytesReceived": 2247496234, "PC_ID": "GC4AfW43BT2T21VTQKgo4lGLYuuhLqg9srdwXwSTFWE", "OS": "Windows", "Services": [{ "Item1": "AdobeARMservice", "Item2": "Running" }] }]
// console.log(F[0].Services)
function Services({match}) {
  const [D, setData] = useState([]);
  const FetchData = () => {
    axios
      .get(
        "http://pchealth.azurewebsites.net/api/Base/GetDiagnosticData"
      )
      .then((res) => {
        console.log(res)
        setData(res.data.filter((d) => d.PC_ID == match.params.id));
        console.log(D[0])
      })
      .catch((err) => console.log(err));
  }
    useEffect(() => {
      FetchData()
    }, );
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
        <tr className = 'header'>
          <th scope="col">Service Name</th>
          <th scope = 'col'>Service Status</th>
        </tr>
      </thead>
      <tbody>
      {D[0].Services.map(s => (
            <tr>
              <td>{s.Item1}</td>
              <td>{s.Item2}</td>
            </tr>
        ))
        }
      </tbody>
    </table>
  );
}

export default Services
