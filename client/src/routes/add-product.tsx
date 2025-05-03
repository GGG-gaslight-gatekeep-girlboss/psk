import { Container } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import { AddProduct } from "../features/products/components/add-product";
import { Product } from "../features/products/types";
import { paths } from "../shared/config/paths";
import { usePageTitle } from "../shared/hooks/use-page-title";

export const AddProductRoute = () => {
  usePageTitle({ title: "Add product" });
  const navigate = useNavigate();

  const navigateToProduct = (product: Product) => navigate(paths.editProduct.getHref(product.id));

  return (
    <Container maw={600} mt="lg">
      <AddProduct onProductAdded={navigateToProduct} />
    </Container>
  );
};
