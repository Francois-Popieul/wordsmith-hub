import { createContext } from "react";
import * as zod from "zod";
import type { schemas } from "../../infrastructure/openApi/client";

export interface StaticDataContextValue {
    currencies: zod.infer<typeof schemas.Currency>[];
    countries: zod.infer<typeof schemas.Country>[];
    languages: zod.infer<typeof schemas.TranslationLanguage>[];
    services: zod.infer<typeof schemas.Service>[];
    legalStatusTypes: zod.infer<typeof schemas.LegalStatusType>[];
    pricingUnits: zod.infer<typeof schemas.PricingUnit>[];
    domainTypes: zod.infer<typeof schemas.DomainType>[];
}

export const StaticDataContext = createContext<StaticDataContextValue | null>(null);
