import { useMutation } from "@tanstack/react-query";
import { z } from "zod";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";
import { Order } from "../types";

const orderItemInputSchema = z.object({
  productId: z.string(),
  quantity: z.string(),
});

export const createOrderInputSchema = z.object({
  pickupTime: z.string(),
  items: z.array(orderItemInputSchema),
});

export type CreateOrderInput = z.infer<typeof createOrderInputSchema>;

export const createOrder = ({ data }: { data: CreateOrderInput }): Promise<Order> => {
  return api.post("/v1/orders", data);
};

type UseCreateOrderOptions = {
  mutationConfig?: MutationConfig<typeof createOrder>;
};

export const useCreateOrder = ({ mutationConfig }: UseCreateOrderOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: createOrder,
  });
};
