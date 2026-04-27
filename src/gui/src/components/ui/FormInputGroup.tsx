import "../../stylesheets/form.css";

type InputType = "text" | "email" | "password";

interface FormInputGroupProps {
    label: string;
    name: string;
    type: InputType;
    placeholder?: string;
    readonly?: boolean;
    required?: boolean;
}

function FormInputGroup({ label, name, type, placeholder, readonly = false, required = true }: FormInputGroupProps) {
    return <div className="form_group">
        <label htmlFor={name} className="form_label">{label}</label>
        <input type={type} name={name} id={name} placeholder={placeholder} className="form_input" readOnly={readonly} required={required} />
    </div>
}

export default FormInputGroup;