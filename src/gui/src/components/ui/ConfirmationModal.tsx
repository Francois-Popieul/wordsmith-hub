import { useEffect } from "react";
import Button from "./Button";
import "./ConfirmationModal.css";

interface ConfirmationModalProps {
    title: string;
    message: string;
    onCancel: () => void;
    onConfirm: () => void;
    isVisible: boolean;
}

function ConfirmationModal({ title, message, onCancel, onConfirm, isVisible }: ConfirmationModalProps) {
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

    if (!isVisible) return null;

    return (<>
        <div className="confirmation_modal_backdrop" onClick={onCancel} />
        <div className="confirmation_modal_container">
            <h2 className="confirmation_modal_title">{title}</h2>
            <p className="confirmation_modal_message">{message}</p>
            <div className="confirmation_modal_button_container">
                <Button name="Annuler" variant="light" width="small" type="button" onClick={onCancel} />
                <Button name="Confirmer" variant="red" width="small" type="button" onClick={onConfirm} />
            </div>
        </div>
    </>
    );
}

export default ConfirmationModal;
