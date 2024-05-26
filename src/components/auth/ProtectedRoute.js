import { Navigate, useLocation } from "react-router-dom";
import { getAuthToken } from '../../js/Utils.js';

export const ProtectedRoute = ({ children }) => {
    let location = useLocation();

    let token = getAuthToken();
    if (!token) {
        return <Navigate to="/" replace state={{ from: location }} />;
    }

    return children;
};
