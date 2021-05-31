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
                <Col><h2>Connect</h2></Col>
                <Col><h2>Open Rooms</h2></Col>
            </div>
            <div>
                <div>
                {errorMessage != null &&
                    <div variant={'danger'}>
                        <b>[!]</b> {errorMessage}
                    </div>}

                <div onSubmit={submit}>
                    <div>
                        <div>Room name</div>
                        <div
                            name="RoomName"
                            placeholder="Room name"
                            value={roomName}
                            onChange={(e) => setRoomName(e.target.value)}
                        />
                    </div>

                    <div>
                        <div>Username</div>
                        <div
                            name="UserName"
                            placeholder="Your username"
                            value={userName}
                            onChange={(e) => setUserName(e.target.value)}
                        />
                    </div>

                    <button type="submit" disabled={!userName || !roomName}>Connect</button>
                </div>
                </div>

                <div>
                {openRooms.map((room, key) => (
                    <div className="mb-1">
                        <div>
                            <div>{room.author}'s {room.name}</div>
                            <a href="#">Enter address</a>
                        </div>
                    </div>
                ))}
                </div>
            </div>
        </div>
    );
};
