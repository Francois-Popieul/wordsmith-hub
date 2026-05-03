import type { Address } from "../../models/Address";

export default class FreelanceDto {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    phone: string | null;
    address: Address | null;
    statusId?: number;

    public constructor(
        id: string,
        firstName: string,
        lastName: string,
        email: string,
        phone: string | null = null,
        address: Address | null = null,
        statusId?: number
    ) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phone = phone;
        this.address = address;
        this.statusId = statusId;
    }
}