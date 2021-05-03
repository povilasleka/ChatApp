import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Lobby } from './components/Lobby';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

import './custom.css'

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

    render () {
        return (
            <Layout>
                <Lobby joinRoom={this.joinRoom}/>
            </Layout>
        );
    }
}
