import { useCallback, useState } from "react";
import type { ToastItem } from "./ToastItem";
import type { ToastPosition, ToastType } from "../../components/ui/Toaster";
import { ToastContext } from "./ToastContext";
import Toaster from "../../components/ui/Toaster";

export function ToastProvider({ children }: { children: React.ReactNode }) {
    const [toasts, setToasts] = useState<ToastItem[]>([]);

    const addToast = useCallback((type: ToastType, message: string, position: ToastPosition, duration: number) => {
        const id = crypto.randomUUID();
        setToasts(prev => [...prev, { id, type, message, position, duration }]);
    }, []);

    const removeToast = useCallback((id: string) => {
        setToasts(prev => prev.filter(t => t.id !== id));
    }, []);

    const indexByPosition = toasts.reduce<Record<string, number>>((stackMap, toast) => {
        stackMap[toast.id] = (stackMap[toast.position] ?? 0);
        stackMap[toast.position] = (stackMap[toast.position] ?? 0) + 1;
        return stackMap;
    }, {});

    return (
        <ToastContext.Provider value={{ addToast }}>
            {children}
            {toasts.map(toast => (
                <Toaster
                    key={toast.id}
                    type={toast.type}
                    message={toast.message}
                    position={toast.position}
                    duration={toast.duration}
                    stackIndex={indexByPosition[toast.id]}
                    onClose={() => removeToast(toast.id)}
                />
            ))}
        </ToastContext.Provider>
    );
}