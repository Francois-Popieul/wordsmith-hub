import { Navigate } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth/useAuth";

interface ProtectedRouteProps {
    children: React.ReactNode;
}

function ProtectedRoute({ children }: ProtectedRouteProps) {
    const token = useAuth().token;
    if (!token) {
        return <Navigate to="/login" replace />;
    }
    return <>{children}</>;
}

export default ProtectedRoute;