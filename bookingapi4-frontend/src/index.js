import React from 'react';
import ReactDOM from 'react-dom';
import 'bootstrap/dist/css/bootstrap.min.css'
import Routes from './Router/Routes';
import { ModalProvider } from './Contexts/ModalContext';
import { ReservationProvider } from './Contexts/ReservationContext';
/*
Cette component gestione le router a la page principal ou a la page d'utilisateur
 */
ReactDOM.render(
  <React.StrictMode>
    <ModalProvider>
    <ReservationProvider>
    <Routes />
    </ReservationProvider>
    </ModalProvider>
  </React.StrictMode>,
  document.getElementById('root')
);
