import React, { useState, useEffect } from 'react';
import { Container, Form, FormLabel, Button, Card, Row, Col, Alert } from 'react-bootstrap';

export const Lobby = ({ joinRoom, errorMessage, openRooms }) => {
    const [userName, setUserName] = useState("");
    const [roomName, setRoomName] = useState("");

    function submit(e) {
        e.preventDefault();
        console.log("Form submitted!");
        joinRoom(userName, roomName);
    }

    return (
        <Container>
            <Row className="mb-3 mt-3">
                <Col><h2>Connect</h2></Col>
                <Col><h2>Open Rooms</h2></Col>
            </Row>
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
                            value={roomName}
                            onChange={(e) => setRoomName(e.target.value)}
                        />
                    </Form.Group>

                    <Form.Group>
                        <FormLabel>Username</FormLabel>
                        <Form.Control
                            name="UserName"
                            placeholder="Your username"
                            value={userName}
                            onChange={(e) => setUserName(e.target.value)}
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
