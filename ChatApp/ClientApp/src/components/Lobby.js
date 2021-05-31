import React, { useState, useEffect } from 'react';

export const Lobby = ({ joinRoom, errorMessage, openRooms }) => {
    const [userName, setUserName] = useState("");
    const [roomName, setRoomName] = useState("");

    function submit(e) {
        e.preventDefault();
        console.log("Form submitted!");
        joinRoom(userName, roomName);
    }

    return (
        <div>
            <div className="mb-3 mt-3">
                <div><h2>Connect</h2></div>
                <div><h2>Open Rooms</h2></div>
            </div>
            <div>
                <div>
                {errorMessage != null &&
                    <div variant={'danger'}>
                        <b>[!]</b> {errorMessage}
                    </div>}

                <form onSubmit={submit}>
                    <div>
                        <div>Room name</div>
                        <input
                            name="RoomName"
                            placeholder="Room name"
                            value={roomName}
                            onChange={(e) => setRoomName(e.target.value)}
                        />
                    </div>

                    <div>
                        <div>Username</div>
                        <input
                            name="UserName"
                            placeholder="Your username"
                            value={userName}
                            onChange={(e) => setUserName(e.target.value)}
                        />
                    </div>

                    <button type="submit" disabled={!userName || !roomName}>Connect</button>
                </form>
                </div>
            </div>
        </div>
    );
};
