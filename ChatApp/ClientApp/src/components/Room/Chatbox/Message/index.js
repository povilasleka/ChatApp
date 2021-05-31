import React from "react";
import "./style.css";

export const Message = function ({ message, positionRight }) {
    let chatBoxClass = "chatbox-message";
    if (positionRight == true) {
        chatBoxClass = "chatbox-message-right";
    }

    return (
        <div className={chatBoxClass}>
            <div className="chatbox-message-author">
                {message.author}
            </div>
            <div className="chatbox-message-text">
                {message.text}
            </div>
        </div>
    );
}