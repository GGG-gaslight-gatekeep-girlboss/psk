import { useMutation } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";
import { Product } from "../types";

export const setProductImage = ({ product, image }: { product: Product; image: File }): Promise<void> => {
  const formData = new FormData();
  formData.append("image", image);

  return api.put(`/v1/products/${product.id}/image`, formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
};

type UseSetProductImageOptions = {
  mutationConfig?: MutationConfig<typeof setProductImage>;
};

export const useSetProductImage = ({ mutationConfig }: UseSetProductImageOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: setProductImage,
  });
};
