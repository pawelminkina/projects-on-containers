import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { PROJECTS, ROOT } from './CONSTANTS';
import Home from '../pages/Home';
import Projects from '../pages/Projects';

export const RouterConfig = () => {
  return (
    <div>
      <Switch>
        <Route exact path={ROOT} component={Home} />
        <Route exact path={PROJECTS} component={Projects} />
      </Switch>
    </div>
  );
};
