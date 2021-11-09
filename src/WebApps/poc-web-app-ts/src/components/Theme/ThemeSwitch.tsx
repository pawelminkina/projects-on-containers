import React from 'react';
import { Switch, Button } from '@mui/material';

export interface ThemeSwitchProps {
  darkState: boolean;
  themeChangedHandler: (
    event: React.ChangeEvent<HTMLInputElement>,
    changed: boolean
  ) => void;
}

export class ThemeSwitch extends React.Component<ThemeSwitchProps> {
  render() {
    return (
      <div>
        <Switch
          checked={this.props.darkState}
          onChange={this.props.themeChangedHandler}
        />
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
  }
}
