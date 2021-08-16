import React from 'react';
import ReactDOM from 'react-dom';
import App from './Components/App';
import 'bootstrap/dist/css/bootstrap.min.css'
import { ModalProvider } from './Contexts/ModalContext';

ReactDOM.render(
  <React.StrictMode>
    <ModalProvider>
    <App />
    </ModalProvider>
  </React.StrictMode>,
  document.getElementById('root')
);
