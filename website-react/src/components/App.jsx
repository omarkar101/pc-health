import React, { useState } from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import Table from "./Table";
import Services from "./Services";
import Settings from "./Settings";
import Login from "./Login";
import Nav from "./Nav";
import Register from "./Register";
import './style.css'
import ProtectedRoute from "./ProtectedRoute";
import ChangePass from "./ChangePass";
import ForgetPassword from "./ForgetPassword";
import ForgetPassword_ID from "./ForgetPassword_ID";


function App() {
  // const [buttonPopup, setButtonPopup] = useState(false);
  const [token, setToken] = useState();
  // const [state, setState] = useState({
  //   interval: 3,
  // });
  // const [isAuth,setAuth] = useState((localStorage.getItem('token') === 'false' || localStorage.getItem('token') === null) ? false :true)
  // if (localStorage.getItem('token') === 'false' || localStorage.getItem('token') === null) { setAuth(false) }
  // else{setAuth(true)}

  // const handleIntervalChange = (event) => {
  //   state.interval = event.target.value;
  //   setState(state);
  //   // setState({ interval: event.target.value })
  // };

  // const handleSubmit = (e) => {
  //   e.preventDefault();
  //   if (state.interval === "" || state.interval < 3) {
  //     alert("time interval has to be 3 seconds or longer!");
  //   } else {
  //     setState(state);
  //     setButtonPopup(false);
  //   }
  // };

  return (
    <>
      <Router>
        {/* interval={state.interval} */}
        <Nav setToken={setToken} />
        {/* <Route path="/" exact component={Login}/> */}
        <main>
          <Route path="/" exact component={Login}>
            <Login setToken={setToken} />
          </Route>
          {/* <div className="settingsDiv">
        <button className="SettingsButton" onClick={() => setButtonPopup(true)}> Settings </button>
        </div> */}
          {/* <layer> */}
            {/* <Settings trigger={buttonPopup} setTrigger={setButtonPopup}>
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
        </Settings> */}
          {/* </layer> */}
          {/* <ProtectedRoute exact path="/table" component={Table}>
            <Table i={state}></Table>
          </ProtectedRoute> */}
          <ProtectedRoute path="/table/:id" component={Services} />
          <ProtectedRoute path="/ChangePass" component={ChangePass}/>
          <Route path="/register" component={Register} />
          <Route  path="/ForgetPassword" component={ForgetPassword}/>
          <Route  path="/forgotpassword/Code" component={ForgetPassword_ID}/>
          {/* <Route path="/NewPassword" component={ForgetPassword_ID}/> */}
        </main>
      </Router>
    </>
  );
}

// {
//    <Table interval={state.interval}></Table> 
// }
export default App;
