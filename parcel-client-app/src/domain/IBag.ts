import { ILetterBag } from './ILetterBag';
import { IBaseDomain } from '../base/contracts/IBaseDomain';
import { IParcelBag } from './IParcelBag';

export interface IBag extends IBaseDomain {
    bagNumber: string;
    parcelBags?: IParcelBag[];
    letterBag?: ILetterBag
}