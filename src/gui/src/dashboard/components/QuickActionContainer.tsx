import "../components/QuickActionContainer.css";

interface QuickActionContainerProps {
    title: string;
    description: string;
    children?: React.ReactNode;
}

function QuickActionContainer(props: QuickActionContainerProps) {
    return (
        <div className="quick_actions_container">
            <div className="quick_actions_header">
                <h2 className="quick_actions_title">{props.title}</h2>
                <p className="quick_actions_description">{props.description}</p>
            </div>
            <div className="quick_actions_cards">
                {props.children}
            </div>
        </div>
    );
}

export default QuickActionContainer;