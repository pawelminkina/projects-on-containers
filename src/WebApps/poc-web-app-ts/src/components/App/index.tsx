import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { BrowserRouter } from 'react-router-dom';
import { RouterConfig } from '../../navigation/RouterConfig';
import 'fontsource-roboto';
import { ThemeProvider, Button, createTheme } from '@mui/material';
import { ThemeSwitch } from '../../components/Theme/ThemeSwitch';
import { dark, light } from '../../styles/muiTheme';

function App() {
  const [darkState, setDarkState] = useState(false);
  const handleThemeChange = () => {
    setDarkState(!darkState);
    console.log('theme=', darkState ? 'dark' : 'light');
  };

  return (
    <div className='App'>
      <header className='App-header'>
        <ThemeProvider theme={darkState ? dark() : light()}>
          <ThemeSwitch
            darkState={darkState}
            themeChangedHandler={handleThemeChange}
          />
          <BrowserRouter>
            <RouterConfig />
          </BrowserRouter>
        </ThemeProvider>
      </header>
    </div>
  );
}

export default App;
