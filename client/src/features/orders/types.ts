export enum OrderStatus {
  Pending = "Pending",
  InProgress = "InProgress",
  Ready = "Ready",
  Completed = "Completed",
}

export type Order = {
  id: string;
  customer: OrderCustomer;
  totalPrice: number;
  pickupTime: string;
  createdAt: string;
  status: OrderStatus;
  items: OrderItem[];
};

export type PaymentIntent = {
  paymentIntentId: string;
  clientSecret: string;
};

export type OrderItem = {
  productId: string;
  productName: string;
  productPrice: number;
  quantity: number;
  totalPrice: number;
};

export type OrderCustomer = {
  id: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
};
