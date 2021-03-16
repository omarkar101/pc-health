import React, {useEffect,useState }from 'react'


function Update() {
    let url = 'https://pchealth.azurewebsites.net/api/Base/GetDiagnosticData';

        var xhReq = new XMLHttpRequest();
        xhReq.open("GET", url, false);
        xhReq.send(null);
        return JSON.parse(xhReq.responseText);
}


function App() {
    const [datalst, setdatalst] = useState([])
    useEffect(() => {
        datalst.map((x) =>
                    <tr key={x.PC_ID}>
                        <td>{x.PC_ID}</td>
                        <td>{x.OS}</td>
                        <td>{x.CpuUsage}</td>
                <td>{x.TotalFreeDiskSpace}</td>
                <td>{x.MemoryUsage}</td>
                <td>{x.AvgNetworkBytesSent}</td>
                <td>{x.AvgNetworkBytesRecieved}</td>
                    </tr>
                )
    });
    let updateCycle
useEffect(() => {
    updateCycle = setInterval(() => setdatalst(Update), 5000) // Set a timer as a side effect
   return () => clearInterval(updateCycle) // Here is the cleanup function: we take down the timer
},[])

    return (<table class = "table table-border">
            <thead>
                <tr>
                    <th onclick="sortByColumn('PC_ID')">Username</th>
                    <th onclick="sortByColumn('OS')">OS</th>
                    <th onclick="sortByColumn('CpuUsage')">CPU Usage</th>
                    <th onclick="sortByColumn('TotalFreeDiskSpace')">Free Disk Space</th>
                    <th onclick="sortByColumn('MemoryUsage')">Memory Usage</th>
                    <th onclick="sortByColumn('AvgNetworkBytesSent')">Average Network-Bytes Sent</th>
                    <th onclick="sortByColumn('AvgNetworkBytesReceived')">Average Network-Bytes Received</th>
                    <th onclick="sortByColumn('PC_ID')">Contact Info</th>
            </tr>
            {/* <button onClick={()=>setdatalst(Update())}></button> */}
            </thead>
        <tbody id="tableData">
            {datalst.map((x) =>
                <tr key={x.PC_ID}>
                    <td>{x.PC_ID}</td>
                    <td>{x.OS}</td>
                    <td>{x.CpuUsage}</td>
                    <td>{x.TotalFreeDiskSpace}</td>
                    <td>{x.MemoryUsage}</td>
                <td>{x.AvgNetworkBytesSent}</td>
                <td>{x.AvgNetworkBytesReceived}</td>
                </tr>
            )}
            </tbody>
    </table>)
}


export default App