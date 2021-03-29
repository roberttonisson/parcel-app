import { IBaseDomain } from '../../base/contracts/IBaseDomain';
export interface IUserDTO extends IBaseDomain {
    firstName: string;
    lastName: string;
    email: string;
}