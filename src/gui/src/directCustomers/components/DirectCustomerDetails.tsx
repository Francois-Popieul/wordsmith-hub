import "./DirectCustomerDetails.css";
import * as zod from "zod";
import { type schemas } from "../../infrastructure/openApi/client";
import { BuildingIcon, CalendarIcon, InvoicesIcon, MailIcon, PhoneIcon } from "../../assets/icons/icons";
import { useCurrencies } from "../../hooks/useStaticData";

interface DirectCustomerDetailsProps {
    directCustomer: zod.infer<typeof schemas.DirectCustomerDto>;
}

function DirectCustomerDetails({ directCustomer }: DirectCustomerDetailsProps) {
    const currencies = useCurrencies();

    return <div className="direct_customer_details_container">
        <div className="details_inner_flex_container">
            <div className="details_section">
                <p className="details_section_header"><MailIcon className="details_icon" />Email</p>
                <p className="details_section_content">{directCustomer.email}</p>
            </div>
            <div className="details_section">
                <p className="details_section_header"><BuildingIcon className="details_icon" />Adresse</p>
                <p className="details_section_content">{`${directCustomer.address.streetInfo}, ${directCustomer.address.city}, ${directCustomer.address.postCode}, ${directCustomer.address.countryId}`}</p>
            </div>
        </div>
        <div className="details_inner_flex_container">
            <div className="details_section">
                <p className="details_section_header"><PhoneIcon className="details_icon" />Numéro de téléphone</p>
                <p className="details_section_content">{directCustomer.phone ? directCustomer.phone : ""}</p>
            </div>
            <div className="details_section">
                <p className="details_section_header"><CalendarIcon className="details_icon" />Début de la collaboration</p>
                <p className="details_section_content"></p>
            </div>
        </div>
        <div className="details_inner_flex_container">
            <div className="details_section">
                <p className="details_section_header"><CalendarIcon className="details_icon" />Délai de paiement</p>
                <p className="details_section_content">{directCustomer.paymentDelay}&nbsp;jours</p>
            </div>
            <div className="details_section">
                <p className="details_section_header"><InvoicesIcon className="details_icon" />Devise</p>
                <p className="details_section_content">{directCustomer.currencyId ? currencies.find(c => c.id === directCustomer.currencyId)?.name : ""} {directCustomer.currencyId ? ` (${currencies.find(c => c.id === directCustomer.currencyId)?.symbol})` : ""}</p>
            </div>
        </div>
    </div>;
}

export default DirectCustomerDetails;