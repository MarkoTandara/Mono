import React from 'react'

function Button(props) {
  return (
    <button onClick={props.customFunction} className="Btn">
        {props.text}
    </button>
  );
}

export default Button