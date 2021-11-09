import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './components/App';
import reportWebVitals from './reportWebVitals';
import BffApiProvider from './service/BffApiProvider';

fetch(`${window.location.origin}/config.json`, { cache: 'no-cache' })
  .then((response) => response.json())
  .then((appsettings) => {
    ReactDOM.render(
      <BffApiProvider baseUri={appsettings.services.bffApi}>
        <React.StrictMode>
          <App />
        </React.StrictMode>
      </BffApiProvider>,
      document.getElementById('root')
    );
  });

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
