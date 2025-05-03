import { useMutation } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";

type DeleteProductParams = {
  productId: string;
};

export const deleteProduct = (params: DeleteProductParams): Promise<void> => {
  return api.delete(`/v1/products/${params.productId}`);
};

type UseDeleteProductOptions = {
  mutationConfig?: MutationConfig<typeof deleteProduct>;
};

export const useDeleteProduct = ({ mutationConfig }: UseDeleteProductOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: deleteProduct,
  });
};
