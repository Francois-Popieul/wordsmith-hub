import "./CheckboxOption.css";

interface CheckboxOptionProps {
    label: string;
    name?: string;
    checked: boolean;
    required?: boolean;
    disabled?: boolean;
    error?: React.ReactNode | undefined;
    onChange: (checked: boolean) => void;
}

function CheckboxOption({
    label,
    name,
    checked,
    required = false,
    disabled = false,
    error,
    onChange, }: CheckboxOptionProps) {
    return (
        <label className="checkbox_option">
            <input
                type="checkbox"
                name={name}
                checked={checked}
                required={required}
                disabled={disabled}
                onChange={(e) => onChange(e.target.checked)}
            />
            {label}
            {error && (<p className="form_error">{error}</p>)}
        </label>
    );
}
export default CheckboxOption;