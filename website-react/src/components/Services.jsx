import React, { useState, useEffect } from "react";
import axios from "axios";
import "./services.css"


function Services({match}) {
  const [D, setData] = useState([]);
  const [search, setSearch] = useState('');
  const [FilteredData, setFilteredData] = useState([]);

  const FetchData = () => {
    axios
      .get("http://pc-health.somee.com/Pc/DiagnosticData", {
      headers: { Authorization: "Bearer " + localStorage.getItem("token") },
      })
      .then((res) => {
        // console.log((res.data)[0].Services);
        // console.log(res.data)
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

  useEffect(() => {
    // const temp = D.map(x => x.Services)
    // const temp = D.map(x => console.log(x))
    // console.log(D);
    // const temp2 = temp.filter(x => console.log(x))
    // console.log(temp);
    // FilteredData = []
    // temp.map(x => x.map (x2 => x2.Item1.toLowerCase().includes(search.toLowerCase())? FilteredData.push(x2) : ''));

    setFilteredData(
      []
      // service.Item1.toLowerCase().includes(search.toLowerCase())
      // D.map(x => (x.Services.filter(service => (service.Item1.toLowerCase().includes(search.toLowerCase()))) ))
      // D.filter(x =>(x.Services.map(service => ( console.log( "service", service.Item1.toLowerCase(), "; search", search.toLowerCase(), "; result", service.Item1.toLowerCase().includes(search.toLowerCase()) ) ))))
      // D.map(x => (x.Services.map(service => (service.Item1))))
      // D.filter(x =>(x.Services.map(service => ( service.Item1.toLowerCase().includes(search.toLowerCase()) ))))
      // temp.filter(x => ( x.Item1.toLowerCase().includes( search.toLowerCase() ) ))
  
      // D.map(x => (x.Services.map(service => (service.Item1))))
      // temp.filter(x =>  x.Item1.toLowerCase().includes(search.toLowerCase()) )
    
      // temp.filter(x => x.map( x2 => ( x2.Item1.toLowerCase().includes(search.toLowerCase()) )) )
      // temp.filter(x => ( x.Item1.toLowerCase().includes( search.toLowerCase() ) ))
      )
      // setFilteredData(
      D.map(x => x.Services.map (x2 => x2.Item1.toLowerCase().includes(search.toLowerCase()) ? FilteredData.push(x2) : ''))
      // )
    // console.log(FilteredData);
    // FilteredData.map(d=>( console.log(d)));
  }, [search, D])

  return (

    <>
    <div className="div_background">
      <input
        class="search"
        type="text"
        placeholder="search username..."
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
        
      {
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
          }
      {/* {console.log(Object.keys(FilteredData).length)} */}
      {/* {alert(FilteredData)} */}
      {/* {
        FilteredData.map(d => {
          
          <>
          {console.log("HERE")}
          <tr>
            <td>{d.Item1}</td>
            <td>{d.Item2}</td>
          </tr>
          </>
        })
      } */}

      </tbody>
    </table>
    </div>
    </>
  );
}

export default Services
