import type { Address } from "../../models/Address";
import type { Service } from "../../models/Service";
import type { TranslationLanguage } from "../../models/TranslationLanguage";

export default class ProfileDto {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    phone: string | null;
    address: Address | null;
    statusId?: number;
    sourceLanguages: TranslationLanguage[];
    targetLanguages: TranslationLanguage[];
    services: Service[];

    public constructor(
        id: string,
        firstName: string,
        lastName: string,
        email: string,
        phone: string | null = null,
        address: Address | null = null,
        statusId?: number,
        sourceLanguages: TranslationLanguage[] = [],
        targetLanguages: TranslationLanguage[] = [],
        services: Service[] = []
    ) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phone = phone;
        this.address = address;
        this.statusId = statusId;
        this.sourceLanguages = sourceLanguages;
        this.targetLanguages = targetLanguages;
        this.services = services;
    }
}