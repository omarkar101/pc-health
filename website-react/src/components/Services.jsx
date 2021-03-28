import React, { useState, useEffect } from "react";
import axios from "axios";


function Services({match}) {
  const [D, setData] = useState([]);
  const FetchData = () => {
    axios
      .get(
        "https://pchealth.azurewebsites.net/api/Base/GetDiagnosticData"
      )
      .then((res) => {
        console.log(res)
        setData(res.data.filter((d) => d.PcId === match.params.id));
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
        {D.map(d => (
          d.Services.map(service => (
            <tr>
              <td>{service.Item1}</td>
              <td>{service.Item2}</td>
            </tr>
          ))
        ))}
      </tbody>
    </table>
  );
}

export default Services
