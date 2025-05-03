import { createTheme, MantineProvider } from "@mantine/core";
import { Notifications } from "@mantine/notifications";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import EditEmployee from "./pages/EditEmployee";
import { AddEmployeeRoute } from "./routes/add-employee";
import { AddProductRoute } from "./routes/add-product";
import { EditProductRoute } from "./routes/edit-product";
import { EmployeeDetailsRoute } from "./routes/employee-details";
import { EmployeesRoute } from "./routes/employees";
import { HomeRoute } from "./routes/home";
import LoginRoute from "./routes/login";
import { ProductsRoute } from "./routes/products";
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

              <Route element={<ProtectedRoute allowedRoles={["BusinessOwner", "Employee"]} />}>
                <Route path={paths.products.path} element={<ProductsRoute />} />
                <Route path={paths.addProduct.path} element={<AddProductRoute />} />
                <Route path={paths.editProduct.path} element={<EditProductRoute />} />
              </Route>
            </Route>

            <Route path="/edit-employee/:id" element={<EditEmployee />} />
          </Routes>
        </Router>
      </QueryClientProvider>
    </MantineProvider>
  );
}

export default App;
