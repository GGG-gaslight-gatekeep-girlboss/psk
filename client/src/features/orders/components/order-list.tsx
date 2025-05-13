import { Table, Text } from "@mantine/core";
import { formatOrderStatus, formatPrice } from "../../../shared/utils/format";
import { useOrders } from "../api/get-orders";

export const OrderList = () => {
  const ordersQuery = useOrders({});

  if (ordersQuery.isLoading) {
    return <Text>Loading...</Text>;
  }

  const orders = ordersQuery.data;
  if (!orders) {
    return <Text>Something went wrong...</Text>;
  }

  const rows = orders.map((o) => (
    <Table.Tr key={o.id}>
      <Table.Td>{o.id}</Table.Td>
      <Table.Td>{`${o.customer.firstName} ${o.customer.lastName}`}</Table.Td>
      <Table.Td>{new Date(o.pickupTime).toLocaleString()}</Table.Td>
      <Table.Td>{formatOrderStatus(o.status)}</Table.Td>
      <Table.Td>{formatPrice(o.totalPrice)}</Table.Td>
    </Table.Tr>
  ));

  return (
    <Table withTableBorder withRowBorders striped highlightOnHover>
      <Table.Thead>
        <Table.Tr>
          <Table.Th>ID</Table.Th>
          <Table.Th>Customer</Table.Th>
          <Table.Th>Pickup time</Table.Th>
          <Table.Th>Status</Table.Th>
          <Table.Th>Total</Table.Th>
        </Table.Tr>
      </Table.Thead>
      <Table.Tbody>{rows}</Table.Tbody>
    </Table>
  );
};
