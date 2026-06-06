import type { ToastPosition, ToastType } from "../../components/ui/Toaster";

export interface ToastItem {
    id: string;
    type: ToastType;
    message: string;
    position: ToastPosition;
    duration: number;
}