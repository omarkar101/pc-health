import React, { useState, useEffect } from "react";
import axios from "axios";
import './style.css'

function Services({ match }) {
  const [D, setData] = useState([]);
  const [search, setSearch] = useState('');
  const [FilteredData, setFilteredData] = useState([]);
  const [FilteredData2, setFilteredData2] = useState([]);

  const FetchData = () => {
    axios
      .get("https://pc-health.azurewebsites.net/Pc/DiagnosticData", {

        headers: { Authorization: "Bearer " + localStorage.getItem("token") },

      })
      .then((res) => {
        setData(res.data.filter((d) => d.PcId === match.params.id));
      })
      .catch((err) => console.log(err));
  }
  useEffect(() => {
    FetchData()
  });
  useEffect(() => {
    const UpdateCycle = setInterval(() => {
      FetchData();
    }, 3000);
    return () => {
      clearInterval(UpdateCycle);
    };
  });

  useEffect(() => {
    setFilteredData(
      []
    )
    D.map(x => (x.Services.map(x2 => (x2.Item1.toLowerCase().includes(search.toLowerCase()) ? FilteredData.push(x2) : ''))))

    setFilteredData2(FilteredData)

  }, [search, D])

  return (

    <>
      <div className="table_div">
        <nav className="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
          <img className="logologintable" src="/images/logo3.png" alt="" />
          <input
            className="search_services"
            type="text"
            placeholder="search services..."
            onChange={(e) => {
              setSearch(e.target.value);
            }}
          />
        </nav>

        <div className="table-box table-container">

          <p className="services_header" style={{ fontSize: "25px" }}>View Current Services: {match.params.name}</p>

          <table className="center bottomBorder roundedCorners">
            <thead>
              <tr className="header_tr">
                <th scope="col" style={{ width: "60%" }}>Service Name</th>
                <th scope="col">Service Status</th>
              </tr>
            </thead>
            <tbody>

              {(FilteredData2.length === 0) ? (
                <div style={{ color: "red" }}> No results found </div>
              ) : (
                FilteredData2.map(d => (

                  <>
                    <tr>
                      <td>{d.Item1}</td>
                      {d.Item2 === "Stopped" ?
                        <td style={{ color: "indianred" }}>{d.Item2}</td> :
                        <td style={{ color: "green" }}>{d.Item2}</td>
                      }
                    </tr>
                  </>
                ))
              )
              }

            </tbody>
          </table>
        </div>
      </div>
    </>
  );
}

export default Services
