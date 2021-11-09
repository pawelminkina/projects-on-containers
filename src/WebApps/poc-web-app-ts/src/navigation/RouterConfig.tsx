import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { PROJECTS, ROOT } from './CONSTANTS';
import Home from '../components/Pages/Home';
import Projects from '../components/Pages/Projects';

export const RouterConfig = () => {
  return (
    <div>
      <Routes>
        <Route path={ROOT} element={<Home />} />
        <Route path={PROJECTS} element={<Projects />} />
      </Routes>
    </div>
  );
};
