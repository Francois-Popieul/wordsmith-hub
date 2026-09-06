import React from "react";

type InputType = "text" | "email" | "tel" | "password" | "date";

interface FormInputGroupProps {
    label: string;
    name: string;
    type: InputType;
    placeholder?: string;
    value?: string;
    readonly?: boolean;
    required?: boolean;
    error?: React.ReactNode | undefined;
    onChange?: (value: string) => void;
    fetchChoices?: (query: string) => Promise<string[]>;
}

function FormInputGroup({ label, name, type, placeholder, value, readonly = false, required = true, error, onChange, fetchChoices }: FormInputGroupProps) {
    const [inputValue, setInputValue] = React.useState(value || '');
    const [showSelect, setShowSelect] = React.useState(false);
    const [choices, setChoices] = React.useState<string[]>([]);

    const handleInputChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = event.target.value;
        setInputValue(newValue);
        if (onChange) {
            onChange(newValue);
        }
        // Show the select if the user has typed a few characters
        setShowSelect(newValue.length > 2);

        if (newValue.length > 2 && fetchChoices) {
            const fetchedChoices = await fetchChoices(newValue);
            setChoices(fetchedChoices);
        }
    };

    const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const newValue = event.target.value;
        setInputValue(newValue);
        if (onChange) {
            onChange(newValue);
        }
        // Hide the select after an item is selected
        setShowSelect(false);
    };
    return <div className="form_group">
        <label htmlFor={name} className="form_label">{label}{required && <span className="form_required_field">*</span>}</label>
        <input type={type} name={name} id={name} placeholder={placeholder} value={inputValue} className="form_input" readOnly={readonly} required={required} onChange={handleInputChange} />
        {showSelect && choices.length > 0 && (
            <select name={`${name}-choices`} id={`${name}-choices`} className="form_choice_list" defaultValue="" size={Math.min(choices.length, 5)} onChange={handleSelectChange}>
                <option value="" disabled hidden></option>
                {choices.map((choice) => (
                    <option key={choice} value={choice}>
                        {choice}
                    </option>
                ))}
            </select>
        )}
        {error && (<p className="form_error_message">{error}</p>)}
    </div>
}

export default FormInputGroup;