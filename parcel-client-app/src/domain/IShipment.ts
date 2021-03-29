import { Airport } from './../base/Airport';
import { IShipmentBag } from './IShipmentBag';
import { IBaseDomain } from '../base/contracts/IBaseDomain';

export interface IShipment extends IBaseDomain {
    shipmentNumber: string;
    airport: Airport;
    flightNumber: string;
    flightDate: string;
    finalized: boolean;
    shipmentBags?: IShipmentBag[];
}