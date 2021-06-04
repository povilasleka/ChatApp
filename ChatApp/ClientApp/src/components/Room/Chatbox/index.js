import React, {useState} from "react";

import "./style.css";
import { Message } from "./Message";

export const Chatbox = function ({ messages, handleSend }) {
    const [messageText, setMessageText] = useState("");
    
    function send(e) {
        e.preventDefault();
        handleSend(messageText);
    }

    return (
        <div className="chatbox padding-sides">
            <div className="chatbox-display">
                {messages.map((message, key) =>
                    <Message
                        message={message}
                        positionRight={message.author === "You"}
                        key={key} />
                )}
            </div>

            <div className="chatbox-write">
                <form onSubmit={send}>
                    <input
                        type="text"
                        placeholder="Your message"
                        value={messageText}
                        onChange={(e) => setMessageText(e.target.value)}
                    />

                    <button>Send</button>
                </form>
            </div>
        </div>
    );
};