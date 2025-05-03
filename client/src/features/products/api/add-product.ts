import { useMutation } from "@tanstack/react-query";
import { z } from "zod";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";
import { Product } from "../types";

export const addProductInputSchema = z.object({
  name: z.string().min(1, "Name is required."),
  description: z.string().min(1, "Description is required."),
  price: z.number().nonnegative("Price must not be negative."),
  stock: z.number().nonnegative("Stock must not be negative."),
});

export type AddProductInput = z.infer<typeof addProductInputSchema>;

export const addProduct = ({ data }: { data: AddProductInput }): Promise<Product> => {
  return api.post("/v1/products", data);
};

type UseAddProductOptions = {
  mutationConfig?: MutationConfig<typeof addProduct>;
};

export const useAddProduct = ({ mutationConfig }: UseAddProductOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: addProduct,
  });
};
