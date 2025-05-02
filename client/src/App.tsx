import { createTheme, MantineProvider } from "@mantine/core";
import { Notifications } from "@mantine/notifications";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import AddEmployee from "./pages/AddEmployee";
import AddProduct from "./pages/AddProduct";
import EditEmployee from "./pages/EditEmployee";
import EditProduct from "./pages/EditProduct";
import Employees from "./pages/Employees";
import ProductManagement from "./pages/ProductManagement";
import { HomeRoute } from "./routes/home";
import LoginRoute from "./routes/login";
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
            </Route>

            <Route path="/employees" element={<Employees />} />
            <Route path="/add-employee" element={<AddEmployee />} />
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
