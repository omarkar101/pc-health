import React, {useEffect,useState }from 'react'


function Update() {
    let url = 'https://pchealth.azurewebsites.net/api/Base/GetDiagnosticData';

        var xhReq = new XMLHttpRequest();
        xhReq.open("GET", url, false);
        xhReq.send(null);
        return JSON.parse(xhReq.responseText);
}

// const API_Host = 'http://069fea200952.ngrok.io/api/Base/GetDiagnosticData'
// const INVENTORY_API_URL=`${API_Host}/inventory`


// const datalst = [{
//     'PC_ID':1,
//     'OS': "Linux",
//     'CpuUsage': 100,
//     'Free':100

// }, {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
// },{
//     'PC_ID':1,
//     'OS': "Linux",
//     'CpuUsage': 100,
//     'Free':100

// }, {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 8,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
// },{
//     'PC_ID':1,
//     'OS': "Linux",
//     'CpuUsage': 100,
//     'Free':100

// }, {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
// },{
//     'PC_ID':1,
//     'OS': "Linux",
//     'CpuUsage': 100,
//     'Free':100

// }, {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 8,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
// },{
//     'PC_ID':1,
//     'OS': "Linux",
//     'CpuUsage': 100,
//     'Free':100

// }, {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
// },{
//     'PC_ID':1,
//     'OS': "Linux",
//     'CpuUsage': 100,
//     'Free':100

// }, {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 8,
//     'Free':190
//     },
// {
//     'PC_ID':2,
//     'OS': "Windows",
//     'CpuUsage': 10,
//     'Free':90
// }]
// function addToTable(datalst) {
//     let user = 1;
//     const tablebody = document.getElementById('tableData');
//     let datastr = '';
//     for (let x of datalst[0]) {
//         datastr += `<tr  onclick = "showHideRow('hidden_row${num}')"><td>${'user-' + x.PC_ID.slice(0,4)}</td><td>${x.OS}</td><td>${x.CpuUsage.toPrecision(5)}%</td><td>${x.TotalFreeDiskSpace.toPrecision(5)} GB</td><td>${x.MemoryUsage.toPrecision(4)} %</td><td>${x.AvgNetworkBytesSent.toPrecision(12)}</td><td>${x.AvgNetworkBytesReceived.toPrecision(12)}</td><td>${'user' + x.PC_ID.slice(0,4)+ '@gmail.com'}</td></tr>
//         <tr style="display:none; background-color: #9dcfccda; white-space:pre-wrap; word-wrap:break-word;"  id="hidden_row${num}"><td colspan="8">PC's ID: ${x.PC_ID} 
//         Total Disk Space= ${x.DiskTotalSpace} GB </tr>`;
//         num++
//         user++
//     }
//     tablebody.innerHTML = datastr;

// const API_HOST = 'https://069fea200952.ngrok.io/api/Base/GetDiagnosticData';


function App() {
    const [datalst, setdatalst] = useState([])
    useEffect(() => {
        datalst.map((x) =>
                    <tr key={x.PC_ID}>
                        <td>{x.PC_ID}</td>
                        <td>{x.OS}</td>
                        <td>{x.CpuUsage}</td>
                        <td>{x.TotalFreeDiskSpace}</td>
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
                </tr>
            )}
            </tbody>
    </table>)
}


export default App