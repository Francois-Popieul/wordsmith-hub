import clsx from "clsx";
import "./Toaster.css"
import { useEffect } from "react";

interface ToasterProps {
    type: "success" | "error" | "information";
    message: string;
    position: "top_left" | "top_center" | "top_right" | "middle_left" | "middle_center" | "middle_right" | "bottom_left" | "bottom_center" | "bottom_right";
    duration: number;
    onClose: () => void;
}

function Toaster(props: ToasterProps) {
    useEffect(() => {
        const timer = setTimeout(() => {
            props.onClose();
        }, props.duration);
        return () => clearTimeout(timer);
    }, [props]);

    return <div>
        <p className={clsx("toast", `toast_${props.type}`, props.position)}>{props.message}<button className="toast_close" onClick={props.onClose}>×</button></p>
    </div>
}

export default Toaster;