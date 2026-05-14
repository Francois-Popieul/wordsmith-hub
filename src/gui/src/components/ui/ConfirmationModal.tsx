import Button from "./Button";
import "./ConfirmationModal.css";

interface ConfirmationModalProps {
    title: string;
    message: string;
    onCancel: () => void;
    onConfirm: () => void;
}

function ConfirmationModal({ title, message, onCancel, onConfirm }: ConfirmationModalProps) {
    return (<>
        <div className="confirmation_modal_backdrop" onClick={onCancel} />
        <div className="confirmation_modal_container">
            <div className="confirmation_modal_container">
                <h2 className="confirmation_modal_title">{title}</h2>
                <p className="confirmation_modal_message">{message}</p>
                <div className="confirmation_modal_button_container">
                    <Button name="Annuler" variant="light" width="small" type="button" onClick={onCancel} />
                    <Button name="Confirmer" variant="red" width="small" type="button" onClick={onConfirm} />
                </div>
            </div>
        </div>
    </>
    );
}

export default ConfirmationModal;
