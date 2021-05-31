import React from "react";

import "./style.css";
import { Message } from "./Message";

export const Chatbox = function ({ messages }) {
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
                <form>
                    <input type="text" placeholder="Your message" />
                    <button>Send</button>
                </form>
            </div>
        </div>
    );
};