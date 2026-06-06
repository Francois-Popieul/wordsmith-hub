import { useCountries, useCurrencies, useDomainTypes, useLanguages, useLegalStatusTypes, usePricingUnits, useServices } from "./useStaticData";
import { type ReactNode } from "react";
import { StaticDataContext } from "./StaticDataContext";

export function StaticDataProvider({ children }: { children: ReactNode }) {
    const currencies = useCurrencies();
    const countries = useCountries();
    const languages = useLanguages();
    const services = useServices();
    const legalStatusTypes = useLegalStatusTypes();
    const pricingUnits = usePricingUnits();
    const domainTypes = useDomainTypes();
    const value = { currencies, countries, languages, services, legalStatusTypes, pricingUnits, domainTypes };

    return <StaticDataContext.Provider value={value}>{children}</StaticDataContext.Provider>;
}
