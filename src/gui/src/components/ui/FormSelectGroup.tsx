import "../../stylesheets/form.css";

interface FormSelectGroupProps {
    label: string;
    name: string;
    options: { value: string; name: string }[];
    readonly?: boolean;
    onChange: (value: string, name: string) => void;
}

function FormSelectGroup(props: FormSelectGroupProps) {
    return <div className="form_group">
        <label htmlFor={props.name} className="form_label">{props.label}</label>
        <select
            name={props.name}
            id={props.name}
            className="form_select"
            onChange={(event) => {
                const selectedValue = event.target.value;
                const selectedOption = props.options.find(opt => opt.value === selectedValue);
                const selectedName = selectedOption?.name ?? "";
                props.onChange(selectedValue, selectedName);
            }}>
            {props.options.map((option) => (
                <option value={option.value}>{option.name}</option>))}
        </select>
    </div>
}

export default FormSelectGroup;