import { Table, Text, Badge, Anchor } from "@mantine/core";
import {
  formatDate,
  formatPrice,
} from "../../../shared/utils/format";
import { useCurrentUserOrders } from "../api/get-current-user-orders";
import { useNavigate } from "react-router-dom";

export const MyOrders = () => {
  const ordersQuery = useCurrentUserOrders({});
  const navigate = useNavigate();

  if (ordersQuery.isLoading) {
    return <Text>Loading...</Text>;
  }

  const orders = ordersQuery.data;
  if (!orders) {
    return <Text>Something went wrong...</Text>;
  }

  const getStatusBadge = (status: string) => {
    let color = "gray";
    if (status === "Pending") color = "orange";
    if (status === "Ready") color = "green";
    return <Badge color={color}>{status}</Badge>;
  };

  const rows = orders.map((o) => (
    <Table.Tr key={o.id}>
      <Table.Td>{o.id}</Table.Td>
      <Table.Td>{formatDate(o.pickupTime)}</Table.Td>
      <Table.Td>{getStatusBadge(o.status)}</Table.Td>
      <Table.Td>{formatPrice(o.totalPrice)}</Table.Td>
      <Table.Td>
        <Anchor component="button" onClick={() => navigate(o.id)}>
          View details
        </Anchor>
      </Table.Td>
    </Table.Tr>
  ));

  return (
    <>
      <Text ta="center" size="xl" fw={500} my="md">
        My Orders
      </Text>
      <Table withTableBorder withRowBorders striped highlightOnHover>
        <Table.Thead>
          <Table.Tr>
            <Table.Th>ID</Table.Th>
            <Table.Th>Pickup time</Table.Th>
            <Table.Th>Status</Table.Th>
            <Table.Th>Total</Table.Th>
            <Table.Th></Table.Th>
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>{rows}</Table.Tbody>
      </Table>
    </>
  );
};
