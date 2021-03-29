import { IBag } from './IBag';
import { IBaseDomain } from '../base/contracts/IBaseDomain';

export interface ILetterBag extends IBaseDomain {
    bagId: string;
    bag?: IBag;
    count: number;
    weight: number;
    price: number;
}