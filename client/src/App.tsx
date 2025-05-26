import { useEffect } from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import { useCurrentUser } from "./features/users/api/get-current-user";
import { TokenMetadata } from "./features/users/types";
import { useUserStore } from "./features/users/user-store";
import { AddEmployeeAdminRoute } from "./routes/admin/add-employee";
import { AddProductAdminRoute } from "./routes/admin/add-product";
import { EditEmployeeAdminRoute } from "./routes/admin/edit-employee";
import { EditProductAdminRoute } from "./routes/admin/edit-product";
import { EmployeesAdminRoute } from "./routes/admin/employees";
import { OrderAdminRoute } from "./routes/admin/order";
import { OrdersAdminRoute } from "./routes/admin/orders";
import { ProductsAdminRoute } from "./routes/admin/products";
import { CartRoute } from "./routes/cart";
import { CheckoutRoute } from "./routes/checkout";
import { HomeRoute } from "./routes/home";
import LoginRoute from "./routes/login";
import { OrderCompletedRoute } from "./routes/order-completed";
import { OrdersRoute } from "./routes/orders";
import { ProtectedRoute } from "./routes/protected-route";
import RegisterRoute from "./routes/register";
import { CoreLayout } from "./shared/components/core-layout";
import { paths } from "./shared/config/paths";
import { getLocalStorageItem, removeLocalStorageItem } from "./shared/utils/local-storage";
import { useOrderNotifications } from './shared/hooks/use-order-notifications';
import { MyOrderDetails } from "./features/orders/components/my-order-details";

function App() {
  const user = useUserStore((state) => state.user);
  const setUser = useUserStore((state) => state.setUser);

  const currentUserQuery = useCurrentUser({ queryConfig: { enabled: false } });

  useEffect(() => {
    if (user !== undefined) {
      return;
    }

    const tokenMetadata = getLocalStorageItem<TokenMetadata>({
      key: "coffeeshop-access-token-metadata",
    });
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
  
  useOrderNotifications();

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

          <Route element={<ProtectedRoute allowedRoles={["Client"]} />}>
            <Route path={paths.checkout.path} element={<CheckoutRoute />} />
            <Route path={paths.cart.path} element={<CartRoute />} />
            <Route path={paths.orderCompleted.path} element={<OrderCompletedRoute />} />
            <Route path={paths.orders.path} element={<OrdersRoute />} /> 
            <Route path={paths.ordersDetails.path} element={<MyOrderDetails />} />
          </Route>

          <Route element={<ProtectedRoute allowedRoles={["BusinessOwner"]} />}>
            <Route path={paths.admin.employees.path} element={<EmployeesAdminRoute />} />
            <Route path={paths.admin.addEmployee.path} element={<AddEmployeeAdminRoute />} />
            <Route path={paths.admin.editEmployee.path} element={<EditEmployeeAdminRoute />} />
          </Route>

          <Route element={<ProtectedRoute allowedRoles={["BusinessOwner", "Employee"]} />}>
            <Route path={paths.admin.products.path} element={<ProductsAdminRoute />} />
            <Route path={paths.admin.addProduct.path} element={<AddProductAdminRoute />} />
            <Route path={paths.admin.editProduct.path} element={<EditProductAdminRoute />} />
            <Route path={paths.admin.orders.path} element={<OrdersAdminRoute />} />
            <Route path={paths.admin.order.path} element={<OrderAdminRoute />} />
          </Route>
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
