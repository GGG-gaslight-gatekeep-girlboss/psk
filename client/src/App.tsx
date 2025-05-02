import { createTheme, MantineProvider } from "@mantine/core";
import { Notifications } from "@mantine/notifications";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import AddProduct from "./pages/AddProduct";
import EditEmployee from "./pages/EditEmployee";
import EditProduct from "./pages/EditProduct";
import ProductManagement from "./pages/ProductManagement";
import { AddEmployeeRoute } from "./routes/add-employee";
import { EmployeeDetailsRoute } from "./routes/employee-details";
import { EmployeesRoute } from "./routes/employees";
import { HomeRoute } from "./routes/home";
import LoginRoute from "./routes/login";
import { ProtectedRoute } from "./routes/protected-route";
import RegisterRoute from "./routes/register";
import { CoreLayout } from "./shared/components/core-layout";
import { paths } from "./shared/config/paths";

const theme = createTheme({
  primaryShade: 7,
});

const queryClient = new QueryClient();

function App() {
  return (
    <MantineProvider theme={theme}>
      <Notifications />
      <QueryClientProvider client={queryClient}>
        <Router>
          <Routes>
            <Route path={paths.login.path} element={<LoginRoute />} />
            <Route path={paths.register.path} element={<RegisterRoute />} />

            <Route element={<CoreLayout />}>
              <Route path={paths.home.path} element={<HomeRoute />} />

              <Route element={<ProtectedRoute allowedRoles={["BusinessOwner"]} />}>
                <Route path={paths.employees.path} element={<EmployeesRoute />} />
                <Route path={paths.addEmployee.path} element={<AddEmployeeRoute />} />
                <Route path={paths.employeeDetails.path} element={<EmployeeDetailsRoute />} />
              </Route>
            </Route>

            <Route path="/edit-employee/:id" element={<EditEmployee />} />
            <Route path="/product-management" element={<ProductManagement />} />
            <Route path="/add-product" element={<AddProduct />} />
            <Route path="/edit-product" element={<EditProduct />} />
          </Routes>
        </Router>
      </QueryClientProvider>
    </MantineProvider>
  );
}

export default App;
