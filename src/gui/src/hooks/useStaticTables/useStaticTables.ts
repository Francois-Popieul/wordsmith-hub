import { useContext } from "react";
import { StaticDataContext } from "./StaticDataContext";

export function useStaticTables() {
    const context = useContext(StaticDataContext);
    if (!context) {
        throw new Error("useStaticTables must be used within a StaticDataProvider");
    }
    return context;
}
