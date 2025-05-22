import { OrderCheckout } from "../features/orders/components/order-checkout";
import { usePageTitle } from "../shared/hooks/use-page-title";

export const CheckoutRoute = () => {
  usePageTitle({ title: "Checkout" });

  return <OrderCheckout />;
};
