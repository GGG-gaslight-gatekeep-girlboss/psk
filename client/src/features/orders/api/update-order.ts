import { z } from "zod";
import { Order } from "../types";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";
import { useMutation } from "@tanstack/react-query";

export const updateOrderInputSchema = z.object({
  orderStatus: z.string().min(1, "Order status is required."),
});

export type UpdateOrderInput = z.infer<typeof updateOrderInputSchema>;

export const updateOrder = ({
  orderId,
  data,
}: {
  orderId: string;
  data: UpdateOrderInput;
}): Promise<Order> => {
  return api.patch(`/v1/orders/${orderId}`, data);
};

type UseUpdateOrderOptions = {
  mutationConfig?: MutationConfig<typeof updateOrder>;
};

export const useUpdateOrder = ({ mutationConfig }: UseUpdateOrderOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: updateOrder,
  });
};
