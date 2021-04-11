import React from 'react'
import { Route, Redirect} from 'react-router-dom'

// function ProtectedRoute({ isAuth: isAuth, component: Component, logout: logout, ...rest }) {
//     return (
//         <Route {...rest} render={(prop) => {
//             if (isAuth) {
//                 return <Component logout={logout}/>
//             } else {
//                 return <Redirect to={{pathname: "/"}}/>
//             }
//         }}/>
//     )
    
// }

function ProtectedRoute({ component: Component, ...rest }) {
    const isAuth =
      (localStorage.getItem("token") !== "false" &&
      localStorage.getItem("token") !== null)
    return (isAuth ? (<Route {...rest} component={Component}/>) : <Redirect to='/'/>)
}

export default ProtectedRoute
