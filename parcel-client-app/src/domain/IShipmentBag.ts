import { IBag } from './IBag';
import { IShipment } from './IShipment';
import { IBaseDomain } from '../base/contracts/IBaseDomain';

export interface IShipmentBag extends IBaseDomain {
    shipmentId?: string;
    shipment: IShipment;
    bagId: string;
    bag?: IBag;
}