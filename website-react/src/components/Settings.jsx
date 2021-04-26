import React from "react";
import './NavStyle.css'


function Settings(props) {

    return (props.trigger) ?
        (< div className="popup" >
            <div className="popup-inner">
                <span onClick={() => props.setTrigger(false)} className="close-btn"
                    title="Close Modal">&times;
                </span>
                {props.children}
            </div>

        </div >) : "";

}


export default Settings
