import React, { useState } from "react";
import './settings.css'
// import Form from './settingsForm'


function Settings(props) {

    return (props.trigger) ?
        (< div className="popup" >
            <div className="popup-inner">
                {/* <button className="close-btn" onClick={() => props.setTrigger(false)}>
                    close
                </button> */}
                <span onClick={() => props.setTrigger(false)} className="close-btn"
                    title="Close Modal">&times;
                </span>
                {props.children}
            </div>

        </div >) : "";
    
}


export default Settings
