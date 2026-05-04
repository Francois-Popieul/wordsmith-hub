interface FormSelectGroupProps {
    label: string;
    name: string;
    options: { value: string; name: string }[];
    readonly?: boolean;
    required?: boolean;
    selected?: string;
    onChange: (value: string, name: string) => void;
}

function FormSelectGroup({ label, name, options, readonly, required, selected = "", onChange }: FormSelectGroupProps) {
    return <div className="form_group">
        <label htmlFor={name} className="form_label">{label}</label>
        <select
            name={name}
            id={name}
            className="form_select"
            value={selected}
            onChange={(event) => {
                const selectedValue = event.target.value;
                const selectedOption = options.find(opt => opt.value === selectedValue);
                const selectedName = selectedOption?.name ?? "";
                onChange(selectedValue, selectedName);
            }} required={required} disabled={readonly}>
            {options.map((option) => (
                <option key={option.value} value={option.value}>{option.name}</option>
            ))}
        </select>
    </div>
}

export default FormSelectGroup;