import { Badge, Button, Card, Container, Group, Table, Text } from "@mantine/core";
import { useNavigate, useParams } from "react-router-dom";
import { useOrder } from "../api/get-order";

export const MyOrderDetails = () => {
  const { orderId } = useParams<{ orderId: string }>();
  const navigate = useNavigate();

  const { data: order, isLoading, isError } = useOrder({ params: { orderId: orderId! } });

  if (isLoading) {
    return <Text>Loading...</Text>;
  }

  if (isError || !order) {
    return <Text>Order not found</Text>;
  }

  const getStatusBadge = (status: string) => {
    let color = "gray";
    if (status === "Pending") color = "orange";
    if (status === "Ready") color = "green";
    return <Badge color={color}>{status}</Badge>;
  };

  return (
    <Container mt="lg">
      <Card shadow="md" padding="lg" radius="md" withBorder>
        <Text fw={500} size="xl" mb="md">
          Order details <br /> ID: {order.id}
        </Text>

        <Group mb="md">
          <Text>Order status:</Text> {getStatusBadge(order.status)}
        </Group>

        <Text>Firstname: {order.customer.firstName}</Text>
        <Text>Lastname: {order.customer.lastName}</Text>
        <Text>Phone number: {order.customer.phoneNumber}</Text>
        <Text>Pickup time: {new Date(order.pickupTime).toLocaleString()}</Text>
        <Text>Created at: {new Date(order.createdAt).toLocaleString()}</Text>

        <Table withTableBorder striped highlightOnHover my="md">
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Product</Table.Th>
              <Table.Th>Price</Table.Th>
              <Table.Th>Quantity</Table.Th>
              <Table.Th>Total</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {order.items.map((item, index) => (
              <Table.Tr key={index}>
                <Table.Td>{item.productName}</Table.Td>
                <Table.Td>{item.productPrice.toFixed(2)} €</Table.Td>
                <Table.Td>{item.quantity}</Table.Td>
                <Table.Td>{item.totalPrice.toFixed(2)} €</Table.Td>
              </Table.Tr>
            ))}
          </Table.Tbody>
        </Table>

        <Text fw={600}>Total order price: {order.totalPrice.toFixed(2)} €</Text>

        <Button mt="md" onClick={() => navigate(-1)}>
          &lt; Back
        </Button>
      </Card>
    </Container>
  );
};
