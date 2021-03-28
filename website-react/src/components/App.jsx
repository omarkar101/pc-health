import React from "react";
import { BrowserRouter as Router, Route} from "react-router-dom";
import Table from "./Table";
import Services from './Services'


function App() {
  return (
    <Router>
      <Route path="/" exact component={Table} />
      <Route path = '/:id' component={Services}/>
  </Router>)
}

export default App;
