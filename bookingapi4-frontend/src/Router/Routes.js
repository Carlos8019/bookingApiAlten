import React from 'react'
//component du router
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import App from '../Components/App'
import Availability from '../Components/Availability.js';
export default function Routes() {
  //gestion des routes d'application
  return (
        <BrowserRouter>
          <Switch>
            <Route exact path="/" component={App} />
            <Route exact path="/disponibilite" component={Availability} />
          </Switch>
        </BrowserRouter>

  )
}




