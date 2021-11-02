import logo from './logo.svg';
import './App.css';
import { BrowserRouter } from 'react-router-dom';
import { RouterConfig } from './navigation/RouterConfig';

function App() {
  return (
    <div className='App'>
      <header className='App-header'>
        <BrowserRouter>
          <RouterConfig />
        </BrowserRouter>
      </header>
    </div>
  );
}

export default App;
