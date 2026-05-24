import "./Label.css";

interface LabelProps {
    name: string;
}

function Label({ name }: LabelProps) {
    return (
        <div className="label_container">
            <p className="label_text">{name}</p>
        </div>
    );
}

export default Label;