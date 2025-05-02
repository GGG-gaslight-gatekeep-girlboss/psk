import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import AddEmployee from "./pages/AddEmployee";
import Employees from "./pages/Employees";
import EditEmployee from "./pages/EditEmployee";
import ProductManagement from "./pages/ProductManagement";
import AddProduct from "./pages/AddProduct";
import EditProduct from "./pages/EditProduct";
import { createTheme, MantineProvider } from "@mantine/core";
import LoginRoute from "./routes/login";
import RegisterRoute from "./routes/register";
import { paths } from "./config/paths";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Notifications } from "@mantine/notifications";

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
            <Route path="/" element={<Home />} />
            <Route path={paths.login.path} element={<LoginRoute />} />
            <Route path={paths.register.path} element={<RegisterRoute />} />
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
