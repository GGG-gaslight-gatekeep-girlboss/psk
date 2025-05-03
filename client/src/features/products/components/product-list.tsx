import { SimpleGrid, Text } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import { paths } from "../../../shared/config/paths";
import { useProducts } from "../api/get-products";
import { Product } from "../types";
import { ProductCard } from "./product-card";

export const ProductList = () => {
  const productsQuery = useProducts({});
  const navigate = useNavigate();

  const navigateToProduct = (product: Product) => navigate(paths.editProduct.getHref(product.id));

  if (productsQuery.isLoading) {
    return <Text>Loading...</Text>;
  }

  if (!productsQuery.data) {
    return <Text>Something went wrong...</Text>;
  }

  return (
    <SimpleGrid cols={{ base: 1, lg: 2, xl: 3 }}>
      {productsQuery.data.map((p) => (
        <ProductCard product={p} onClick={navigateToProduct} key={p.id} />
      ))}
    </SimpleGrid>
  );
};
