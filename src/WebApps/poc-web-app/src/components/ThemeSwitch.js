import React from 'react';
import { Switch, Button } from '@material-ui/core';

export const ThemeSwitch = ({ darkState, handleThemeChange }) => {
  return (
    <div>
      <Switch checked={darkState} onChange={handleThemeChange} />
      <Button
        variant='contained'
        color='primary'
        onClick={() => {
          console.log('Btn clicked!');
        }}
      >
        Theme Test
      </Button>
    </div>
  );
};
