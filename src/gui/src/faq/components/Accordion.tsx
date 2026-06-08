import { useState } from "react";
import { ArrowDownIcon, ArrowUpIcon } from "../../assets/icons/icons";
import "./Accordion.css";

interface AccordionProps {
    title: string;
    children: React.ReactNode;
}

function Accordion({ title, children }: AccordionProps) {
    const [isOpen, setIsOpen] = useState(false);

    return (
        <div className="accordion">
            <button className="accordion_title" onClick={() => setIsOpen(!isOpen)}>
                {title} {isOpen ? <ArrowDownIcon /> : <ArrowUpIcon />}
            </button>
            {isOpen && <div className="accordion_content">{children}</div>}
        </div>
    );
}

export default Accordion;