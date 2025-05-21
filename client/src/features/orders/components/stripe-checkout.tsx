import {
  Elements,
  PaymentElement,
  useElements,
  useStripe,
} from "@stripe/react-stripe-js";
import { stripePromise } from "../../../shared/config/stripe";
import { Button } from "@mantine/core";
import { useCartStore } from "../cart-store";
import { useCreateOrder } from "../api/create-order";

export const StripeCheckout = (props: { getPickupTime: () => Date | null }) => {
  const amount = useCartStore((state) => state.totalPrice);

  return (
    <Elements
      stripe={stripePromise}
      options={{ mode: "payment", amount: amount * 100, currency: "eur" }}
    >
      <CheckoutForm getPickupTime={props.getPickupTime} />
    </Elements>
  );
};

const CheckoutForm = (props: { getPickupTime: () => Date | null }) => {
  const cartItems = useCartStore((state) => state.items);
  const stripe = useStripe();
  const elements = useElements();

  const createOrderMutation = useCreateOrder({
    mutationConfig: {
      onSuccess: async (paymentIntent) => {
        if (!stripe || !elements) {
          return;
        }

        try {
          const result = await stripe.confirmPayment({
            clientSecret: paymentIntent.clientSecret,
            confirmParams: {
              return_url: "https://google.com",
            },
          });

          console.log(result);
        } catch (err) {
          console.log(err);
        }
      },
    },
  });

  const createOrder = (event: any) => {
    event.preventDefault();

    const pickupTime = props.getPickupTime();
    if (!pickupTime) {
      return;
    }

    const orderItems = cartItems.map((x) => ({
      productId: x.productId,
      quantity: x.quantity,
    }));

    createOrderMutation.mutate({
      data: {
        pickupTime: pickupTime.toISOString(),
        items: orderItems,
      },
    });
  };

  return (
    <form onSubmit={createOrder}>
      <PaymentElement />
      <Button type="submit">Pay</Button>
    </form>
  );
};
