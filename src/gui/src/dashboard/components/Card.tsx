interface CardProps {
    title: string;
    value: string;
    statistics: string;
    icon: React.ReactNode;
}

function Card({ title, value, statistics, icon }: CardProps) {
    return (
        <div className="card">
            <div className="card_header">
                <h3 className="card_title">{title}</h3>
                <span className="card_icon">{icon}</span>
            </div>
            <div className="card_content">
                <p className="card_value">{value}</p>
                <p className="card_statistics">{statistics}</p>
            </div>
        </div>
    );
}

export default Card;