import { useState, useCallback, type ReactNode } from "react";
import { AuthContext } from "./AuthContext";

interface AuthProviderProps {
    children: ReactNode;
}

export function AuthProvider({ children }: AuthProviderProps) {
    const [userId, setUserId] = useState<string | null>(null);
    const [token, setToken] = useState<string | null>(null);

    const login = useCallback((newUserId: string, newToken: string) => {
        setUserId(newUserId);
        setToken(newToken);
    }, []);

    const logout = useCallback(() => {
        setUserId(null);
        setToken(null);
    }, []);

    const isAuthenticated = Boolean(userId && token);

    return (
        <AuthContext.Provider value={{ isAuthenticated, userId, token, login, logout }}>{children}</AuthContext.Provider>
    );
}