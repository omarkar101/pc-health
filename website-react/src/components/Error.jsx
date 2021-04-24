import React from 'react'
import './Error.css'
import ReactDOM, { render } from "react-dom";

const f = () => {
  setTimeout(() => {
    document.body.classList.remove("loading")
  },2000)
}

function Error() {

    return (
      <div id="error">
        <h1>500</h1>
        <h2>Unexpected Error <b>:(</b></h2>
        <div class="gears">
          <div class="gear one">
            <div class="bar"></div>
            <div class="bar"></div>
            <div class="bar"></div>
          </div>
          <div class="gear two">
            <div class="bar"></div>
            <div class="bar"></div>
            <div class="bar"></div>
          </div>
          <div class="gear three">
            <div class="bar"></div>
            <div class="bar"></div>
            <div class="bar"></div>
          </div>
        </div></div>
    )
}
export default Error

