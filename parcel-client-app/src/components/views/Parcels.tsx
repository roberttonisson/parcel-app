import { TextField } from "@material-ui/core";
import { FormEvent, useEffect, useState } from "react";
import { Button, Container, Modal, Table } from "react-bootstrap";
import { useHistory } from "react-router-dom";
import { BaseService } from "../../base/BaseService";
import { IParcel } from "../../domain/IParcel";



const Parcels = () => {
    const history = useHistory();
    const [parcels, setParcels] = useState([] as IParcel[]);
    const [newParcel, setNewParcel] = useState({recipient: "", destination: '', weight: 0, price: 0
    } as IParcel);


    /*Modal declarations*/
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    //Get all parcel data
    const data = async () => await BaseService
        .getEntities<IParcel>('parcels/available')
        .then(data => setParcels(data));

    useEffect(() => {
        data();
    }, []);

    //Change inputs for new parcel
    const handleChange = (target: EventTarget & (HTMLInputElement | HTMLTextAreaElement)) => {
        setNewParcel({ ...newParcel, [target.name]: target.value });
    };

    //Add new parcel through api
    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        save();
        e.preventDefault();
    }
    const save = async () => await BaseService
    .createEntity(newParcel, 'parcels')
    .then(() => { history.go(0)});

    //Validate parcel data for new entity and generate warnings
    function validate() {
        if (!newParcel.destination || newParcel.destination.length !== 2) {
            return (
                <label className="text-danger"> **Lenght must be 2 letters.</label>
            )
        }
        return
    }

    //Validate parcel data for new entity generate buttons
    function validateButton() {
        if ((!newParcel.destination || newParcel.destination.length !== 2) || !newParcel.weight || !newParcel.price || !newParcel.recipient) {
            return (
                <>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="secondary">
                        Add parcel
                    </Button>
                </>
            )
        }
        return (
            <>
                <Button variant="secondary" onClick={handleClose}>
                    Close
                </Button>
                <Button variant="primary" type="submit">
                    Add parcel
                </Button>
            </>
        )
    }

    //Modals for adding new parcels
    function parcelModal() {
        return (
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Create a letterbag into this shipment</Modal.Title>
                </Modal.Header>
                <form onSubmit={handleSubmit}>
                    <Modal.Body>
                        <h5>Fill the parameters:</h5>
                        <label><h6>Destination country 2 letters(EE, FI, RU etc.): </h6></label>
                        {validate()}
                        <TextField
                            id="destination"
                            name="destination"
                            value={newParcel.destination}
                            onChange={(e) => handleChange(e.target)}
                            className="form-control"
                            type="string"
                        />
                        <label><h6>Recipient: </h6></label>
                        <TextField
                            id="recipient"
                            name="recipient"
                            value={newParcel.recipient}
                            onChange={(e) => handleChange(e.target)}
                            className="form-control"
                            type="string"
                        />
                        <label><h6>Weight: </h6></label>
                        <TextField
                            id="weight"
                            name="weight"
                            value={newParcel.weight}
                            onChange={(e) => handleChange(e.target)}
                            className="form-control"
                            type="number"
                        />
                        <label><h6>Total cost: </h6></label>
                        <TextField
                            id="price"
                            name="price"
                            value={newParcel.price}
                            onChange={(e) => handleChange(e.target)}
                            className="form-control"
                            type="number"
                        />
                    </Modal.Body>
                    <Modal.Footer>
                        {validateButton()}
                    </Modal.Footer>
                </form>
            </Modal>
        )
    }

    return (
        <Container>
            <Button variant="primary" onClick={handleShow}>
                Add a new parcel
            </Button>
            {parcelModal()}
            <Table bordered hover>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Parcel Number</th>
                        <th>Recipient</th>
                        <th>Destination</th>
                        <th>Weight</th>
                        <th>Price</th>
                    </tr>
                </thead>

                <tbody>
                    {parcels.map((parcel, index) => (
                        <tr>
                            <td>{index + 1}</td>
                            <td>{parcel.parcelNumber}</td>
                            <td>{parcel.recipient}</td>
                            <td>{parcel.destination}</td>
                            <td>{parcel.weight}</td>
                            <td>{parcel.price}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>
    );
};

export default Parcels;