export type OrderStatus = "Pending" | "InProgress" | "Ready" | "Completed";

export type Order = {
  id: string;
  customer: OrderCustomer;
  totalPrice: number;
  pickupTime: string;
  status: OrderStatus;
};

export type OrderItem = {
  productId: string;
  productName: string;
  productPrice: number;
  quantity: number;
};

export type OrderCustomer = {
  id: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
};
