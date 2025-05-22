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
  checkout: {
    path: "/checkout",
    getHref: () => "/checkout",
  },
  cart: {
    path: "/cart",
    getHref: () => "/cart",
  },
  orderCompleted: {
    path: "/order-completed",
    getHref: () => "/order-completed",
  },
  orders: {
    path: "/orders",
    getHref: () => "/orders",
  },
  admin: {
    employees: {
      path: "/admin/employees",
      getHref: () => "/admin/employees",
    },
    addEmployee: {
      path: "/admin/employees/add",
      getHref: () => "/admin/employees/add",
    },
    editEmployee: {
      path: "/admin/employees/:employeeId/edit",
      getHref: (employeeId: string) => `/admin/employees/${employeeId}/edit`,
    },
    products: {
      path: "/admin/products",
      getHref: () => "/admin/products",
    },
    addProduct: {
      path: "/admin/products/add",
      getHref: () => "/admin/products/add",
    },
    editProduct: {
      path: "/admin/products/:productId/edit",
      getHref: (productId: string) => `/admin/products/${productId}/edit`,
    },
    orders: {
      path: "/admin/orders",
      getHref: () => "/admin/orders",
    },
    order: {
      path: "/admin/orders/:orderId",
      getHref: (orderId: string) => `/admin/orders/${orderId}`,
    },
  },
} as const;
