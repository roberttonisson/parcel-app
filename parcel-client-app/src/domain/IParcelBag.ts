import { IBaseDomain } from '../base/contracts/IBaseDomain';
import { IBag } from './IBag';
import { IParcel } from './IParcel';

export interface IParcelBag extends IBaseDomain {
    bagId: string;
    bag?: IBag;
    parcelId: string;
    parcel?: IParcel;
}