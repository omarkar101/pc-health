import React, {useState, useEffect} from "react";
import { Link, withRouter} from "react-router-dom";
import axios from "axios";
import './style.css'
import Chart from "react-google-charts";
import {CgDanger} from 'react-icons/cg';
import {IoMdCheckmarkCircleOutline} from 'react-icons/io';
import {AiFillWindows} from 'react-icons/ai';
import {FcLinux} from 'react-icons/fc';
import Nav from './Nav'

function Table() {
    const [search, setSearch] = useState('');
    const [datalst, setData] = useState([]);
    const [FilteredData, setFilteredData] = useState([]);
    const [detailsShown, setDetailShown] = useState([]);
  const [temp, setTemp] = useState([]);
  const FetchData = async () => {
  axios
    .get("http://pc-health.somee.com/Pc/DiagnosticData", {
      headers: { Authorization: "Bearer " + localStorage.getItem("token") },
    })
    .then((res) => {
      setTemp(datalst)
      setData(res.data);
    });
  }
  useEffect(() => {
    setTemp([])
  },[])
    const toggleShown = username => {
        const shownState = detailsShown.slice();
        const index = shownState.indexOf(username);
        if (index >= 0) {
            shownState.splice(index, 1); 
            setDetailShown(shownState);
        } else {
            shownState.push(username);
            setDetailShown(shownState);
        }
    };

    useEffect(() => {
        FetchData();
    }, []);

    
    useEffect(() => {
        const UpdateCycle = setInterval(() => {
            FetchData();
        }, (localStorage.getItem('interval') * 1000));
        return () => {
          // console.log(datalst);
            clearInterval(UpdateCycle);
        };
    });

    useEffect(() => {
      // console.log("datalst", datalst);
        setFilteredData(
            datalst.filter((username) => username.PcConfiguration.PcUsername.toLowerCase().includes(search.toLowerCase()))
        )
    }, [search, datalst])
  let counter = 0
  // console.log(counter)
  return (
    <>
      <div className="table_div">
        <Nav />
        <input
          className="search"
          type="text"
          placeholder="search username..."
          onChange={(e) => {
            setSearch(e.target.value);
          }}
        />

        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>&nbsp;</th>
                <th>Username</th>
                {/* <th>Activity</th> */}
                {/* <th>Status</th> */}
                <th>Status</th>
                <th>Operating System</th>
                <th>More Info </th>
                <th>Contact info</th>
                {/* <th>Statistics</th> */}
              </tr>
            </thead>
            <tbody>
              {FilteredData.length === 0 ? (
                <div style={{ color: "red" }}> No results found </div>
              ) : (
                FilteredData.map((x) => (
                  <>
                    {console.log(temp[counter]===x)}
                    {/* {console.log("datalst", x)} */}
                    <tr
                      key={"NAME:" + x.PcId}
                      onClick={() => toggleShown(x.PcId)}
                    >
                      {x.HealthStatus === "Healthy" ? (
                        // <td style={{color: "green"}}> {x.HealthStatus} </td>
                        <td>
                          {" "}
                          <IoMdCheckmarkCircleOutline
                            color="green"
                            size="1.5rem"
                          />
                        </td>
                      ) : (
                        // <td style={{color: "red"}}> In Danger </td>
                        <td>
                          {" "}
                          <CgDanger color="red" size="1.5rem" />
                        </td>
                      )}
                      <td style={{ fontWeight: "bold" }}>
                        {x.PcConfiguration.PcUsername}
                        {/* </Link> */}
                      </td>
                      {temp[counter] === x ? (
                        <td>Inactive</td>
                      ) : (
                        <td>Active</td>
                      )}
                      {x.Os === "Windows" ? (
                        <td>
                          <AiFillWindows size="1.2rem" /> &nbsp; {x.Os}
                        </td>
                      ) : x.Os === "linux" ? (
                        <td>
                          <FcLinux size="1.2rem" />
                          {x.Os}
                        </td>
                      ) : (
                        <td>{x.Os}</td>
                      )}

                      <td>
                        <Link
                          to={"/table/" + x.PcId}
                          target="_blank"
                          className="tablelinks"
                        >
                          Services
                        </Link>
                        &nbsp; &nbsp; &nbsp;
                        <Link
                          to={"/Stats/" + x.PcId}
                          target="_blank"
                          className="tablelinks"
                        >
                          Performance
                        </Link>
                      </td>
                      <td>{x.PcConfiguration.PcEmail}</td>
                    </tr>
                    {detailsShown.includes(x.PcId) && (
                      <tr key={"DETAIL:" + x.PcId} className="additional-info">
                        <td align="center" colSpan="6">
                          <div className="wrapper">
                            <div className="first">
                              <Chart
                                width={"350px"}
                                height={"150px"}
                                chartType="PieChart"
                                loader={<div>Loading Chart</div>}
                                data={[
                                  ["memory", "usage"],
                                  ["Used", x.MemoryUsage],
                                  ["Remaining", 100 - x.MemoryUsage],
                                ]}
                                options={{
                                  title: "Memory",
                                  slices: {
                                    0: { color: "steelblue" },
                                    1: { color: "black" },
                                  },
                                }}
                                // rootProps={{ 'data-testid': '2' }}
                              />
                            </div>
                            <div className="second">
                              <Chart
                                width={"350px"}
                                height={"150px"}
                                chartType="PieChart"
                                loader={<div>Loading Chart</div>}
                                data={[
                                  ["cpu", "usage"],
                                  ["Used", x.CpuUsage],
                                  ["Remaining", 100 - x.CpuUsage],
                                ]}
                                options={{
                                  title: "CPU",
                                  slices: {
                                    0: { color: "lightseagreen" },
                                    1: { color: "black" },
                                  },
                                }}
                                // rootProps={{ 'data-testid': '1' }}
                              />
                            </div>
                            <div className="third">
                              <Chart
                                width={"350px"}
                                height={"150px"}
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
                                  title: "Disk",
                                  slices: {
                                    0: { color: "indianred" },
                                    1: { color: "black" },
                                  },
                                }}
                                // rootProps={{ 'data-testid': '1' }}
                              />
                            </div>
                            <div className="fourth">
                              <p>
                                <span>
                                  Average Network Bytes Sent:{" "}
                                  {x.AvgNetworkBytesSent} bytes
                                </span>
                                <span>
                                  Average Network Bytes Received:{" "}
                                  {x.AvgNetworkBytesReceived} bytes
                                </span>
                                <br></br>
                                {x.FirewallStatus === "Active" ? (
                                  <div style={{ overflow: "hidden" }}>
                                    <p style={{ float: "left" }}>
                                      Firewall Status: &nbsp;
                                    </p>
                                    <p style={{ color: "lime" }}>
                                      {x.FirewallStatus}
                                    </p>{" "}
                                  </div>
                                ) : (
                                  <div style={{ overflow: "hidden" }}>
                                    <p style={{ float: "left" }}>
                                      Firewall Status: &nbsp;
                                    </p>
                                    <p style={{ color: "red" }}>
                                      {x.FirewallStatus}
                                    </p>{" "}
                                  </div>
                                )}
                              </p>
                            </div>
                          </div>
                        </td>
                      </tr>
                    )}
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
