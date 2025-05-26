import { Container, Table, Title } from "@mantine/core";
import { useCurrentUserOrders } from "../features/orders/api/get-current-user-orders";
import { formatDate, formatOrderStatus, formatPrice } from "../shared/utils/format";
import { Link } from "react-router-dom"; 

export const OrdersRoute = () => {
  const ordersQuery = useCurrentUserOrders({});

  if (ordersQuery.isLoading) {
    return <div>Loading...</div>;
  }

  const orders = ordersQuery.data;
  if (!orders) {
    return <div>Something went wrong...</div>;
  }

  const rows = orders.map((o) => (
    <Table.Tr key={o.id}>
      <Table.Td>{o.id}</Table.Td>
      <Table.Td>{formatDate(o.pickupTime)}</Table.Td>
      <Table.Td>{formatOrderStatus(o.status)}</Table.Td>
      <Table.Td>{formatPrice(o.totalPrice)}</Table.Td>
      <Table.Td>
        <Link to={`/orders/${o.id}`}>View Details</Link> {/* Pridedame nuorodą */}
      </Table.Td>
    </Table.Tr>
  ));

  return (
    <Container>
      <Title order={3} my="md">
        My orders
      </Title>

      <Table withTableBorder withRowBorders striped highlightOnHover>
        <Table.Thead>
          <Table.Tr>
            <Table.Th>ID</Table.Th>
            <Table.Th>Pickup time</Table.Th>
            <Table.Th>Status</Table.Th>
            <Table.Th>Total</Table.Th>
            <Table.Th>Action</Table.Th> {/* Naujas stulpelis */}
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>{rows}</Table.Tbody>
      </Table>
    </Container>
  );
};
