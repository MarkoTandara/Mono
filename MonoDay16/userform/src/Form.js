import React, { useState } from 'react'
import Button from './Button';
import countyData from './county.json';


function Form(props) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [email, setEmail] = useState('');
  const [county, setCounty] = useState('');
  const [error, setError] = useState(null);

  function isValidEmail(email) {
    return /\S+@\S+\.\S+/.test(email);
  }
  

  const changeUsername = (event) => {
    setUsername(event.target.value);
  };
  
  const changePassword = (event) => {
    setPassword(event.target.value);
  };

  const changeEmail = (event) => {
    if (!isValidEmail(event.target.value)) {
      setError('Email is invalid');
    } else {
      setError(null);
    }
    setEmail(event.target.value);
  };
  
  const changeCounty = (event) => {
    setCounty(event.target.value);
  };

  const transferValue = (event) => {
    event.preventDefault();
    const val = {
      username,
      password,
      email,
      county,
    };
    props.func(val);
    clearState();
  };
  
  const clearState = () => {
    setUsername('');
    setPassword('');
    setEmail('');
    setCounty('');
  };
  return (
    <form>
      <div>
        <label>Username</label>
        <input type="text" value={username} onChange={changeUsername} />
      </div>
      <div>
        <label>Password</label>
        <input type="text" value={password} onChange={changePassword} />
      </div>
      <div>
        <label>Email</label>
        <input type="text" value={email} onChange={changeEmail} />
      </div>
      <div>
        <label>County</label>
        <input type="text" value={county} onChange={changeCounty} />
      </div>
      {error && <a style={{color: 'red'}}>{error}</a>}
      <Button customFunction={transferValue} text="Submit" />
    </form>
  );
}

export default Form