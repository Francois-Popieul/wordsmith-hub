import type React from "react";
import "./Button.css";
import clsx from "clsx";

interface ButtonProps {
    name: string;
    variant: "plain" | "outline" | "outline_on_dark_background";
    width?: "default" | "very_small" | "small" | "medium" | "large" | "extra_large";
    type?: "submit" | "button";
    special?: "right_side" | "left_side";
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
                `button_${props.special}`,
            )}
            onClick={props.onClick}
        >
            {props.name}{props.children}
        </button>
    );
};

export default Button;