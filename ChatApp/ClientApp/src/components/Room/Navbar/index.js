import React from "react";

import "./index.css";

export const Navbar = function () {
    return (
        <>
            <div class="title padding-sides">
                <h5>Chat app</h5>
            </div>
            <div class="navbar padding-sides">
                <ul>
                    <li><a href="#">Change name</a></li>
                    <li><a href="#">Chat info</a></li>
                    <li><a href="#">Disconnect</a></li>
                </ul>
            </div>
        </>
    );
};