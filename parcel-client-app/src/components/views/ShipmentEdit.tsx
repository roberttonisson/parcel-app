import React, { FormEvent, useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Accordion, Button, Card, Container, Modal, Table } from "react-bootstrap";
import { BaseService } from "../../base/BaseService";
import { IShipment } from "../../domain/IShipment";
import { IBag } from "../../domain/IBag";
import { Airport } from "../../base/Airport";
import { TextField } from "@material-ui/core";
import { ILetterBagAddDTO, IParcelBagAddDTO } from "../../domain/DTO/ILetterBagAddDTO";
import { IParcel } from "../../domain/IParcel";
import { IParcelAddDTO } from "../../domain/DTO/IParcelAddDTO";


const ShipmentEdit = () => {
    const history = useHistory();
    let { shipmentNumber } = useParams<{ shipmentNumber: string }>();
    const [shipment, setShipment] = useState({ id: '', shipmentNumber: '', airport: 0, flightNumber: '', flightDate: '', finalized: false, shipmentBags: [] } as IShipment);
    const [availableParcels, setAvailableParcels] = useState([] as IParcel[]);
    const [newLetterBag, setLetterBag] = useState({ count: 0, price: 0, weight: 0, shipmentid: '' } as ILetterBagAddDTO);
    const [newParcelBag, setParcelBag] = useState({ shipmentid: '', parcelid: '' } as IParcelBagAddDTO);
    const [editShipment, setEditShipment] = useState({ id: '', shipmentNumber: '', airport: 0, flightNumber: '', flightDate: '', finalized: false, shipmentBags: [] } as IShipment);
    const [newParcel, setParcel] = useState({ parcelId: '', bagId: '' } as IParcelAddDTO);

    const data = async () => {
        await BaseService
            .getSingle<IShipment>('shipments/nonfinalized/' + shipmentNumber)
            .then(data => {
                setShipment(data!);
                setEditShipment(data!);
                
            });
        await BaseService
            .getEntities<IParcel>('parcels/available')
            .then(data => setAvailableParcels(data!));
    }

    /*Modal declarations*/
    const [showLetterBagModal, setShowLetterBagModal] = useState(false);
    const [showParcelBagModal, setShowParcelBagModal] = useState(false);
    const [showAddParcelModal, setShowAddParcelModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showConfirmModal, setShowConfirmModal] = useState(false);

    const handleClose = () => {
        setShowLetterBagModal(false);
        setShowParcelBagModal(false);
        setShowEditModal(false);
        setShowAddParcelModal(false);
        setShowConfirmModal(false);
    };

    const handleShowLetterBag = () => {
        setShowLetterBagModal(true);
    };
    const handleShowParcelBag = () => {
        setShowParcelBagModal(true);
    };
    const handleShowEditModal = () => {
        setShowEditModal(true);
    };
    const handleShowAddParcel = () => {
        setShowAddParcelModal(true);
    };
    const handleShowConfirmModal = () => {
        setShowConfirmModal(true);
    };

    useEffect(() => {
        data();
    }, []);

    //Check if shipment is finalized
    function checkFinalization() {
        if (shipment.finalized) {
            return (<>YES</>)
        }
        return (<>NO</>)
    }
    //Check if bag is letter or parcel bag and generate html based on that
    function checkType(bag: IBag, index: number) {
        if (bag.letterBag) {
            return (
                <tr>
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
                    <tr data-toggle="collapse" data-target={"#hiddenrow" + index.toString()} className="accordion-toggle">
                        <td>{bag!.bagNumber}</td>
                        <td>Parcels</td>
                        <td>{bag.parcelBags.length}</td>
                        <td>{parseFloat(totalWeight.toFixed(3))}</td>
                        <td>{totalPrice}</td>
                        <td><div><Button variant="primary" onClick={handleShowAddParcel}>Add parcel</Button></div></td>
                    </tr>
                    <tr className="table-active accordian-body collapse" id={"hiddenrow" + index.toString()}>
                        <th></th>
                        <th>Parcel Number</th>
                        <th>Destination</th>
                        <th>Weight</th>
                        <th>Price</th>
                    </tr>
                    {bag.parcelBags.map((parcelBag) => (
                        <tr className="table-active accordian-body collapse" id={"hiddenrow" + index.toString()}>
                            <th></th>
                            <td>{parcelBag.parcel!.parcelNumber}</td>
                            <td>{parcelBag.parcel!.destination}</td>
                            <td>{parcelBag.parcel!.weight}</td>
                            <td>{parcelBag.parcel!.price}</td>
                        </tr>
                    ))}
                    {addParcelModal(bag.id)}
                </>)
        }
        return;
    }
    //Modal for confirming finalization
    function confirmModal() {
        return (
            <Modal show={showConfirmModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm finalization</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <h5>You will not be able to change bags nor the shipment once it's finalized.</h5>
                    <p></p>
                    {validateFinalization()}
                </Modal.Body>
                <Modal.Footer>
                    {validateFinalization(true)}
                </Modal.Footer>
            </Modal>
        )
    }
    //Check if all requierments are met for finalization
    function validateFinalization(buttons = false) {
        var valid = true;
        if (!shipment.flightDate || (Date.parse(shipment.flightDate) - Date.now()) < 0 || shipment.shipmentBags!.length < 1) {
            valid = false;
        }
        if (shipment.shipmentBags) {
            shipment.shipmentBags!.forEach((sp) => {
                if (!sp.bag!.letterBag && sp.bag!.parcelBags!.length < 1) {
                    valid = false;
                }

            })
        }

        if (buttons) {
            if (valid) {
                return (
                    <>
                        <Button variant="primary" onClick={handleClose}>
                            Close
                        </Button>
                        <Button variant="success" onClick={handleFinalization}>
                            Finalize
                        </Button>
                    </>
                )
            }

            return (
                <>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                        </Button>
                    <Button variant="secondary">
                        Finalize
                        </Button>
                </>
            )
        }
        if (!valid) {
            <label><h6 className="text-danger">Bags can't be empty and date can't be in the past at the moment of finalization!</h6></label>
        }
        return

    }
    //Finalize shipment throgh API
    function handleFinalization() {
        shipment.shipmentBags = [];
        shipment.finalized = true;
        finalize();
    }
    const finalize = async () => await BaseService
        .updateEntity(shipment, 'shipments')
        .then(() => { history.push("/"); });

    //Modal for modifying shipment details    
    function editShipmentModal() {
        return (
            <Modal show={showEditModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit this shipment</Modal.Title>
                </Modal.Header>
                <form onSubmit={handleEditSubmit}>
                    <Modal.Body>
                        <h5>Change the parameters:</h5>
                        {dropDown()}
                        <p></p>
                        <label><h6>Flight date and time:</h6></label>
                        {validateDate()}
                        <TextField
                            id="datetime-local"
                            value={editShipment.flightDate}
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
        )
    }

    //Dropdown for Airports
    function dropDown() {
        var values: number[] = [];
        Object.keys(Airport).map(x => values.push(Number(x)))
        return (
            <>
                <label><h6>Airport:&nbsp;&nbsp;</h6></label>
                <select value={editShipment.airport} onChange={(e) => handleAirportChange(e.target)}>
                    {values.map(y => (
                        <>{dropDownValues(y)}</>
                    ))}
                </select>


            </>)
    }
    //Values for airport dropdown
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

    //Change editShipment date if user changed it
    const handleChange = (target: EventTarget & (HTMLInputElement | HTMLTextAreaElement)) => {
        setEditShipment({ ...editShipment, flightDate: target.value });
    }
    //Change editShipment  airport if user changed it
    const handleAirportChange = (target: EventTarget & HTMLSelectElement) => {
        setEditShipment({ ...editShipment, airport: Number(target.value) })
    }
    //Save shipment changes through API
    const handleEditSubmit = (e: FormEvent<HTMLFormElement>) => {
        editShipment.shipmentBags = [];
        save();
        e.preventDefault();
    }
    const save = async () => await BaseService
        .updateEntity(editShipment, 'shipments')
        .then(() => { history.go(0); });

    //Validate if date is correct and generate HTML based on that
    function validateDate(forButton = false) {
        if (!editShipment.flightDate || (Date.parse(editShipment.flightDate) - Date.now()) < 0) {
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
                        Modify Shipment
                    </Button>
                </>
            )
        }
        return
    }

    //Modal for adding new letterbags to shipment
    function letterBagModal() {
        return (
            <Modal show={showLetterBagModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Create a letterbag into this shipment</Modal.Title>
                </Modal.Header>
                <form onSubmit={handleLetterBagSubmit}>
                    <Modal.Body>
                        <h5>Fill the parameters:</h5>
                        <label><h6>Amount of letters: </h6></label>
                        <TextField
                            id="count"
                            name="count"
                            value={newLetterBag.count}
                            onChange={(e) => handleLetterBagChange(e.target)}
                            className="form-control"
                            type="number"
                        />
                        <label><h6>Total weight: </h6></label>
                        <TextField
                            id="weight"
                            name="weight"
                            value={newLetterBag.weight}
                            onChange={(e) => handleLetterBagChange(e.target)}
                            className="form-control"
                            type="number"
                        />
                        <label><h6>Total cost: </h6></label>
                        <TextField
                            id="price"
                            name="price"
                            value={newLetterBag.price}
                            onChange={(e) => handleLetterBagChange(e.target)}
                            className="form-control"
                            type="number"
                        />
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleClose}>
                            Close
                        </Button>
                        <Button variant="primary" type="submit">
                            Add Letter Bag
                        </Button>
                    </Modal.Footer>
                </form>
            </Modal>
        )
    }

    //Modal for adding new parcel to parcelbag
    function parcelBagModal() {
        return (
            <Modal size="lg" show={showParcelBagModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Create a bag for parcels</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <h5>Select a parcel to add a new parcel bag.</h5>
                    {validateParcelBag()}<p></p>
                    <select onChange={(e) => handleParcelBagChange(e)}>
                        <option selected value={""}>------------</option>
                        {availableParcels.map(parcel => (
                            <option value={parcel.id}>{parcel.parcelNumber} | Recipient: {parcel.recipient} | {parcel.destination}  | {parcel.weight}g | {parcel.price}$</option>
                        ))}
                    </select>

                </Modal.Body>
                <Modal.Footer>
                    {validateParcelBag(true)}
                </Modal.Footer>
            </Modal>
        )
    }
    
    //
    function addParcelModal(bagId: string) {
        return (
            <Modal size="lg" show={showAddParcelModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add an available parcel to the bag</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <h5>Select a parcel to add a new parcel bag.</h5>
                    {validateAddParcel(bagId)}<p></p>
                    <select onChange={(e) => handleAddParcelChange(e)}>
                        <option selected value={""}>------------</option>
                        {availableParcels.map(parcel => (
                            <option value={parcel.id}>{parcel.parcelNumber} | Recipient: {parcel.recipient} | {parcel.destination}  | {parcel.weight}g | {parcel.price}$</option>
                        ))}
                    </select>

                </Modal.Body>
                <Modal.Footer>
                    {validateAddParcel(bagId, true)}
                </Modal.Footer>
            </Modal>
        )
    }

    function validateParcelBag(submitButton = false) {
        if (!newParcelBag.parcelid || newParcelBag.parcelid.length === 0) {
            if (submitButton) {
                return (
                    <>
                        <Button variant="secondary" onClick={handleClose}>
                            Close
                        </Button>
                        <Button variant="secondary">
                            Add Parcel Bag
                        </Button>
                    </>
                )
            }
            return (
                <label className="text-danger"> *Must choose a parcel!</label>
            )
        }
        if (submitButton) {
            return (
                <>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={(e) => handleParcelBagSubmit()}>
                        Add Parcel Bag
                    </Button>
                </>
            )
        }
        return
    }

    function validateAddParcel(bagId: string, submitButton = false) {
        if (!newParcel.parcelId || newParcel.parcelId.length === 0) {
            if (submitButton) {
                return (
                    <>
                        <Button variant="secondary" onClick={handleClose}>
                            Close
                        </Button>
                        <Button variant="secondary">
                            Add to Bag
                        </Button>
                    </>
                )
            }
            return (
                <label className="text-danger"> *Must choose a parcel!</label>
            )
        }
        if (submitButton) {
            return (
                <>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={(e) => handleAddParcelSubmit(bagId)}>
                        Add to Bag
                    </Button>
                </>
            )
        }
        return
    }

    function handleParcelBagChange(e: React.ChangeEvent<HTMLSelectElement>) {
        setParcelBag({ ...newParcelBag, parcelid: e.target.value });
    }

    function handleAddParcelChange(e: React.ChangeEvent<HTMLSelectElement>) {
        setParcel({ ...newParcel, parcelId: e.target.value });
    }

    const handleLetterBagChange = (target: EventTarget & (HTMLInputElement | HTMLTextAreaElement)) => {
        setLetterBag({ ...newLetterBag, [target.name]: target.value });
    }


    const handleLetterBagSubmit = async (e: FormEvent<HTMLFormElement>) => {
        newLetterBag.shipmentid = shipment.id;
        await BaseService
            .createEntity(newLetterBag, 'letterBags/addToShipments');
        e.preventDefault();
    }

    const postParcelBag = async () =>
        await BaseService
            .createEntity(newParcelBag, 'parcelBags/addToShipments')
            .then(() => { });

    const handleParcelBagSubmit = () => {
        newParcelBag.shipmentid = shipment.id;
        postParcelBag();
        history.go(0);
    }

    const handleAddParcelSubmit = async (bagid: string) => {
        newParcel.bagId = bagid;
        await BaseService
            .createEntity(newParcel, 'parcelBags/')
            .then(() => {history.go(0)});
    }

    return (
        <Container>
            <Accordion defaultActiveKey="0">
                <Card >
                    <Card.Header>
                        <Accordion.Toggle as={Button} variant="link" eventKey="0">
                            <Table className="table-borderless">
                                <thead>
                                    <tr>
                                        <th>Shipment number: {shipment.shipmentNumber}</th>
                                        <th>Flight number: {shipment.flightNumber}</th>
                                        <th>Airport: {Airport[shipment.airport]}</th>
                                        <th>Flight date: {new Date(shipment.flightDate).toString()}</th>
                                        <th>Finalized: {checkFinalization()}</th>
                                    </tr>
                                </thead>
                            </Table>
                        </Accordion.Toggle>
                    </Card.Header>
                    <Accordion.Collapse eventKey="0">
                        <Card.Body>
                            <button className="btn btn-primary" onClick={handleShowLetterBag}>Add letter bag</button>
                            {letterBagModal()}
                            <>&nbsp;</>
                            <button className="btn btn-primary" onClick={handleShowParcelBag}>Add parcel bag</button>
                            {parcelBagModal()}
                            <>&nbsp;</>
                            <button className="btn btn-primary" onClick={handleShowEditModal}>Edit Shipment Details</button>
                            {editShipmentModal()}
                            <>&nbsp;</>
                            <button className="btn btn-success" onClick={handleShowConfirmModal}>Finalize</button>
                            {confirmModal()}
                            <p></p>
                            <Table bordered hover>
                                <thead>
                                    <tr>
                                        <th>Bag Number</th>
                                        <th>Type</th>
                                        <th>Amount</th>
                                        <th>Total weight</th>
                                        <th>Total price</th>
                                        <th>Add Parcel</th>
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

            </Accordion>
        </Container>
    );
};

export default ShipmentEdit;