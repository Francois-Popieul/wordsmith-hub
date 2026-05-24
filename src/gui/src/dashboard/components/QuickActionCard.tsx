import "./QuickActionCard.css";

interface QuickActionCardProps {
    icon: React.ReactNode;
    title: string;
    description: string;
    onClick: () => void;
}

function QuickActionCard({ icon, title, description, onClick }: QuickActionCardProps) {
    return (
        <div className="quick_action_card" onClick={onClick}>
            <span className="quick_action_icon">{icon}</span>
            <h3 className="quick_action_title">{title}</h3>
            <p className="quick_action_description">{description}</p>
        </div>
    );
}

export default QuickActionCard;