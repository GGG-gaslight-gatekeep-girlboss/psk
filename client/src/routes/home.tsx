import { Button, Container, Group, Modal, NumberInput, SimpleGrid, Text } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useCartStore } from "../features/orders/cart-store";
import { useProducts } from "../features/products/api/get-products";
import { ProductCard } from "../features/products/components/product-card";
import { Product } from "../features/products/types";
import { useUserStore } from "../features/users/user-store";
import { paths } from "../shared/config/paths";
import { usePageTitle } from "../shared/hooks/use-page-title";
import { formatPrice } from "../shared/utils/format";

export const HomeRoute = () => {
  usePageTitle({ title: null });
  const productsQuery = useProducts({});
  const [modalOpened, { open: openModal, close: closeModal }] = useDisclosure(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [selectedQuantity, setSelectedQuantity] = useState<string | number>(1);
  const addToCart = useCartStore((state) => state.addToCart);
  const cartItems = useCartStore((state) => state.items);
  const currentUser = useUserStore((state) => state.user);
  const navigate = useNavigate();

  if (productsQuery.isLoading) {
    return <div>Loading...</div>;
  }

  const products = productsQuery.data;
  if (!products) {
    return <div>Something went wrong...</div>;
  }

  const handleAddToCart = () => {
    if (currentUser) {
      if (!selectedProduct) {
        return;
      }

      addToCart(selectedProduct, selectedQuantity as number);
      closeModal();
    } else {
      navigate(paths.login.getHref());
    }
  };

  return (
    <>
      <Modal opened={modalOpened} onClose={closeModal} title={selectedProduct?.name}>
        {selectedProduct && (
          <>
            <Text>{selectedProduct.description}</Text>
            <Text>{formatPrice(selectedProduct.price)}</Text>
            <NumberInput label="Quantity" mt="sm" min={1} value={selectedQuantity} onChange={setSelectedQuantity} />

            <Group justify="end" mt="lg">
              <Button variant="default" onClick={closeModal}>
                Cancel
              </Button>
              <Button onClick={handleAddToCart}>Add to cart</Button>
            </Group>
          </>
        )}
      </Modal>

      <Container size="xl">
        <SimpleGrid cols={{ base: 1, lg: 2, xl: 3 }}>
          {products.map((p) => (
            <ProductCard
              product={p}
              onClick={() => {
                setSelectedProduct(p);
                const existingItem = cartItems.find((x) => x.productId === p.id);
                if (existingItem) {
                  setSelectedQuantity(existingItem.quantity);
                } else {
                  setSelectedQuantity(1);
                }
                openModal();
              }}
            />
          ))}
        </SimpleGrid>
      </Container>
    </>
  );
};
