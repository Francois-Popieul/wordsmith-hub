import { useEffect, useRef, useState } from "react";
import "./FormMultiSelectGroup.css";
import { ArrowDownIcon, ArrowUpIcon } from "../../assets/icons/icons";

interface FormMultiSelectGroupProps {
    label: string;
    name: string;
    placeholder: string;
    options: { value: string; name: string }[];
    disabled?: boolean;
    required?: boolean;
    selected?: string[];
    error?: React.ReactNode;
    onChange: (values: string[]) => void;
}

function FormMultiSelectGroup({ label, name, placeholder, options, disabled, required = false, selected = [], error, onChange }: FormMultiSelectGroupProps) {
    const [isOpen, setIsOpen] = useState(false);
    const containerRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        function handleClickOutside(event: MouseEvent) {
            if (containerRef.current && !containerRef.current.contains(event.target as Node)) {
                setIsOpen(false);
            }
        }
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    function handleToggle(value: string) {
        const updated = selected.includes(value)
            ? selected.filter((v) => v !== value)
            : [...selected, value];
        onChange(updated);
    }

    const triggerLabel = selected.length === 0
        ? placeholder
        : options.filter((o) => selected.includes(o.value)).map((o) => o.name).join(", ");

    return (
        <div className="form_group">
            <label className="form_label">
                {label}{required && <span className="form_required_field">*</span>}
            </label>
            <div className="form_multiselect" ref={containerRef}>
                <button
                    type="button"
                    className={`form_multiselect_trigger${selected.length === 0 ? " form_multiselect_placeholder" : ""}`}
                    onClick={() => !disabled && setIsOpen((o) => !o)}
                    disabled={disabled}
                    aria-haspopup="listbox"
                    aria-expanded={isOpen}
                >
                    <span className="form_multiselect_label">{triggerLabel}</span>
                    <span className="form_multiselect_arrow">{isOpen ? <ArrowUpIcon /> : <ArrowDownIcon />}</span>
                </button>
                {isOpen && (
                    <ul className="form_multiselect_dropdown" role="listbox">
                        {options.map((option) => (
                            <li key={option.value} className="form_multiselect_option" role="option" aria-selected={selected.includes(option.value)}>
                                <label>
                                    <input
                                        type="checkbox"
                                        value={option.value}
                                        checked={selected.includes(option.value)}
                                        onChange={() => handleToggle(option.value)}
                                    />
                                    {option.name}
                                </label>
                            </li>
                        ))}
                    </ul>
                )}
                {/* hidden inputs so FormData picks up the values */}
                {selected.map((v) => (
                    <input key={v} type="hidden" name={name} value={v} />
                ))}
            </div>
            {error && <p className="form_error_message">{error}</p>}
        </div>
    );
}

export default FormMultiSelectGroup;

