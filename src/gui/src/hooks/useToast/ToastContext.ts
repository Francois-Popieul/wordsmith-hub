import { createContext } from "react";
import type { ToastPosition, ToastType } from "../../components/ui/Toaster";

export interface ToastContextValue {
    addToast: (type: ToastType, message: string, position: ToastPosition, duration: number) => void;
}

export const ToastContext = createContext<ToastContextValue | null>(null);