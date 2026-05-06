import type React from "react";
import "./Button.css";
import clsx from "clsx";

interface ButtonProps {
    name: string;
    variant: "dark" | "grey" | "light" | "red" | "sidebar" | "sidebar_selected";
    width?: "default" | "small" | "medium" | "contained" | "extended" | "full_width";
    type?: "submit" | "button";
    disabled?: boolean;
    children?: React.ReactNode;
    onClick?: () => void;
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
        >
            {props.children} {props.name}
        </button>
    );
};

export default Button;