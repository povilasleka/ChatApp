import React, { useState } from 'react';
import { Container, Form, FormLabel, Button } from 'react-bootstrap';

export const Lobby = () => {
    const [userName, setUserName] = useState("");
    const [roomName, setRoomName] = useState("");

    return (
        <Container>
            <h1>Choose a room</h1>
            <Form>
                <Form.Group>
                    <FormLabel>Room name</FormLabel>
                    <Form.Control
                        name="RoomName"
                        placeholder="Room name"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                    />
                </Form.Group>

                <Form.Group>
                    <FormLabel>Username</FormLabel>
                    <Form.Control
                        name="UserName"
                        placeholder="Your username"
                        value={roomName}
                        onChange={(e) => setRoomName(e.target.value)}
                    />
                </Form.Group>

                <Button type="submit" disabled={!userName || !roomName}>Connect</Button>
            </Form>
        </Container>
    );
};
