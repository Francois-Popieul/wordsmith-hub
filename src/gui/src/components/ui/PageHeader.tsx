import { FaArrowLeft } from "react-icons/fa6";
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
                {props.backLink && <Button variant="light" name="{props.backLink}" width="default" type="button"><FaArrowLeft /></Button>}
                <h1 className="page_title">{props.pageTitle}</h1>
                <h2 className="page_subtitle">{props.pageSubtitle}</h2>
            </div>
            {props.button && <div className="page_header_button_container">{props.button}</div>}
        </div>
    );
}

export default PageHeader;