import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import Table from "./Table";
import Services from "./Services";
import Settings from "./Settings";
import Login from "./Login";
import Nav from "./Nav";
import Register from "./Register";
import './style.css'


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
      console.log(state);
      setState(state);
      setButtonPopup(false);
    }
  };

  return (
    <>
      
      
      <Router interval={state.interval}>
        <Nav />
        {/* <Route path="/" exact component={Login}/> */}
        <main>
          <Route path="/" exact component={Login}>
            <Login />
          </Route>
          <div className="settingsDiv">
        <button className="SettingsButton" onClick={() => setButtonPopup(true)}> Settings </button>
        </div>
         <layer>
        <Settings trigger={buttonPopup} setTrigger={setButtonPopup}>
          <form onSubmit={handleSubmit}>
            <label> Time Interval
                </label>
            <br />
            <input type="number" onChange={handleIntervalChange} placeholder="set time interval (in seconds)" />
            <br />
            <button type="submit">
              Save Changes
            </button>
          </form>
        </Settings>
      </layer>
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
