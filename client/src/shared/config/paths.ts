export const paths = {
  home: {
    path: "/",
    getHref: () => "/",
  },
  login: {
    path: "/login",
    getHref: () => "/login",
  },
  register: {
    path: "/register",
    getHref: () => "/register",
  },
  employees: {
    path: "/employees",
    getHref: () => "/employees",
  },
  addEmployee: {
    path: "/employees/add",
    getHref: () => "/employees/add",
  },
  editEmployee: {
    path: "/employees/:employeeId/edit",
    getHref: (employeeId: string) => `/employees/${employeeId}/edit`,
  },
  products: {
    path: "/products",
    getHref: () => "/products",
  },
  addProduct: {
    path: "/products/add",
    getHref: () => "/products/add",
  },
  editProduct: {
    path: "/products/:productId/edit",
    getHref: (productId: string) => `/products/${productId}/edit`,
  },
} as const;
