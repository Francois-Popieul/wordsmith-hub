interface FormSelectGroupProps {
    label: string;
    name: string;
    placeholder: string;
    options: { value: string; name: string }[];
    disabled?: boolean;
    required?: boolean;
    selected?: string;
    onChange: (value: string, name: string) => void;
}

function FormSelectGroup({ label, name, placeholder, options, disabled, required, selected = "", onChange }: FormSelectGroupProps) {
    return <div className="form_group">
        <label htmlFor={name} className="form_label">{label}{required && <span className="form_required_field">*</span>}</label>
        <select
            id={name}
            name={name}
            className="form_select"
            value={selected}
            onChange={(event) => {
                const selectedValue = event.target.value;
                const selectedOption = options.find(opt => opt.value === selectedValue);
                const selectedName = selectedOption?.name ?? "";
                onChange(selectedValue, selectedName);
            }} required={required} disabled={disabled}>
            <option value="" disabled>{placeholder}</option>
            {options.map((option) => (
                <option key={option.value} value={option.value}>{option.name}</option>
            ))}
        </select>
    </div>
}

export default FormSelectGroup;