import type React from "react";
import "./Button.css";
import clsx from "clsx";

interface ButtonProps {
    name: string;
    variant: "dark" | "grey" | "light" | "red" | "blue" | "sidebar" | "sidebar_selected" | "action";
    width?: "default" | "small" | "medium" | "contained" | "extended" | "full_width";
    type?: "submit" | "button";
    disabled?: boolean;
    children?: React.ReactNode;
    onClick?: () => void;
    ariaLabel?: string;
}

function Button(props: ButtonProps) {
    return (
        <button
            className={clsx(
                "button",
                `button_${props.variant}`,
                `button_${props.width}`,
            )}
            type={props.type ?? "button"}
            disabled={props.disabled}
            onClick={props.onClick}
            aria-label={props.ariaLabel}
        >
            {props.children} {props.name}
        </button>
    );
};

export default Button;