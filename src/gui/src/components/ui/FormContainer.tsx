import Button from "./Button";
import "./FormContainer.css";

interface FormContainerProps {
    icon?: React.ReactNode;
    title: string;
    presentation: string;
    children: React.ReactNode;

    // Button labels
    cancel_button_name: string;
    save_button_name: string;
    modify_button_name: string;

    // Form logic
    isEditing: boolean;
    modifyDisabled?: boolean;
    onModify: () => void;
    onModifyDisabled?: () => void;
    onCancel: () => void;
    onSubmit: React.ReactEventHandler<HTMLFormElement>;
}

function FormContainer({
    icon,
    children,
    title,
    presentation,
    modify_button_name,
    cancel_button_name,
    save_button_name,
    isEditing,
    modifyDisabled,
    onModify,
    onModifyDisabled,
    onCancel,
    onSubmit
}: FormContainerProps) {

    return (
        <form className="form_container" onSubmit={onSubmit}>
            <section className="form_header">
                <div>
                    <h2 className="form_title">
                        {icon && <span className="form_icon">{icon}</span>} {title}
                    </h2>
                    <p className="form_presentation">{presentation}</p>
                </div>

                <div className="form_button">
                    {isEditing ? (
                        <>
                            <Button
                                type="button"
                                name={cancel_button_name}
                                variant="light"
                                width="medium"
                                onClick={onCancel}
                            />
                            <Button
                                type="submit"
                                name={save_button_name}
                                variant="blue"
                                width="medium"
                            />
                        </>
                    ) : (
                        <span style={{ display: "inline-block" }} onClick={modifyDisabled ? onModifyDisabled : undefined}>
                            <Button
                                type="button"
                                name={modify_button_name}
                                variant="blue"
                                width="medium"
                                disabled={modifyDisabled}
                                onClick={onModify}
                            />
                        </span>
                    )}
                </div>
            </section>

            <section className="form_content">
                {children}
            </section>
        </form>
    );
}

export default FormContainer;
