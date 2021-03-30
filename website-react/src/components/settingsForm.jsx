import React, { useState, Component } from "react";
import Table from './Table'

// let i = 3;

// import Component2 from './Component2';


function Form() {
    
    const [state, setState] = useState({ interval: "" })

    const handleIntervalChange = (event) => {
        // event.preventDefault()
        // this.setState({
        //     interval: event.target.value
        // })
        // console.log(state.interval);
        setState({ interval: event.target.value })
    }

    const handleSubmit = event => {
        // event.preventDefault()
        if (this.state.interval === '' || this.state.interval < 3) {
            alert("time interval has to be 3 seconds or longer!")

        }
        else {
            this.setState({ interval: event.target.value })
            // i = this.state.interval;
        }
        // console.log(i);
    }
    // render() {
    return (

        <form onSubmit={handleSubmit}>
            <label> Time Interval
                </label>
            <br />
            <input type="text" value={state.interval} onChange={handleIntervalChange} placeholder="set time interval (in seconds)" />
            <br />
            <button className="save-btn" type="submit">
                Save Changes
            </button>
            <Table interval={state.interval}></Table>
        </form>
    )
    // }
}

export default Form
// export { i };