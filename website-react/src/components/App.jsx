import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import Table from "./Table";
import Services from "./Services";
import Settings from "./Settings";
import Login from "./Login";
// import Test from './Test'
import Nav from "./Nav";
import Register from "./Register";

function App() {
  const [buttonPopup, setButtonPopup] = useState(false);
  const [token, setToken] = useState();
  const [state, setState] = useState({
    interval: 3,
  });

  const handleIntervalChange = (event) => {
    state.interval = event.target.value;
    setState(state);
    // setState({ interval: event.target.value })
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (state.interval === "" || state.interval < 3) {
      alert("time interval has to be 3 seconds or longer!");
    } else {
      // setState({ interval: event.target.value })
      console.log(state);
      setState(state);
      setButtonPopup(false);
    }
  };

  return (
    <>
      {/* <Test interval={state.interval}></Test> */}
      <Router interval={state.interval}>
        <Nav />
        {/* <Route path="/" exact component={Login}/> */}
        <button onClick={() => setButtonPopup(true)}> Settings </button>
        <Settings trigger={buttonPopup} setTrigger={setButtonPopup}>
          <form onSubmit={handleSubmit}>
            <label> Time Interval</label>
            <br />
            <input
              type="number"
              onChange={handleIntervalChange}
              placeholder="set time interval (in seconds)"
            />
            <br />
            <button className="save-btn" type="submit">
              Save Changes
            </button>
            {/* <Table interval={state.interval}></Table> */}
          </form>
        </Settings>

        <main>
          <Route path="/" exact component={Login}>
            <Login />
          </Route>
          <Route path="/table" exact component={Table}>
            <Table i={state}></Table>
          </Route>
          {/* <Route path="/:id" component={Services} /> */}
          <Route path="/register" component={Register} />
        </main>
      </Router>
    </>
  );
}
//
{
  /* <Table interval={state.interval}></Table> */
}
export default App;
