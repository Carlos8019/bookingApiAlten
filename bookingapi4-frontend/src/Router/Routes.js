import React from 'react'
import { BrowserRouter, Route, Switch,Redirect } from 'react-router-dom'
import App from '../Components/App'
import Availability from '../Components/Availability.js';
export default function Routes() {
  return (

        <BrowserRouter>
          <Switch>
            <Route exact path="/" component={App} />
            <Route exact path="/disponibilite" component={Availability} />
          </Switch>
        </BrowserRouter>

  )
}




