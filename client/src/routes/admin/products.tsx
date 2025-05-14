import { Button, Container, Group, Title } from "@mantine/core";
import { Link } from "react-router-dom";
import { paths } from "../../shared/config/paths";
import { usePageTitle } from "../../shared/hooks/use-page-title";
import { ProductList } from "../../features/products/components/product-list";

export const ProductsAdminRoute = () => {
  usePageTitle({ title: "Products" });

  return (
    <Container size="xl">
      <Group justify="space-between" mb="md">
        <Title order={5}>Products</Title>
        <Button component={Link} to={paths.admin.addProduct.getHref()}>
          Add product
        </Button>
      </Group>

      <ProductList />
    </Container>
  );
};
