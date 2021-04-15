import React,{useState} from 'react'

function Stats({match}) {
    const [stats, setStats] = useState([])
    const submit = async (e) => {
        e.preventDefault();
        const response = await fetch(
          `http://pc-health.somee.com/Pc/DiagnosticDataSpecific?pcId=${match.params.id}` ,{
            headers: { Authorization: "Bearer " + localStorage.getItem("token") },
          }
        );
        const ans = await response.json();
        setStats(response.data)
        console.log(stats);
    }
    return (
        <div>
            
        </div>
    )
}

export default Stats
