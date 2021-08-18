import React from 'react'
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import App from '../Components/App'
import { ModalProvider } from '../Contexts/ModalContext'
import Availability from '../Components/Availability.js'
import { ReservationProvider } from '../Contexts/ReservationContext'
import UserProvider from '../Contexts/UserContext'
export default function Routes() {
  return (
    <UserProvider>
      <ModalProvider>
        <BrowserRouter>
          <Switch>
            <Route exact path="/" component={App} />
            <Route exact path="/disponibilite">
              <ReservationProvider>
                <Availability />
              </ReservationProvider>
            </Route>
          </Switch>
        </BrowserRouter>
      </ModalProvider>
    </UserProvider>
  )
}





