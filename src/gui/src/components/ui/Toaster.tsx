import clsx from "clsx";
import "./Toaster.css"
import { type CSSProperties, useEffect, useRef } from "react";

const TOAST_STACK_OFFSET = 60; // px per stacked toast

export type ToastType = "success" | "error" | "information";

export type ToastPosition =
    | "top_left" | "top_center" | "top_right"
    | "middle_left" | "middle_center" | "middle_right"
    | "bottom_left" | "bottom_center" | "bottom_right";

interface ToasterProps {
    type: ToastType;
    message: string;
    position: ToastPosition;
    duration: number;
    stackIndex: number;
    onClose: () => void;
}

function getStackStyle(position: ToastPosition, stackIndex: number): CSSProperties {
    const offset = `calc(20px + ${stackIndex * TOAST_STACK_OFFSET}px)`;
    if (position.startsWith("bottom")) {
        return { bottom: offset };
    }
    return { top: offset };
}

function Toaster(props: ToasterProps) {
    const onCloseRef = useRef(props.onClose);

    useEffect(() => {
        const timer = setTimeout(() => {
            onCloseRef.current();
        }, props.duration);
        return () => clearTimeout(timer);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return <div>
        <p
            className={clsx("toast", `toast_${props.type}`, props.position)}
            style={getStackStyle(props.position, props.stackIndex)}
        >
            {props.message}
            <button className="toast_close" onClick={props.onClose}>×</button>
        </p>
    </div>
}

export default Toaster;