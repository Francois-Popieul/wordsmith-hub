import "./FormModal.css"
import Button from "./Button";
import { CloseIcon } from "../../assets/icons/icons";

interface FormModalProps {
    title: string;
    presentation: string;
    children: React.ReactNode;
    validateButtonText: string;
    onCancel: () => void;
    onSubmit: (event: React.SubmitEvent<HTMLFormElement>) => void;
}

function FormModal(props: FormModalProps) {
    return (
        <div className="form_modal_backdrop">
            <form className="form_modal" onSubmit={props.onSubmit}>
                <div className="form_modal_header_container">
                    <div className="form_modal_header">
                        <h2 className="form_modal_title">{props.title}</h2>
                        <p className="form_modal_presentation">{props.presentation}</p>
                    </div>
                    <div className="form_modal_header_button_container">
                        <button className="form_modal_header_close_button" type="button" onClick={props.onCancel}><CloseIcon /></button>
                    </div>
                </div>
                <div className="form_modal_content">
                    {props.children}
                </div>
                <div className="form_modal_footer">
                    <Button name="Annuler" variant="light" width="contained" type="button" onClick={props.onCancel}></Button>
                    <Button name={props.validateButtonText} variant="blue" width="extended" type="submit"></Button>
                </div>
            </form>
        </div>
    );
}

export default FormModal;