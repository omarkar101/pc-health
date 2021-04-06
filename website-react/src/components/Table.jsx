import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";
import './style.css'
import Chart from "react-google-charts";



function Table(props) {
    const [search, setSearch] = useState('');
    const [datalst, setData] = useState([]);
    const [FilteredData, setFilteredData] = useState([]);

    const [detailsShown, setDetailShown] = useState([]);

    const FetchData = async () => {
        //Function to fetch the data from the Server
        axios.get("http://pchealth.somee.com/api/Base/GetDiagnosticData")
            .then((res) => {
                // console.log(res);
                setData(res.data);
            })
            .catch((err) => console.log(err));
    };

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
            <input class="search" type="text" placeholder="search username..." onChange={(e) => {
                setSearch(e.target.value)
            }} />

            <div class="table-container">
                <table>
                    <thead>
                        <tr>
                            <th>Username</th>
                            <th>Status</th>
                            <th>Operating System</th>
                            <th>Services</th>
                            <th>Contact info</th>
                        </tr>
                    </thead>
                    <tbody>
                        {FilteredData.length === 0 ? <div style={{ color: "red" }}> No results found </div> :
                            FilteredData.map((x) => (
                                <>
                                    <tr key={"NAME:" + x.PcId} onClick={() => toggleShown(x.PcId)}>
                                        <td>
                                            {/* <Link to={"/" + x.PcId} target="_blank"> */}
                                            {x.PcId}
                                            {/* </Link> */}
                                        </td>
                                        <td>Healthy</td>
                                        <td>{x.Os}</td>
                                        {/* <td>{x.CpuUsage.toFixed(2)}</td> */}
                                        {/* <td>{x.DiskTotalSpace.toFixed(2)}</td> */}
                                        {/* <td>{x.TotalFreeDiskSpace.toFixed(2)}</td> */}
                                        {/* <td>{x.MemoryUsage.toFixed(2)}</td> */}
                                        {/* <td>{x.AvgNetworkBytesSent}</td> */}
                                        {/* <td>{x.AvgNetworkBytesReceived}</td> */}
                                        {/* <td>{x.FirewallStatus}</td> */}
                                        <td>
                                            <Link to={"/" + x.PcId} target="_blank">
                                                Details
                                            </Link>
                                        </td>
                                        <td>{x.PcId.slice(0, 4) + "@gmail.com"}</td>
                                    </tr>

                                    {detailsShown.includes(x.PcId) && (
                                        <tr key={"DETAIL:" + x.PcId} className="additional-info" >
                                            <td align="center" colspan="5" >
                                                <div class="wrapper" >
                                                    <div class="first">
                                                        <Chart
                                                            width={'350px'}
                                                            height={'150px'}
                                                            chartType="PieChart"
                                                            loader={<div>Loading Chart</div>}
                                                            data={[
                                                                ['memory', 'usage'],
                                                                ['Used', x.MemoryUsage],
                                                                ['Remaining', 100 - x.MemoryUsage],
                                                            ]}
                                                            options={{
                                                                title: 'Memory',
                                                                slices: {
                                                                    0: { color: 'steelblue' },
                                                                    1: { color: 'black' },
                                                                },

                                                            }}
                                                        // rootProps={{ 'data-testid': '2' }}
                                                        /></div>
                                                    <div class="second">
                                                        <Chart

                                                            width={'350px'}
                                                            height={'150px'}
                                                            chartType="PieChart"
                                                            loader={<div>Loading Chart</div>}
                                                            data={[
                                                                ['cpu', 'usage'],
                                                                ['Used', x.CpuUsage],
                                                                ['Remaining', 100 - x.CpuUsage],
                                                            ]}
                                                            options={{
                                                                title: 'CPU',
                                                                slices: {
                                                                    0: { color: 'lightseagreen' },
                                                                    1: { color: 'black' },
                                                                },

                                                            }}
                                                        // rootProps={{ 'data-testid': '1' }}
                                                        /></div>
                                                    <div class="third">
                                                        <Chart

                                                            width={'350px'}
                                                            height={'150px'}
                                                            chartType="PieChart"
                                                            loader={<div>Loading Chart</div>}
                                                            data={[
                                                                ['disk', 'usage'],
                                                                ['Used', x.DiskTotalSpace - x.TotalFreeDiskSpace],
                                                                ['Remaining', x.TotalFreeDiskSpace],
                                                            ]}
                                                            options={{
                                                                title: 'Disk',
                                                                slices: {
                                                                    0: { color: 'indianred' },
                                                                    1: { color: 'black' },
                                                                },

                                                            }}
                                                        // rootProps={{ 'data-testid': '1' }}
                                                        /></div>
                                                    <div class="fourth">
                                                        <p>
                                                            <span>
                                                                Average Network Bytes Sent: {x.AvgNetworkBytesSent} bytes
                                                            </span><span>
                                                                Average Network Bytes Received: {x.AvgNetworkBytesReceived} bytes
                                                            </span>
                                                            <br></br>
                                                            <span>
                                                                Firewall Status: {x.FirewallStatus}
                                                            </span>
                                                        </p>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    )}
                                </>
                            ))}
                    </tbody>
                </table>
            </div>
        </>
    );
}

export default Table;
