import React from 'react'
import { useState, useEffect } from 'react'
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom'
import { Columns } from './Coloumns'
import { useMemo } from 'react'
import Data from './Data.json'
import './table.css'
import {useTable} from 'react-table'

// function Update() {
//     let url = 'https://pchealth.azurewebsites.net/api/Base/GetDiagnosticData';

//         var xhReq = new XMLHttpRequest();
//         xhReq.open("GET", url, false);
//         xhReq.send(null);
//         return JSON.parse(xhReq.responseText);
// }

// function handle(id) {
//     console.log(id);
// }


// function Table() {
//     const [datalst, setdatalst] = useState([])
//     // useEffect(() => {
//     //     datalst.map((x) =>
//     //         <tr key={x.PC_ID}>
//     //             <td>{x.PC_ID}</td>
//     //             <td>{x.OS}</td>
//     //             <td>{x.CpuUsage}</td>
//     //             <td>{x.TotalFreeDiskSpace}</td>
//     //             <td>{x.MemoryUsage}</td>
//     //             <td>{x.AvgNetworkBytesSent}</td>
//     //             <td>{x.AvgNetworkBytesRecieved}</td>
//     //             <td>{x.FirewallStatus}</td>
//     //         </tr>
//     //     )
//     // });
//     let updateCycle
//     useEffect(() => {
//         updateCycle = setInterval(() => setdatalst(Update), 1000) // Set a timer as a side effect
//         return () => clearInterval(updateCycle) // Here is the cleanup function: we take down the timer
//     }, [])

//     return (<table className="table table-border">
//         <thead>
//             <tr className="header">
//                 <th onclick="sortByColumn('PC_ID')">Username</th>
//                 <th onclick="sortByColumn('OS')">OS</th>
//                 <th onclick="sortByColumn('CpuUsage')">CPU Usage</th>
//                 <th onclick="sortByColumn('TotalFreeDiskSpace')">Free Disk Space</th>
//                 <th onclick="sortByColumn('MemoryUsage')">Memory Usage</th>
//                 <th onclick="sortByColumn('AvgNetworkBytesSent')">Average Network-Bytes Sent</th>
//                 <th onclick="sortByColumn('AvgNetworkBytesReceived')">Average Network-Bytes Received</th>
//                 {/* <th onclick="sortByColumn('PC_ID')">Contact Info</th> */}
//                 <th onclick="sortByColumn('Firewall Status')">Firewall Status</th>
//                 <th>Services Detials</th>
//             </tr>
//             {/* <button onClick={()=>setdatalst(Update())}></button> */}
//         </thead>
//         <tbody id="tableData">
//             {datalst.map((x) =>
//                     // {<TableRow {...x} />}
//                  <tr key={x.PC_ID}>
//                     <td>{x.PC_ID}</td>
//                     <td>{x.OS}</td>
//                     <td>{x.CpuUsage}</td>
//                     <td>{x.TotalFreeDiskSpace}</td>
//                     <td>{x.MemoryUsage}</td>
//                     <td>{x.AvgNetworkBytesSent}</td>
//                     <td>{x.AvgNetworkBytesReceived}</td>
//                     <td>{x.FirewallStatus}</td>
//                     <td><Link to={'/'+x.PC_ID} style={{textDecoration:'none'}} onClick = {handle(x.PC_ID)}>Details</Link></td>
//     </tr>
//             )}
//         </tbody>
//     </table>)
// }
// export default Table;

export const Table = () => {
    const columns = useMemo(() => Columns, []);
  const data = useMemo(() => Data, []);
  const instance = useTable({
    columns,
    data
  });
  const {
    getTableProps,
    getTableBodyProps,
      headerGroups,
    footerGroups,
    rows,
    prepareRow
  } = instance;
  return (
    <div>
      <table {...getTableProps}>
        <thead>
          {headerGroups.map((headerGroup) => (
            <tr {...headerGroup.getHeaderGroupProps()}>
              {headerGroup.headers.map((column) => (
                <th {...column.getHeaderProps()}>
                  {column.render('Header')}
                </th>
              ))}
            </tr>
          ))}
        </thead>
        <tbody {...getTableBodyProps}>
          {rows.map((row) => {
            prepareRow(row);
            return <tr {...row.getRowProps()}>{row.cells.map(cell=>{
              return <td{...cell.getCellProps()}>{cell.render('Cell')}</td>
            })}</tr>;
          })}
              </tbody>
              <tfoot>
                  {
                      footerGroups.map(footerGroup => (
                          <tr{...footerGroup.getFooterGroupProps()}>
                              {footerGroup.headers.map(column => (
                                  <td {...column.GetFooterProps}>
                                      {
                                          column.render('Footer')
                                      }</td>
                              ))}
                          </tr>
                      ))
                  }</tfoot>
      </table>
    </div>
  );
}