import React, { useState, useEffect } from "react";
import axios from "axios";
import './style.css'

function Services({match}) {
  const [D, setData] = useState([]);
  const [search, setSearch] = useState('');
  const [FilteredData, setFilteredData] = useState([]);
  const [FilteredData2, setFilteredData2] = useState([]);

  const FetchData = () => {
    axios
      .get("https://pchealth.azurewebsites.net/Pc/DiagnosticData", {

        headers: { Authorization: "Bearer " + localStorage.getItem("token") },

      })
      .then((res) => {
        setData(res.data.filter((d) => d.PcId === match.params.id));
        // console.log(res)
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

  useEffect(() => {
    // console.log("D:",D)
    setFilteredData(
      // D.filter(D.map(x => (x.Services.map (x2 => (x2.Item1.toLowerCase().includes(search.toLowerCase()))))))
      []
      )
      // D.map(x => (x.Services.map (x2 => (console.log(x2)))))
      D.map(x => (x.Services.map (x2 => (x2.Item1.toLowerCase().includes(search.toLowerCase()) ? FilteredData.push(x2) : ''))))
      // setFilteredData(FilteredData)
      setFilteredData2(FilteredData)

  }, [search, D])

  // useEffect(() => {
  //   setFilteredData2(FilteredData)
  // }, [FilteredData])

  return (

    <>
    <div className="div_background">
      <input
        className="search"
        type="text"
        placeholder="search services..."
        onChange={(e) => {
          setSearch(e.target.value);
        }}
      />

    <table className="table">
      <thead className="thead-dark">
        <tr className = 'header'>
          <th scope="col">Service Name</th>
          <th scope = 'col'>Service Status</th>
        </tr>
      </thead>
      <tbody>
        {console.log(FilteredData2)}
      {/* {
      D.length === 0 ? (
          <div style={{ color: "red" }}> No results found </div>
        ): (

          D.map(d=>(
            d.Services.map(service => (            
            <>
            <tr>
              <td>{service.Item1}</td>
              <td>{service.Item2}</td>
            </tr>
            </>
            ))
          ))

          )
          } */}

      { (FilteredData2.length === 0 ) ? (
        <div style={{ color: "red" }}> No results found </div>
      ) : (
        FilteredData2.map(d =>(
          
          <>
          <tr>
            <td>{d.Item1}</td>
            <td>{d.Item2}</td>
          </tr>
          </>
      ))
      )
      }

      </tbody>
    </table>
    </div>
    </>
  );
}

export default Services
