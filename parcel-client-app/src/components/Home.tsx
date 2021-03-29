import { FormEvent, useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { Accordion, Button, Card, Container, Modal, Table } from "react-bootstrap";
import { BaseService } from "../base/BaseService";
import { IBag } from "../domain/IBag";
import { IShipment } from "../domain/IShipment";
import { IShipmentAddDTO } from "../domain/DTO/IShipmentAddDTO";
import { Airport } from "../base/Airport";
import { TextField } from "@material-ui/core";


const Home = () => {
    const history = useHistory();
    const [shipments, setShipments] = useState([] as IShipment[]);
    const [newShipment, setState] = useState({airport: 0, flightDate: ''} as IShipmentAddDTO)

    /*Modal declarations*/
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    //Get all shipments through api
    const data = async () => await BaseService
        .getEntities<IShipment>('shipments/')
        .then(data => setShipments(data));

    useEffect(() => {
        data();
    }, []);

    //Change airport value for new shipment
    const handleAirportChange = (target: EventTarget & HTMLSelectElement) => {
        setState({ ...newShipment, airport: Number(target.value) })
    }

    //Redirect to single shipment spesific page where you can edit
    function routeToEdit(shipmentNumber: string) {
        let path = 'shipment/' + shipmentNumber;
        history.push(path);
    }


    //Check if shipment is finalized and generate HTML based on that
    function checkFinalization(shipment: IShipment) {
        if (shipment.finalized) {
            return (<>
                <th>Finalized: YES</th>
                <th></th>
            </>
            )
        }
        return (<>
            <th>Finalized: NO</th>
            <th><Button onClick={() => routeToEdit(shipment.shipmentNumber)} className="btn btn-primary>">EDIT</Button></th>
        </>
        )
    }
    //Check bag type to generate according HTML
    function checkType(bag: IBag, i: number) {
        if (bag.letterBag) {
            return (
                <tr key={"trr" + i.toString()}>
                    <td>{i + 1}</td>
                    <td>{bag!.bagNumber}</td>
                    <td>Letters</td>
                    <td>{bag.letterBag.count}</td>
                    <td>{bag.letterBag.weight}</td>
                    <td>{bag.letterBag.price}</td>
                </tr>)
        }
        if (bag.parcelBags) {
            var totalPrice = 0;
            var totalWeight = 0;
            bag.parcelBags.forEach(function (parcelBag) {
                totalPrice += parcelBag.parcel!.price;
                totalWeight += parcelBag.parcel!.weight;
            })
            return (
                <>
                    <tr key={"trd" + i.toString()} data-toggle="collapse" data-target={"#hiddenrow" + i.toString()} className="accordion-toggle">
                        <td>{i + 1}</td>
                        <td>{bag!.bagNumber}</td>
                        <td>Parcels</td>
                        <td>{bag.parcelBags.length}</td>
                        <td>{parseFloat(totalWeight.toFixed(3))}</td>
                        <td>{totalPrice}</td>
                    </tr>
                    <tr className="table-active accordian-body collapse" id={"hiddenrow" + i.toString()}>
                        <th></th>
                        <th>#</th>
                        <th>Parcel Number</th>
                        <th>Destination</th>
                        <th>Weight</th>
                        <th>Price</th>
                    </tr>
                    {bag.parcelBags.map((parcelBag, index) => (
                        <tr key={"tr" + index.toString()} className="table-active accordian-body collapse" id={"hiddenrow" + i.toString()}>
                            <td></td>
                            <td>{index + 1}</td>
                            <td>{parcelBag.parcel!.parcelNumber}</td>
                            <td>{parcelBag.parcel!.destination}</td>
                            <td>{parcelBag.parcel!.weight}</td>
                            <td>{parcelBag.parcel!.price}</td>
                        </tr>
                    ))}

                </>)
        }
        return;
    }
    //Dropdown for Airports
    function dropDown() {
        var values: number[] = [];
        Object.keys(Airport).map(x => values.push(Number(x)))
        return (
            <>
                <label><h6>Airport:&nbsp;&nbsp;</h6></label>
                <select value={newShipment.airport} onChange={(e) => handleAirportChange(e.target)}>
                    {values.map(y => (
                        <>{dropDownValues(y)}</>
                    ))}
                </select>


            </>)
    }
    //Values for Aiport dropdown
    function dropDownValues(y: number) {
        if (!isNaN(y)) {
            return (
                <option key={y} value={y}>
                    {Airport[y]}
                </option>
            )
        }
        return <></>;
    }
    //Change new shipment's flightDate
    const handleChange = (target: EventTarget & (HTMLInputElement | HTMLTextAreaElement)) => {
        setState({ ...newShipment, flightDate: target.value });
    }
    //Add new Shipment through API
    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        BaseService
            .createEntity(newShipment, 'shipments');

        history.go(0);
        e.preventDefault();
    }

    //Date validation for new shipment
    function validateDate(forButton = false) {
        if (!newShipment.flightDate || (Date.parse(newShipment.flightDate) - Date.now()) < 0) {
            if (forButton) {
                return (
                    <>
                        <Button variant="secondary" onClick={handleClose}>
                            Close
                        </Button>
                        <Button variant="secondary">
                            Add Shipment
                        </Button>
                    </>
                )
            }
            return (
                <label className="text-danger"> **Date must be in the future!</label>
            )
        }
        if (forButton) {
            return (
                <>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" type="submit">
                        Add Shipment
                    </Button>
                </>
            )
        }
        return
    }


    return (
        <Container>
            <Button variant="primary" onClick={handleShow}>
                Create a new shipment
            </Button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Create a shipment</Modal.Title>
                </Modal.Header>
                <form onSubmit={handleSubmit}>
                    <Modal.Body>
                        <h5>Fill the parameters:</h5>

                        {dropDown()}
                        <p></p>
                        <label><h6>Flight date and time:</h6></label>
                        {validateDate()}
                        <TextField
                            id="datetime-local"
                            value={newShipment.flightDate}
                            onChange={(e) => handleChange(e.target)}
                            className="form-control"
                            type="datetime-local"
                        />
                    </Modal.Body>
                    <Modal.Footer>
                        {validateDate(true)}
                    </Modal.Footer>
                </form>
            </Modal>
            <p></p>

            <Accordion defaultActiveKey="x">
                {shipments.map((shipment, index) => (
                    <Card >
                        <Card.Header>
                            <Accordion.Toggle as={Button} variant="link" eventKey={index.toString()}>
                                <Table className="table-borderless">
                                    <thead>
                                        <tr>
                                            <th>Shipment number: {shipment.shipmentNumber}</th>
                                            <th>Flight number: {shipment.flightNumber}</th>
                                            <th>Airport: {Airport[shipment.airport]}</th>
                                            <th>Flight date: {new Date(shipment.flightDate).toString()}</th>
                                            {checkFinalization(shipment)}
                                        </tr>
                                    </thead>
                                </Table>
                            </Accordion.Toggle>
                        </Card.Header>
                        <Accordion.Collapse eventKey={index.toString()}>
                            <Card.Body>
                            <div>Click on Parcel Bag to see parcels</div>
                                <Table bordered hover>
                                    
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Bag Number</th>
                                            <th>Type</th>
                                            <th>Amount</th>
                                            <th>Total weight</th>
                                            <th>Total price</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {shipment.shipmentBags?.map((shipmentBag, index) => (
                                            <>
                                                {checkType(shipmentBag.bag!, index)}
                                            </>
                                        ))}
                                    </tbody>
                                </Table>
                            </Card.Body>
                        </Accordion.Collapse>
                    </Card>
                ))}

            </Accordion>
        </Container>
    );
};

export default Home;