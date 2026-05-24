import Button from "./Button";
import { PlusSignIcon } from "../../assets/icons/icons";
import "./ListContainer.css";

interface ListContainerProps {
    icon: React.ReactNode;
    title: string;
    presentation: string;
    add_button_name: string;
    no_content_message: React.ReactNode;
    no_content_button: React.ReactNode;
    children: React.ReactNode;
    list_length: number;
    onClickAdd?: () => void;
}

function ListContainer(props: ListContainerProps) {
    const { icon, title, presentation, add_button_name, children, list_length, no_content_message, no_content_button, onClickAdd } = props;
    return (
        <section className="list-container">
            <section className="list-container-header">
                <div>
                    <h2 className="list-container-title">
                        {icon && <span className="list-container-icon">{icon}</span>} {title}
                    </h2>
                    <p className="list-container-presentation">{presentation}</p>
                </div>
                <div className="list-container-button">
                    <Button name={add_button_name} variant="blue" width="default" type="button" onClick={onClickAdd}><PlusSignIcon /></Button>
                </div>
            </section>
            <section className="list-container-content">
                {list_length === 0 ? (
                    <div className="no_content_container">
                        <span className="no_content_icon">{icon}</span>
                        <span className="no_content_message">{no_content_message}</span>
                        <span className="no_content_button">{no_content_button}</span>
                    </div>
                ) : (
                    children
                )}
            </section>
        </section>
    );
}

export default ListContainer;

