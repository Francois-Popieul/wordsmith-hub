import { useState, useCallback, type ReactNode } from "react";
import { AuthContext } from "./AuthContext";

interface AuthProviderProps {
    children: ReactNode;
}

export function AuthProvider({ children }: AuthProviderProps) {
    const [token, setToken] = useState<string | null>(() => localStorage.getItem("wshToken"));

    const login = useCallback((newToken: string) => {
        localStorage.setItem("wshToken", newToken);
        setToken(newToken);
    }, []);

    const logout = useCallback(() => {
        localStorage.removeItem("wshToken");
        setToken(null);
    }, []);

    return (
        <AuthContext.Provider value={{ token, login, logout }}>{children}</AuthContext.Provider>
    );
}