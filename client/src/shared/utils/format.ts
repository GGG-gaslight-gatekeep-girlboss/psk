import { OrderStatus } from "../../features/orders/types";

export const formatPrice = (price: number) => {
  return `${price} €`;
};

export const formatOrderStatus = (status: OrderStatus) => {
  switch (status) {
    case "Pending":
      return "Pending";
    case "InProgress":
      return "In progress";
    case "Ready":
      return "Ready";
    case "Completed":
      return "Completed";
    default:
      return status;
  }
};
