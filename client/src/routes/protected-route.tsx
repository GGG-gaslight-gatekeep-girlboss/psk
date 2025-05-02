import { Navigate, Outlet } from "react-router-dom";
import { Role } from "../features/users/types";
import { useUserStore } from "../features/users/user-store";
import { paths } from "../shared/config/paths";

export const ProtectedRoute = ({ allowedRoles }: { allowedRoles: Role[] }) => {
  const user = useUserStore((state) => state.user);

  if (user && allowedRoles.includes(user.role)) {
    return <Outlet />;
  }

  return <Navigate to={paths.home.getHref()} replace />;
};
