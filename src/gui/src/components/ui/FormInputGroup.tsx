type InputType = "text" | "email" | "password";

interface FormInputGroupProps {
    label: string;
    name: string;
    type: InputType;
    placeholder?: string;
    readonly?: boolean;
    required?: boolean;
    error?: React.ReactNode | undefined;
}

function FormInputGroup({ label, name, type, placeholder, readonly = false, required = true, error }: FormInputGroupProps) {
    return <div className="form_group">
        <label htmlFor={name} className="form_label">{label}{required && <span className="form_required_field">*</span>}</label>
        <input type={type} name={name} id={name} placeholder={placeholder} className="form_input" readOnly={readonly} required={required} />
        {error && (<p className="form_error">{error}</p>)}
    </div>
}

export default FormInputGroup;