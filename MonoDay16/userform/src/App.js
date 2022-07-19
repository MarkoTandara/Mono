import logo from './logo.svg';
import './App.css';
import Button from './Button';
import Table from './Table';



function App() {
  return (
    <div className="App">
      <header className="App-header">
      <img src={logo} className="App-logo" alt="logo" />
        <ul>
          <li><Button text="Home"/></li>
          <li><Button text="Service"/></li>
          <li><Button text="Contact"/></li>
          <li><Button text="Sign In"/></li>
        </ul>
      </header>
      <Table/>
    </div>
  );
}

export default App;
