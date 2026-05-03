type InputType = "text" | "email" | "tel" | "password";

interface FormInputGroupProps {
    label: string;
    name: string;
    type: InputType;
    placeholder?: string;
    value?: string;
    readonly?: boolean;
    required?: boolean;
    error?: React.ReactNode | undefined;
}

function FormInputGroup({ label, name, type, placeholder, value, readonly = false, required = true, error }: FormInputGroupProps) {
    return <div className="form_group">
        <label htmlFor={name} className="form_label">{label}{required && <span className="form_required_field">*</span>}</label>
        <input type={type} name={name} id={name} placeholder={placeholder} value={value} className="form_input" readOnly={readonly} required={required} />
        {error && (<p className="form_error">{error}</p>)}
    </div>
}

export default FormInputGroup;