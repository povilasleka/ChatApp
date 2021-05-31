import React from "react";

import "./style.css";

export const Chatbox = function ({ messages }) {
    return (
        <div class="chatbox padding-sides">
            <div class="chatbox-display">
                {messages.map((message, key) => {
                    let chatBoxClass = "chatbox-message";
                    if (message.author === 'You')
                        chatBoxClass = "chatbox-message-right";

                    return (
                        <div class={chatBoxClass}>
                            <div class="chatbox-message-author">
                                {message.author}
                            </div>
                            <div class="chatbox-message-text">
                                {message.text}
                            </div>
                        </div>
                    );
                })}
            </div>

            <div class="chatbox-write">
                <form>
                    <input type="text" placeholder="Your message" />
                    <button>Send</button>
                </form>
            </div>
        </div>
    );
};