import { useEffect } from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import { useCurrentUser } from "./features/users/api/get-current-user";
import { TokenMetadata } from "./features/users/types";
import { useUserStore } from "./features/users/user-store";
import { AddEmployeeRoute } from "./routes/add-employee";
import { AddProductRoute } from "./routes/add-product";
import { EditEmployeeRoute } from "./routes/edit-employee";
import { EditProductRoute } from "./routes/edit-product";
import { EmployeesRoute } from "./routes/employees";
import { HomeRoute } from "./routes/home";
import LoginRoute from "./routes/login";
import { OrdersRoute } from "./routes/orders";
import { ProductsRoute } from "./routes/products";
import { ProtectedRoute } from "./routes/protected-route";
import RegisterRoute from "./routes/register";
import { CoreLayout } from "./shared/components/core-layout";
import { paths } from "./shared/config/paths";
import { getLocalStorageItem, removeLocalStorageItem } from "./shared/utils/local-storage";

function App() {
  const user = useUserStore((state) => state.user);
  const setUser = useUserStore((state) => state.setUser);

  const currentUserQuery = useCurrentUser({ queryConfig: { enabled: false } });

  useEffect(() => {
    const tokenMetadata = getLocalStorageItem<TokenMetadata>({ key: "coffeeshop-access-token-metadata" });
    if (!tokenMetadata) {
      setUser(null);
      return;
    }

    const expiresAt = new Date(tokenMetadata.expiresAt);
    const now = new Date();

    if (expiresAt.getTime() > now.getTime()) {
      currentUserQuery.refetch();
    } else {
      removeLocalStorageItem({ key: "coffeeshop-access-token-metadata" });
      setUser(null);
    }
  }, [setUser, currentUserQuery]);

  useEffect(() => {
    if (currentUserQuery.isError) {
      setUser(null);
    }

    if (currentUserQuery.data) {
      setUser(currentUserQuery.data);
    }
  }, [setUser, currentUserQuery.isError, currentUserQuery.data]);

  if (user === undefined) {
    return null;
  }

  return (
    <Router>
      <Routes>
        <Route path={paths.login.path} element={<LoginRoute />} />
        <Route path={paths.register.path} element={<RegisterRoute />} />

        <Route element={<CoreLayout />}>
          <Route path={paths.home.path} element={<HomeRoute />} />

          <Route element={<ProtectedRoute allowedRoles={["BusinessOwner"]} />}>
            <Route path={paths.employees.path} element={<EmployeesRoute />} />
            <Route path={paths.addEmployee.path} element={<AddEmployeeRoute />} />
            <Route path={paths.editEmployee.path} element={<EditEmployeeRoute />} />
          </Route>

          <Route element={<ProtectedRoute allowedRoles={["BusinessOwner", "Employee"]} />}>
            <Route path={paths.products.path} element={<ProductsRoute />} />
            <Route path={paths.addProduct.path} element={<AddProductRoute />} />
            <Route path={paths.editProduct.path} element={<EditProductRoute />} />
            <Route path={paths.orders.path} element={<OrdersRoute />} />
          </Route>
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
