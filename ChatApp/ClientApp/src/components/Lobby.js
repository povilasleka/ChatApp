import React, { useState, useEffect } from 'react';
import { Container, Form, FormLabel, Button, Card, Row, Col, Alert } from 'react-bootstrap';
import axios from 'axios';

export const Lobby = ({ joinRoom, errorMessage }) => {
    const [userName, setUserName] = useState("");
    const [roomName, setRoomName] = useState("");
    const [openRooms, setOpenRooms] = useState([]);

    function submit(e) {
        e.preventDefault();
        console.log("Form submitted!");
        joinRoom(userName, roomName);
    }

    useEffect(() => {
        axios.get('/room')
            .then(function (response) {
                setOpenRooms(response.data);
            });
    }, []);

    return (
        <Container>
            <h1>Choose a room</h1>
            <Row>
                <Col>
                {errorMessage != null &&
                    <Alert variant={'danger'}>
                        <b>[!]</b> {errorMessage}
                    </Alert>}

                <Form onSubmit={submit}>
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
                </Col>

                <Col>
                {openRooms.map((room, key) => (
                    <Card className="mb-1">
                        <Card.Body>
                            <Card.Subtitle>{room.author}'s {room.name}</Card.Subtitle>
                            <Card.Link href="#">Enter address</Card.Link>
                        </Card.Body>
                    </Card>
                ))}
                </Col>
            </Row>
        </Container>
    );
};
