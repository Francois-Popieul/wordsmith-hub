import type React from "react";
import Button from "./Button";
import { Link } from "react-router";
import "../../stylesheets/form.css";

interface FormContainerProps {
    title: string;
    presentation: string;
    children: React.ReactNode;
    button_name: string;
    onSubmit: (event: React.SubmitEvent<HTMLFormElement>) => void;
    link?: {
        link_message: string;
        link_destination: string;
        link_text: string;
    }
}

function FormContainer(props: FormContainerProps) {
    return <form
        onSubmit={props.onSubmit}
        action=""
        method="post"
        className="form">
        <h1 className="form_title">{props.title}</h1>
        <p className="form_presentation">{props.presentation}</p>
        {props.children}
        <div className="form_centered_container">
            <Button name={props.button_name} variant="dark" width="full_width" />
            {props.link && <><p>{props.link.link_message} <Link to={props.link.link_destination}>{props.link.link_text}</Link></p></>}
        </div>
    </form >
}

export default FormContainer;