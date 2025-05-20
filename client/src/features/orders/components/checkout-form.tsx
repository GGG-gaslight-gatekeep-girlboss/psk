import { PaymentElement, useStripe } from "@stripe/react-stripe-js";

export const CheckoutForm = (props: { amount: number }) => {
  const stripe = useStripe();

  return <PaymentElement />;
};
