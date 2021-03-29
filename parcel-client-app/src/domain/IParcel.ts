import { IParcelBag } from './IParcelBag';
import { IBaseDomain } from '../base/contracts/IBaseDomain';

export interface IParcel extends IBaseDomain {
    parcelNumber: string;
    recipient: string;
    destination: string;
    weight: number;
    price: number;
    parcelBags?: IParcelBag[];
}