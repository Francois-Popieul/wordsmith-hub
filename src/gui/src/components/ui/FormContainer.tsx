import Button from "./Button";
import "./FormContainer.css";

interface FormContainerProps {
    icon?: React.ReactNode;
    title: string;
    presentation: string;
    children: React.ReactNode;
    button_name: string;
    onSubmit: (event: React.SubmitEvent<HTMLFormElement>) => void;
}

function FormContainer(props: FormContainerProps) {
    const { icon, children, title, presentation, button_name, onSubmit } = props;
    return <form className="form_container" onSubmit={onSubmit}>
        <section className="form_header">
            <div>
                <h1 className="form_title">{icon && <span className="form_icon">{icon}</span>} {title}</h1>
                <p className="form_presentation">{presentation}</p>
            </div>
            <div className="form_button">
                <Button name={button_name} variant="dark" width="medium" onClick={() => null} />
            </div>
        </section>
        <section className="form_content">
            {children}
        </section>
    </form>
}

export default FormContainer;