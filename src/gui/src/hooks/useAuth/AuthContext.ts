import { createContext } from "react";

export interface AuthContextValue {
    isAuthenticated: boolean;
    userId: string | null;
    token: string | null;
    login: (userId: string, token: string) => void;
    logout: () => void;
}

export const AuthContext = createContext<AuthContextValue | undefined>(undefined);