import { Button, Group } from "@mantine/core";
import { Elements, PaymentElement, useElements, useStripe } from "@stripe/react-stripe-js";
import { useState } from "react";
import { paths } from "../../../shared/config/paths";
import { stripePromise } from "../../../shared/config/stripe";
import { useCreateOrder } from "../api/create-order";
import { useCartStore } from "../cart-store";

export const StripeCheckout = (props: { getPickupTime: () => Date | null; onBackClick: () => void }) => {
  const amount = useCartStore((state) => state.totalPrice);

  return (
    <Elements
      stripe={stripePromise}
      options={{ mode: "payment", amount: Math.round(amount * 100), currency: "eur", paymentMethodTypes: ["card"] }}
    >
      <CheckoutForm getPickupTime={props.getPickupTime} onBackClick={props.onBackClick} />
    </Elements>
  );
};

const CheckoutForm = (props: { getPickupTime: () => Date | null; onBackClick: () => void }) => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
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
          await stripe.confirmPayment({
            elements: elements,
            clientSecret: paymentIntent.clientSecret,
            confirmParams: {
              return_url: `http://localhost:3000${paths.orderCompleted.getHref()}`,
            },
          });
        } catch (err) {
          console.log(err);
        } finally {
          setIsLoading(false);
        }
      },
      onError: () => {
        setIsLoading(false);
      },
    },
  });

  const createOrder = async (event: { preventDefault: () => void }) => {
    event.preventDefault();

    const pickupTime = props.getPickupTime();
    if (!pickupTime || !elements) {
      return;
    }

    setIsLoading(true);

    const orderItems = cartItems.map((x) => ({
      productId: x.productId,
      quantity: x.quantity,
    }));

    await elements.submit();

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

      <Group justify="space-between" mt="lg">
        <Button variant="default" onClick={props.onBackClick}>
          Back
        </Button>
        <Button type="submit" loading={isLoading}>
          Pay
        </Button>
      </Group>
    </form>
  );
};
