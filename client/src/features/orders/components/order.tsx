import { useNavigate } from "react-router-dom";
import { formatDate, formatPrice } from "../../../shared/utils/format";
import { useOrder } from "../api/get-order";
import { Button, Card, Group, Select, Table, Text, Title } from "@mantine/core";
import { paths } from "../../../shared/config/paths";
import {
  UpdateOrderInput,
  updateOrderInputSchema,
  useUpdateOrder,
} from "../api/update-order";
import { useForm, zodResolver } from "@mantine/form";
import { showSuccessNotification } from "../../../shared/utils/notifications";
import { OrderStatus } from "../types";
import { useEffect } from "react";

export const Order = (props: { orderId: string }) => {
  const orderQuery = useOrder({ params: { orderId: props.orderId } });
  const navigate = useNavigate();
  const updateOrderMutation = useUpdateOrder({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Order updated successfully." });
      },
    },
  });

  const updateOrderForm = useForm<UpdateOrderInput>({
    mode: "uncontrolled",
    initialValues: {
      orderStatus: "",
    },
    validate: zodResolver(updateOrderInputSchema),
  });

  useEffect(() => {
    const order = orderQuery.data;
    if (order) {
      updateOrderForm.setValues({ orderStatus: order.status });
    }
  }, [orderQuery.data]);

  if (orderQuery.isLoading) {
    return <Text>Loading...</Text>;
  }

  const order = orderQuery.data;
  if (!order) {
    return <Text>Something went wrong...</Text>;
  }

  const handleUpdateOrder = (data: UpdateOrderInput) => {
    updateOrderMutation.mutate({ orderId: props.orderId, data });
  };

  const rows = order.items.map((i) => (
    <Table.Tr>
      <Table.Td>{i.productName}</Table.Td>
      <Table.Td>{formatPrice(i.productPrice)}</Table.Td>
      <Table.Td>{i.quantity}</Table.Td>
      <Table.Td>{formatPrice(i.totalPrice)}</Table.Td>
    </Table.Tr>
  ));

  return (
    <Card withBorder p="lg">
      <Title size="h4" mb="sm">
        Order details
      </Title>

      <form onSubmit={updateOrderForm.onSubmit(handleUpdateOrder)}>
        <Group mb="sm">
          <Text>Status:</Text>
          <Select
            data={Object.values(OrderStatus)}
            key={updateOrderForm.key("orderStatus")}
            {...updateOrderForm.getInputProps("orderStatus")}
          />
        </Group>

        <Text>ID: {order.id}</Text>
        <Text>First name: {order.customer.firstName}</Text>
        <Text>Last name: {order.customer.lastName}</Text>
        <Text>Phone number: {order.customer.phoneNumber}</Text>
        <Text>Pickup time: {formatDate(order.pickupTime)}</Text>
        <Text>Created at: {formatDate(order.createdAt)}</Text>

        <Table withTableBorder withRowBorders striped mt="md">
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Product</Table.Th>
              <Table.Th>Price</Table.Th>
              <Table.Th>Quantity</Table.Th>
              <Table.Th>Total</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>{rows}</Table.Tbody>
        </Table>

        <Text mt="xs">Total price: {formatPrice(order.totalPrice)}</Text>

        <Group mt="md" justify="space-between">
          <Button
            onClick={() => navigate(paths.admin.orders.getHref())}
            variant="default"
          >
            Back
          </Button>

          <Button color="teal" type="submit">
            Save
          </Button>
        </Group>
      </form>
    </Card>
  );
};
