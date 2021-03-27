import { useEffect, useState } from "react"
import {Link,Route,BrowserRouter as Router} from 'react-router-dom'
import React from 'react'



function TableRow(x) {
    const S = {
        background: 'lightblue'
    }
    return (
        <tr key={x.PC_ID}>
            <td> <Link to={'/' + x.PC_ID}>{x.PC_ID}</Link></td>
                    <td>{x.OS}</td>
                    <td>{x.CpuUsage}</td>
                    <td>{x.TotalFreeDiskSpace}</td>
                    <td>{x.MemoryUsage}</td>
                    <td>{x.AvgNetworkBytesSent}</td>
                    <td>{x.AvgNetworkBytesReceived}</td>
                   <td>{x.FirewallStatus}</td>
    </tr>)
}

export default TableRow