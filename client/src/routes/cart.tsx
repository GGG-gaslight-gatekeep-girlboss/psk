import { Button, Container, Group, Paper, Table, Text, Title } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import { useCartStore } from "../features/orders/cart-store";
import { paths } from "../shared/config/paths";
import { usePageTitle } from "../shared/hooks/use-page-title";
import { formatPrice } from "../shared/utils/format";

export const CartRoute = () => {
  usePageTitle({ title: "Cart" });
  const cartItems = useCartStore((state) => state.items);
  const totalPrice = useCartStore((state) => state.totalPrice);
  const navigate = useNavigate();
  const removeFromCart = useCartStore((state) => state.removeFromCart);

  const cartItemRows = cartItems.map((item) => (
    <Table.Tr>
      <Table.Td>{item.productName}</Table.Td>
      <Table.Td>{formatPrice(item.productPrice)}</Table.Td>
      <Table.Td>{item.quantity}</Table.Td>
      <Table.Td>{formatPrice(item.totalPrice)}</Table.Td>
      <Table.Td>
        <Button color="red" size="compact-xs" variant="light" onClick={() => removeFromCart(item.productId)}>
          Remove
        </Button>
      </Table.Td>
    </Table.Tr>
  ));

  const navigateToCheckout = () => {
    navigate(paths.checkout.getHref());
  };

  return (
    <Container>
      <Title order={3} my="md">
        Cart
      </Title>

      {cartItems.length === 0 ? (
        <Text>Your cart is empty</Text>
      ) : (
        <Paper withBorder p="md">
          <Table withTableBorder withRowBorders striped my="md">
            <Table.Thead>
              <Table.Tr>
                <Table.Th>Product</Table.Th>
                <Table.Th>Price</Table.Th>
                <Table.Th>Quantity</Table.Th>
                <Table.Th>Total</Table.Th>
                <Table.Th></Table.Th>
              </Table.Tr>
            </Table.Thead>
            <Table.Tbody>{cartItemRows}</Table.Tbody>
          </Table>

          <Text>Total price: {formatPrice(totalPrice)}</Text>

          <Group justify="end">
            <Button onClick={navigateToCheckout}>Checkout</Button>
          </Group>
        </Paper>
      )}
    </Container>
  );
};
