import React, { useState, useEffect } from "react";
import { Link, withRouter } from "react-router-dom";
import axios from "axios";
import './style.css'
import Chart from "react-google-charts";
import { CgDanger } from 'react-icons/cg';
import { IoMdCheckmarkCircleOutline } from 'react-icons/io';
import { AiFillWindows } from 'react-icons/ai';
import { FcLinux } from 'react-icons/fc';
import Nav from './Nav'
import { AiOutlineWarning } from 'react-icons/ai'
import { GrStatusUnknown } from 'react-icons/gr'
import { BsQuestionCircle } from "react-icons/bs";
import "react-loader-spinner/dist/loader/css/react-spinner-loader.css";
import Loader from "react-loader-spinner";
// import { ReactComponent as Logo } from './images/logo.jpeg';

function Table() {
  const [search, setSearch] = useState('');
  const [datalst, setData] = useState([]);
  const [FilteredData, setFilteredData] = useState([]);
  const [detailsShown, setDetailShown] = useState([]);
  const [temp, setTemp] = useState([]);
  const [temp2, setTemp2] = useState([]);
  const [inactives, setInactives] = useState([]);
  const [active, setActive] = useState([]);
  // const [counter, setCounter] = useState(0)
  const FetchData = async () => {
    axios
      .get("https://pc-health.azurewebsites.net/Pc/DiagnosticData", {
        headers: { Authorization: "Bearer " + localStorage.getItem("token") },
      })
      .then((res) => {
        // setTemp(datalst)
        setData(res.data);
      });
  }
  const FetchData2 = async () => {
    axios
      .get("https://pc-health.azurewebsites.net/Pc/DiagnosticData", {
        headers: { Authorization: "Bearer " + localStorage.getItem("token") },
      })
      .then((res) => {
        setTemp(res.data);
        // setData(res.data);
      });
  }
  useEffect(() => {
    setTemp([])
  }, [])

  const toggleShown = (username, a) => {
    // console.log(detailsShown)
    const shownState = detailsShown.slice();
    const index = shownState.indexOf(username);

    const InactiveShownState = inactives.slice();
    const iindex = InactiveShownState.indexOf(username);

    if (a === "Inactive") {
      if (iindex >= 0) {
        // close
        InactiveShownState.splice(index, 1);
        setInactives(InactiveShownState);
      } else {
        // open extra info 
        InactiveShownState.push(username);
        setInactives(InactiveShownState);
      }
    }
    else {
      if (index >= 0) {
        // close
        shownState.splice(index, 1);
        setDetailShown(shownState);
      } else {
        // open extra info
        shownState.push(username);
        setDetailShown(shownState);
      }
    }
  };


  useEffect(() => {
    FetchData();
  }, []);

  useEffect(() => {
    setTimeout(() => {
      FetchData2();
    }, 1000)
  }, []);


  useEffect(() => {
    const UpdateCycle = setInterval(() => {
      FetchData();
      setTimeout(function () { FetchData2(); }, 1000);

    }, (localStorage.getItem('interval') * 1000));
    return () => {
      // console.log(datalst);
      clearInterval(UpdateCycle);
    };
  });


  useEffect(() => {
    setFilteredData(
      datalst.filter((username) => username.PcConfiguration.PcUsername.toLowerCase().includes(search.toLowerCase()))
    )
    setTemp2(
      temp.filter((username) => username.PcConfiguration.PcUsername.toLowerCase().includes(search.toLowerCase()))
    )
  }, [search, datalst])


  useEffect(() => {
    FilteredData.map((x, index) => (
      (temp2.length !== 0) ?
        (((x.CpuUsage === temp2[index].CpuUsage) && (x.AvgNetworkBytesReceived === temp2[index].AvgNetworkBytesReceived) && (x.AvgNetworkBytesSent === temp2[index].AvgNetworkBytesSent) && (x.MemoryUsage === temp2[index].MemoryUsage)) ? (active[index] = "Inactive") : (active[index] = "Active")) :
        ('')
    )
    )
  }, [temp2])



  return (
    <>
      <div className="table_div">
        <div className="search_settings">
          <img className="logo" src="/images/logo2.jpg" alt="" />
          {/* <Nav /> */}
          <input
            className="search"
            type="text"
            placeholder=" Search username..."
            onChange={(e) => {
              setSearch(e.target.value);
            }}
          />
          <Nav />
        </div>

        <br />

        <div className="table-box table-container">
          <table className="center bottomBorder roundedCorners">
            <thead>
              <tr className="header_tr">
                <th>&nbsp;</th>
                <th>Username</th>
                <th>Activity Status</th>
                <th>O.S</th>
                <th>More Info </th>
                <th>Contact info</th>
              </tr>
            </thead>
            <tbody>
              {FilteredData.length === 0 ? (
                <div style={{ color: "red" }}> No results found </div>
              ) : (
                FilteredData.map((x, c) => (
                  <>
                    {/* {console.log(temp[counter] === x)} */}
                    {/* {console.log("datalst", x)} */}
                    <tr className="not_collapsed"
                      key={"NAME:" + x.PcId}
                      onClick={() => toggleShown(x.PcId, active[c])}
                    >
                      {
                        (active[c] === "Inactive" || active.length === 0) ?
                          (
                            <td>
                              <BsQuestionCircle
                                color="gold"
                                size="1.5rem"
                              />
                            </td>) :
                          (
                            x.HealthStatus === "Healthy" ?
                              (
                                <td>
                                  <IoMdCheckmarkCircleOutline
                                    color="green"
                                    size="1.5rem"
                                  />
                                </td>) :
                              <td>
                                (<AiOutlineWarning color="red" size="1.5rem" />
                              </td>
                          )
                      }

                      {/* // {(active[c] === "Inactive" || active.length === 0) ?
                      //   <td>
                      //     {" "}
                      //     <BsQuestionCircle
                      //       color="gold"
                      //       size="1.5rem"
                      //     />
                      //   </td>
                      // :
                      // x.HealthStatus === "Healthy" ? (
                      //   // <td style={{color: "green"}}> {x.HealthStatus} </td>
                      //   <td>
                      //     {" "}
                      //     <IoMdCheckmarkCircleOutline
                      //       color="green"
                      //       size="1.5rem"
                      //     />
                      //   </td>
                      // ) : (
                      //   // <td style={{color: "red"}}> In Danger </td>
                      //   <td>
                      //     {" "}
                      //     <AiOutlineWarning color="red" size="1.5rem" />
                      //   </td>
                      // )} */}
                      <td style={{ fontWeight: "bold" }}>
                        {x.PcConfiguration.PcUsername}
                        {/* </Link> */}
                      </td>
                      {/* {temp[counter] === x ? (
                        <td>Inactive</td>
                      ) : (
                        <td>Active</td>
                      )} */}
                      <td>{(active.length === 0) ?
                        <Loader
                          type="ThreeDots"
                          color="grey"
                          height={20}
                          width={20}
                          timeout={6000}
                        />
                        : active[c]}</td>
                      {x.Os === "Windows" ? (
                        <td>
                          <AiFillWindows size="1.2rem" /> {x.Os}
                        </td>
                      ) : x.Os === "Linux" ? (
                        <td>
                          <FcLinux size="1.2rem" />
                          {x.Os}
                        </td>
                      ) : (
                        <td>{x.Os}</td>
                      )}


                      {(active.length === 0) ?

                        <td>
                          <Loader
                            type="ThreeDots"
                            color="grey"
                            height={20}
                            width={20}
                            timeout={6000}
                          />
                        </td>
                        :
                        (active[c] === "Inactive") ? <td>Unavailable</td>
                          :

                          <td>

                            <Link
                              to={"/table/" + x.PcId}
                              target="_blank"
                              className="tablelinks"
                            >
                              Services
                        </Link>
                        &nbsp; &nbsp;
                        <Link
                              to={"/Stats/" + x.PcId}
                              target="_blank"
                              className="tablelinks"
                            >
                              Performance
                        </Link>
                          </td>

                      }
                      <td>{x.PcConfiguration.PcEmail}</td>
                    </tr>
                    {/* {console.log(detailsShown.includes(x.PcId) )} */}
                    {/* {(active.length === 0) && (<tr className="additional-info">
                      <td align="center" colSpan="6">
                        Loading...
                          </td>
                    </tr>)} */}
                    {(inactives.includes(x.PcId)) ? (
                      <tr className="additional-info">
                        <td align="center" colSpan="6">
                          This PC is currently inactive. Cannot view additional information.
                          </td>
                      </tr>
                    ) :
                      (detailsShown.includes(x.PcId) && active.length !== 0) ? (
                        <tr key={"DETAIL:" + x.PcId} className="additional-info">
                          <td align="center" colSpan="6">
                            {/* <hr className="collapse_line"/> */}
                            {/* {console.log("HERE")} */}
                            {console.log(detailsShown.includes(x.PcId))}
                            <div className="wrapper">
                              <div className="piechart">
                                <p className="chart_p">Memory</p>
                                <Chart
                                  className="charts"
                                  chartType="PieChart"
                                  loader={<div>Loading Chart</div>}
                                  data={[
                                    ["memory", "usage"],
                                    ["Used", x.MemoryUsage],
                                    ["Remaining", 100 - x.MemoryUsage],
                                  ]}
                                  options={{
                                    // title: "Memory",
                                    slices: {
                                      0: { color: "steelblue" },
                                      1: { color: "black" },
                                    },
                                    width: '100%',
                                    height: 150,
                                    // backgroundColor: 'salmon',
                                    chartArea: { width: '100%', height: '70%' },
                                    legend: { position: 'bottom' },
                                    titlePosition: 'none',
                                  }}
                                />
                              </div>
                              <div className="piechart">
                                <p className="chart_p">CPU</p>
                                <Chart
                                  className="charts"
                                  chartType="PieChart"
                                  loader={<div>Loading Chart</div>}
                                  data={[
                                    ["cpu", "usage"],
                                    ["Used", x.CpuUsage],
                                    ["Remaining", 100 - x.CpuUsage],
                                  ]}
                                  options={{
                                    // title: "CPU",
                                    slices: {
                                      0: { color: "lightseagreen" },
                                      1: { color: "black" },
                                    },
                                    width: '100%',
                                    height: 150,
                                    chartArea: { width: '100%', height: '70%' },
                                    legend: { position: 'bottom' },
                                    titlePosition: 'none',
                                  }}
                                />
                              </div>
                              <div className="piechart">
                                <p className="chart_p">Disk</p>
                                <Chart
                                  className="charts"
                                  chartType="PieChart"
                                  loader={<div>Loading Chart</div>}
                                  data={[
                                    ["disk", "usage"],
                                    [
                                      "Used",
                                      x.DiskTotalSpace - x.TotalFreeDiskSpace,
                                    ],
                                    ["Remaining", x.TotalFreeDiskSpace],
                                  ]}
                                  options={{
                                    // title: "Disk",
                                    slices: {
                                      0: { color: "indianred" },
                                      1: { color: "black" },
                                    },
                                    width: '100%',
                                    height: 150,
                                    chartArea: { width: '100%', height: '70%' },
                                    legend: { position: 'bottom' },
                                    titlePosition: 'none',
                                  }}
                                />
                              </div>

                              <div className="fourth">
                                <p>
                                  <span style={{ fontWeight: "bold", textDecoration: "underline" }}>
                                    More Info:
                                </span>
                                  <br />
                                  <span style={{ color: "black" }}>
                                    Average Network Bytes
                                  </span>
                                  <span style={{ color: "midnightblue" }}>
                                    Received:  &nbsp;{x.AvgNetworkBytesReceived.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')}
                                  </span>
                                  <span style={{ color: "midnightblue" }}>
                                    Sent:  &nbsp;&nbsp;&nbsp;{x.AvgNetworkBytesSent.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')}
                                  </span>
                                  <br />
                                  <span style={{ color: "black" }}>Firewall Status</span>
                                  {x.FirewallStatus === "Active" ? (
                                    <span style={{ color: "lime" }}>Active</span>
                                  ) : (
                                    <span style={{ color: "lime" }}>{x.FirewallStatus}</span>
                                  )}
                                </p>
                              </div>
                            </div>
                          </td>
                        </tr>
                      ) : ''}
                  </>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
}
export default withRouter(Table);