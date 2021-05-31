import React from "react";

import "./style.css";

export const Sidebar = function ({ users }) {
    return (
        <div class="sidebar padding-sides">
            {users.map((user, key) => (
                <div key={key} class="sidebar-item">
                    {user}
                </div>
            ))}
        </div>
    );
};