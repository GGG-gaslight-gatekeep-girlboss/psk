import { Button, Paper, Stack, Text, Title } from "@mantine/core";
import { useEffect } from "react";
import { Navigate, useNavigate, useSearchParams } from "react-router-dom";
import { useConfirmPayment } from "../features/orders/api/confirm-payment";
import { paths } from "../shared/config/paths";

export const OrderCompletedRoute = () => {
  const [searchParams] = useSearchParams();
  const confirmPaymentMutation = useConfirmPayment({});
  const navigate = useNavigate();

  const paymentIntentId = searchParams.get("payment_intent");

  useEffect(() => {
    if (!paymentIntentId) {
      return;
    }

    confirmPaymentMutation.mutate({ paymentIntentId });
  }, [paymentIntentId]);

  if (!paymentIntentId) {
    return <Navigate to={paths.home.getHref()} />;
  }

  const payment = confirmPaymentMutation.data;
  if (!payment) {
    return <div>Loading...</div>;
  }

  return (
    <Paper>
      <Stack gap="xs" mt="xl">
        <Title order={2} ta="center">
          Thank you!
        </Title>
        <Text ta="center">Your order has been placed.</Text>
        <Text ta="center">Order ID: {payment.orderId}</Text>
        <Button w={280} mx="auto" mt="md" onClick={() => navigate(paths.orders.getHref())}>
          View my orders
        </Button>
      </Stack>
    </Paper>
  );
};
