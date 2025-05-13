import { Container, Group, Title } from "@mantine/core";
import { OrderList } from "../features/orders/components/order-list";
import { usePageTitle } from "../shared/hooks/use-page-title";

export const OrdersRoute = () => {
  usePageTitle({ title: "Orders" });

  return (
    <Container size="xl">
      <Group justify="space-between" mb="md">
        <Title order={5}>Orders</Title>
      </Group>

      <OrderList />
    </Container>
  );
};
