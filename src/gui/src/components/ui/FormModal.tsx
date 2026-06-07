import "./FormModal.css"
import { useEffect } from "react";
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

function FormModal({ title, presentation, children, validateButtonText, onCancel, onSubmit }: FormModalProps) {
    useEffect(() => {
        function handleKeyDown(event: KeyboardEvent) {
            if (event.key === "Escape") {
                onCancel();
            }
        }

        window.addEventListener("keydown", handleKeyDown);

        return () => {
            window.removeEventListener("keydown", handleKeyDown);
        };
    }, [onCancel]);

    return (
        <div className="form_modal_backdrop">
            <form className="form_modal" onSubmit={onSubmit}>
                <div className="form_modal_header_container">
                    <div className="form_modal_header">
                        <h2 className="form_modal_title">{title}</h2>
                        <p className="form_modal_presentation">{presentation}</p>
                    </div>
                    <div className="form_modal_header_button_container">
                        <button className="form_modal_header_close_button" type="button" onClick={onCancel}><CloseIcon /></button>
                    </div>
                </div>
                <div className="form_modal_content">
                    {children}
                </div>
                <div className="form_modal_footer">
                    <Button name="Annuler" variant="light" width="contained" type="button" onClick={onCancel}></Button>
                    <Button name={validateButtonText} variant="blue" width="extended" type="submit"></Button>
                </div>
            </form>
        </div>
    );
}

export default FormModal;