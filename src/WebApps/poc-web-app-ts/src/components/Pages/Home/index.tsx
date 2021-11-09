import React from 'react';

export interface HomeProperties {}

const Home: React.FC<HomeProperties> = () => {
  return (
    <div>
      <h1>Home view</h1>
    </div>
  );
};

export default Home;
