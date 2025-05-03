import { Container } from "@mantine/core";
import { Navigate, useParams } from "react-router-dom";
import { EditProduct } from "../features/products/components/edit-product";
import { paths } from "../shared/config/paths";
import { usePageTitle } from "../shared/hooks/use-page-title";

export const EditProductRoute = () => {
  usePageTitle({ title: "Edit product" });
  const { productId } = useParams();

  if (!productId) {
    return <Navigate to={paths.home.getHref()} />;
  }

  return (
    <Container maw={600} mt="lg">
      <EditProduct productId={productId} />
    </Container>
  );
};
