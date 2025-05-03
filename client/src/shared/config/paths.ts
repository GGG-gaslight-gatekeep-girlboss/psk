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
  employeeDetails: {
    path: "/employees/:employeeId",
    getHref: (employeeId: string) => `/employees/${employeeId}`,
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
