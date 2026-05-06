import type React from "react";
import Button from "./Button";
import { Link } from "react-router";
import { BackArrow } from "../../assets/icons/icons";

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

function AuthFormContainer({ title, presentation, children, button_name, onSubmit, link }: FormContainerProps) {
    return <form
        onSubmit={onSubmit}
        action=""
        method="post"
        className="auth_form">
        <p className="logo_container"><img className="form_logo" src="./logo.png" alt="Logo de Wordsmith Hub" /></p>
        <h1 className="form_title">{title}</h1>
        <p className="form_presentation">{presentation}</p>
        {children}
        <div className="form_centered_container">
            <Button name={button_name} type="submit" variant="dark" width="full_width" />
            {link && <><p>{link.link_message} <Link to={link.link_destination}>{link.link_text}</Link></p></>}
            <Link to="/"><div className="back_home_container"><BackArrow /><p>Revenir à l’accueil</p></div></Link>
        </div>
    </form >
}

export default AuthFormContainer;