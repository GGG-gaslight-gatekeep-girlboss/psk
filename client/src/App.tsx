import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Login from './pages/Login';
import Register from './pages/Register';
import AddEmployee from './pages/AddEmployee';
import Employees from './pages/Employees';
import EditEmployee from './pages/EditEmployee';
import ProductManagement from './pages/ProductManagement';
import AddProduct from './pages/AddProduct';
import EditProduct from './pages/EditProduct';


function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/employees" element={<Employees />} />
        <Route path="/add-employee" element={<AddEmployee />} />
        <Route path="/edit-employee/:id" element={<EditEmployee />} />
        <Route path="/product-management" element={<ProductManagement />} />
        <Route path="/add-product" element={<AddProduct />} />
        <Route path="/edit-product" element={<EditProduct />} />


      </Routes>
    </Router>
  );
}

export default App;
