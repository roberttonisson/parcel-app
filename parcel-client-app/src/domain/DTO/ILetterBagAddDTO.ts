

export interface ILetterBagAddDTO {
    count: number;
    weight: number;
    price: number;
    shipmentid: string;
}

export interface IParcelBagAddDTO {
    shipmentid: string;
    parcelid: string
}