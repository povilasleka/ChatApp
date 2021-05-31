import React from 'react';

import { Sidebar } from "./Sidebar";
import { Chatbox } from "./Chatbox";
import { Navbar } from "./Navbar";

import "./style.css";

const users = ['user1', 'user2'];
const messages = [
    { author: 'user1', text: 'Message1' },
    { author: 'user2', text: 'Message2' },
    { author: 'You', text: 'My Message3' }
];

export const Room = ({ connection }) => {
    return (
        <div className="container">
            <Navbar />
            <Sidebar users={users} />
            <Chatbox messages={messages}/>
        </div>
    );
};