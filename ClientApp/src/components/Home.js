import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Greetings, gamer</h1>
        <p>Welcome to our game. Press <b>Game</b> in the navigation bar to game the game!</p>
      </div>
    );
  }
}
