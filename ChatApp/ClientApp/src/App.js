import React, { Component } from 'react';
import { Lobby } from './components/Lobby';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

import './custom.css'
import { Room } from './components/Room';

export default class App extends Component {
    static displayName = App.name;

    state = {
        connection: null
    };

    joinRoom = async (client, room) => {
        const connection = new HubConnectionBuilder()
            .withUrl("chathub")
            .configureLogging(LogLevel.Information)
            .build();

        connection.on("ReceiveMessage", (client, message) => {
            console.log("message received " + client + ": " + message);
        });

        await connection.start();
        await connection.invoke("JoinRoom", { client, room });

        this.setState({ connection });
    };

    render() {
        if (this.state.connection === null) {
            return <Lobby joinRoom={this.joinRoom} />
        }
        else {
            return <Room connection={this.state.connection} />
        }
    }
}
