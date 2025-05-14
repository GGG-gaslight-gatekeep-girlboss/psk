import { Navigate, useParams } from "react-router-dom";
import { usePageTitle } from "../../shared/hooks/use-page-title";
import { paths } from "../../shared/config/paths";
import { Container } from "@mantine/core";
import { Order } from "../../features/orders/components/order";

export const OrderAdminRoute = () => {
  usePageTitle({ title: "Order" });
  const { orderId } = useParams();

  if (!orderId) {
    return <Navigate to={paths.home.getHref()} />;
  }

  return (
    <Container mt="lg">
      <Order orderId={orderId} />
    </Container>
  );
};
