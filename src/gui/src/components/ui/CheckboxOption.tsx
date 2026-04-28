import "./CheckboxOption.css";

interface CheckboxOptionProps {
    label: string;
    checked: boolean;
    onChange: (checked: boolean) => void;
}

function CheckboxOption({
    label,
    checked,
    onChange, }: CheckboxOptionProps) {
    return (
        <label className="checkbox_option">
            <input
                type="checkbox"
                checked={checked}
                onChange={(e) => onChange(e.target.checked)}
            />
            {label}
        </label>
    );
}
export default CheckboxOption;