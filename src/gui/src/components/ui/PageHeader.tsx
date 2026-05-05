import { BackArrow } from "../../assets/icons/icons";
import Button from "./Button";
import "./PageHeader.css";

interface PageHeaderProps {
    backLink?: string;
    pageTitle: string;
    pageSubtitle: string;
    button?: React.ReactNode;
}

function PageHeader(props: PageHeaderProps) {
    return (
        <div className="page_header">
            <div>
                {props.backLink && <Button variant="light" name="{props.backLink}" width="default" type="button"><BackArrow /></Button>}
                <h1 className="page_title">{props.pageTitle}</h1>
                <p className="page_subtitle">{props.pageSubtitle}</p>
            </div>
            {props.button && <div className="page_header_button_container">{props.button}</div>}
        </div>
    );
}

export default PageHeader;