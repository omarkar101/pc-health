import React, { useState, useEffect } from 'react'
import Chart from "react-google-charts";
import "./style.css";

function Stats({ match }) {
  const [stats, setStats] = useState([])

  const FetchStats = async () => {
    // e.preventDefault();
    // console.log("id", match.params.id)
    const response = await fetch(
      `https://pc-health.azurewebsites.net/Pc/DiagnosticDataSpecific?pcId=${ match.params.id }`, {
      headers: { Authorization: "Bearer " + localStorage.getItem("token") },
    }
    );
    const ans = await response.json();
    setStats(ans)
    // console.log(ans)
  }

  // const [stats, setStats] = useState([])
  const [info, setInfo] = useState({
    cpu: [['secs', '%'],],
    memory: [['secs', '%'],],
    avBytesSent: [['secs', 'bytes'],],
    avBytesReceived: [['secs', 'bytes'],]
  });


  // useEffect(() => {
  //   FetchStats()
  // }, );

  // useEffect(() => {
  //   const timer = setTimeout(() => {
  //     FetchStats()
  //   }, 1000);
  //   return () => {
  //     clearTimeout(timer);
  //   }
  // }, []);

  useEffect(() => {
    const interval = setInterval(() => {
      FetchStats()
    }, 1000);
    return () => clearInterval(interval);
  }, []);

  // setInterval ( () =>{
  //   FetchStats();
  // }, 1000)

  function init(){
    info.cpu= [['secs', '%'],]
    info.memory= [['secs', '%'],]
    info.avBytesSent= [['secs', 'bytes'],]
    info.avBytesReceived= [['secs', 'bytes'],]
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
      <div className="wrapper">
        <div className="first">
          <Chart
            width={'500px'}
            height={'300px'}
            chartType="AreaChart"
            loader={<div>Loading Chart</div>}
            data={info.cpu}
            options={{
              title: 'CPU Performance',
              hAxis: { titleTextStyle: { color: '#333' }, textPosition: 'none' },
              vAxis: { minValue: 0 },
              chartArea: { width: '50%', height: '70%' },
              colors: ['cornflowerblue'],
              backgroundColor: 'white',
            }}
          />
        </div>
        <div className="second">
          <Chart
            width={'500px'}
            height={'300px'}
            chartType="AreaChart"
            loader={<div>Loading Chart</div>}
            data={info.memory}
            options={{
              title: 'Memory Usage',
              hAxis: { titleTextStyle: { color: '#333' }, textPosition: 'none' },
              vAxis: { minValue: 0 },
              chartArea: { width: '50%', height: '70%' },
              colors: ['lightpink'],
            }}
          /></div>
        <div className="third">
          <Chart
            width={'500px'}
            height={'300px'}
            chartType="AreaChart"
            loader={<div>Loading Chart</div>}
            data={info.avBytesSent}
            options={{
              title: 'Average Network Bytes Send',
              hAxis: { titleTextStyle: { color: '#333' }, textPosition: 'none' },
              vAxis: { minValue: 0 },
              chartArea: { width: '50%', height: '70%' },
              colors: ['mediumslateblue'],
            }}
          />
        </div>
        <div className="fifth">
          <Chart
            width={'500px'}
            height={'300px'}
            chartType="AreaChart"
            loader={<div>Loading Chart</div>}
            data={info.avBytesReceived}
            options={{
              title: 'Average Network Bytes Received',
              hAxis: { titleTextStyle: { color: '#333' }, textPosition: 'none' },
              vAxis: { minValue: 0 },
              chartArea: { width: '50%', height: '70%' },
              colors: ['darkcyan'],
              // lineWidth: 25
            }}
          />
        </div>
      </div>
    </>
  )
}

export default Stats
