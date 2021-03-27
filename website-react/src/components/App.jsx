import React, { useEffect, useState } from 'react'
import { Table } /*{ Update }*/ from './Table'
import { Link, BrowserRouter as Router, Route, Redirect, Switch } from 'react-router-dom'
import TableRow from './TableRow'
import Services from './Services'
import {SortingTable } from './SortingTable'
const datalst = [{"CpuUsage":13.206216,"TotalFreeDiskSpace":805.2314,"DiskTotalSpace":998.3052,"MemoryUsage":42.882226905539916,"AvgNetworkBytesSent":354624474,"AvgNetworkBytesReceived":4170268287,"PC_ID":"GC4AfW43BT2T21VTQKgo4lGLYuuhLqg9srdwXwSTFWE","OS":"Windows"}]


function App() {
    return (
        // <Router>
        // <Switch>
        // <Route path="/:id"  exact component = {Services}/>    
        // <Route path="/"exact component={Table}/>
        //     </Switch></Router>)
        <SortingTable/>)
}


export default App