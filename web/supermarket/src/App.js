import React from 'react';
import { Router, Route } from 'react-router-dom';
import { connect } from 'react-redux';

import logo from './logo.svg';
import './App.css';
import SignUp from './_components/SignUp';
import { createStore } from 'redux'
import rootReducer from './_reducers'

class App extends React.Component {
  constructor(props) {
    super(props);

    const { dispatch } = this.props;

  }

  render() {
    const { alert } = this.props;
    return (
      <div className="jumbotron">
        <SignUp />
      </div>
    );
  }
}

function mapStateToProps(state) {
  const { alert } = state;
  return {
    alert
  };
}

const connectedApp = connect(mapStateToProps)(App);
export { connectedApp as App };
