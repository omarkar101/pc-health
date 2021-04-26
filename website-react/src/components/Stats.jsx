import React, { useState, useEffect } from 'react'
import Chart from "react-google-charts";
import "./style.css";
import "./StatsStyle.css";

function Stats({ match }) {
  const [stats, setStats] = useState([])

  const FetchStats = async () => {
    const response = await fetch(
      `https://pc-health.azurewebsites.net/Pc/DiagnosticDataSpecific?pcId=${match.params.id}`, {

      headers: { Authorization: "Bearer " + localStorage.getItem("token") },
    }
    );
    const ans = await response.json();
    setStats(ans)
  }

  const [info, setInfo] = useState({
    cpu: [['secs', '%'],],
    memory: [['secs', '%'],],
    avBytesSent: [['secs', 'bytes'],],
    avBytesReceived: [['secs', 'bytes'],]
  });


  useEffect(() => {
    FetchStats()
  });

  useEffect(() => {
    const interval = setInterval(() => {
      FetchStats()
    }, 1000);
    return () => clearInterval(interval);
  }, []);


  function init() {
    info.cpu = [['secs', '%'],]
    info.memory = [['secs', '%'],]
    info.avBytesSent = [['secs', 'bytes'],]
    info.avBytesReceived = [['secs', 'bytes'],]
  }

  function setData() {
    init()
    stats.map((x) => (
      info.cpu.push([x.Second, x.PcCpuUsage]),
      info.memory.push([x.Second, x.PcMemoryUsage]),
      info.avBytesSent.push([x.Second, x.PcNetworkAverageBytesSend]),
      info.avBytesReceived.push([x.Second, x.PcNetworkAverageBytesReceived])
    ))
  }

  return (
    <>

      {setData()}
      <div className="table_div div2">
        <nav className="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
          <img className="logologintable" src="/images/logo3.png" alt="" />

        </nav>
        <br />
        <br />
        <div className="stats_wrapper">
          <p style={{ fontSize: "25px" }}>View Addition Information: {match.params.name}</p>
          <br />
          <div className="linecharts">
            <Chart
              className="linechart"
              chartType="AreaChart"
              loader={<div>Loading Chart</div>}
              data={info.cpu}
              options={{
                title: 'CPU Performance',
                hAxis: { titleTextStyle: { color: '#333' }, textPosition: 'none' },
                vAxis: { titleminValue: 0 },
                chartArea: { width: '50%', height: '70%' },
                colors: ['cornflowerblue'],
                backgroundColor: 'transparent',
              }}
            />
            <Chart
              className="linechart"
              chartType="AreaChart"
              loader={<div>Loading Chart</div>}
              data={info.memory}
              options={{
                title: 'Memory Usage',
                hAxis: { titleTextStyle: { color: '#333' }, textPosition: 'none' },
                vAxis: { minValue: 0 },
                chartArea: { width: '50%', height: '70%' },
                backgroundColor: 'transparent',
                colors: ['lightpink'],
              }}
            />
            <Chart
              className="linechart"
              chartType="AreaChart"
              loader={<div>Loading Chart</div>}
              data={info.avBytesSent}
              options={{
                title: 'Average Network Bytes Send',
                hAxis: { titleTextStyle: { color: '#333' }, textPosition: 'none' },
                vAxis: { minValue: 0 },
                chartArea: { width: '50%', height: '70%' },
                backgroundColor: 'transparent',
                colors: ['mediumslateblue'],
              }}
            />
            <Chart
              className="linechart"
              chartType="AreaChart"
              loader={<div>Loading Chart</div>}
              data={info.avBytesReceived}
              options={{
                title: 'Average Network Bytes Received',
                hAxis: { titleTextStyle: { color: '#333' }, textPosition: 'none' },
                vAxis: { minValue: 0 },
                chartArea: { width: '50%', height: '70%' },
                backgroundColor: 'transparent',
                colors: ['darkcyan'],
              }}
            />
          </div>
        </div>
      </div>
    </>
  )
}

export default Stats
