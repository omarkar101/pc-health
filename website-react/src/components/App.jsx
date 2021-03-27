import React, { useEffect, useState } from "react";
import axios from "axios";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import Table from "./Table";
import Services from './Services'


function App() {
  return (
    <Router>
      <Route path="/" exact component={Table} />
      <Route path = '/:id' component={Services}/>
  </Router>)
  // return (<h1>Hello</h1>)
}

export default App;
