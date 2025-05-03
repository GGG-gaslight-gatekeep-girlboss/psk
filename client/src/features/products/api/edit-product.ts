import { useMutation } from "@tanstack/react-query";
import { z } from "zod";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";
import { Product } from "../types";

export const editProductInputSchema = z.object({
  name: z.string().optional(),
  description: z.string().optional(),
  price: z.number().nonnegative("Price must not be negative.").optional(),
  stock: z.number().nonnegative("Stock must not be negative.").optional(),
});

export type EditProductInput = z.infer<typeof editProductInputSchema>;

export const editProduct = ({ productId, data }: { productId: string; data: EditProductInput }): Promise<Product> => {
  return api.patch(`/v1/products/${productId}`, data);
};

type UseEditProductOptions = {
  mutationConfig?: MutationConfig<typeof editProduct>;
};

export const useEditProduct = ({ mutationConfig }: UseEditProductOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: editProduct,
  });
};
