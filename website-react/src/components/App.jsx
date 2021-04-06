import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import Table from "./Table";
import Services from './Services'
import Settings from './Settings'
import './style.css'




function App() {

  const [buttonPopup, setButtonPopup] = useState(false);

  const [state, setState] = useState({
    interval: 3
  })

  const handleIntervalChange = (event) => {
    state.interval = event.target.value;
    setState(state);
    // setState({ interval: event.target.value })
  }

  const handleSubmit = (e) => {
    e.preventDefault();
    if (state.interval === '' || state.interval < 3) {
      alert("time interval has to be 3 seconds or longer!")
    }
    else {
      console.log(state)
      setState(state);
      setButtonPopup(false);
    }
  }

  return (
    <>
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
      <Router interval={state.interval}>
        <Route path="/" exact component={Table}><Table i={state}></Table></Route>
        <Route path='/:id' component={Services} />
      </Router>
    </>
  )
}

export default App;
