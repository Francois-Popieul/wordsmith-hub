import { useState } from "react";

interface FormNumberInputGroupProps {
    name: string;
    label: string;
    value: string;
    placeholder?: string;
    required?: boolean;
    error?: string;
    onChange: (value: string) => void;
}

function FormNumberInputGroup({ name, label, value, placeholder, required = true, error, onChange }: FormNumberInputGroupProps) {
    const [displayValue, setDisplayValue] = useState(formatForDisplay(value));

    function formatForDisplay(number: string) {
        if (number === null || number === undefined) return "";
        return new Intl.NumberFormat("fr-FR", {
            minimumFractionDigits: 0,
            maximumFractionDigits: 6
        }).format(Number(number));
    }

    function parseFrench(string: string) {
        if (!string) return null;

        const normalizedString = string
            .replace(/\s/g, "")
            .replace(",", ".");

        const normalizedNumber = Number(normalizedString);
        return isNaN(normalizedNumber) ? null : normalizedNumber;
    }

    function handleChange(event: React.ChangeEvent<HTMLInputElement>) {
        const rawValue = event.target.value;

        if (!/^[0-9\s,\\.]*$/.test(rawValue)) return;

        setDisplayValue(rawValue);

        const parsedValue = parseFrench(rawValue);
        onChange(parsedValue !== null ? parsedValue.toString() : "");
    }

    function handleBlur() {
        const parsedValue = parseFrench(displayValue);
        setDisplayValue(formatForDisplay(parsedValue !== null ? parsedValue.toString() : ""));
    }
    return <div className="form_group">
        <label htmlFor={name} className="form_label">{label}</label>
        <input type="text" name={name} id={name} className="form_input" placeholder={placeholder} value={displayValue} required={required} onChange={handleChange} onBlur={handleBlur} />
        {error && <span className="form_error">{error}</span>}
    </div>
}

export default FormNumberInputGroup;