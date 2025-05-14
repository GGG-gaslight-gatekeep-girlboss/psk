import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { QueryConfig } from "../../../shared/config/react-query";
import { Order } from "../types";

type GetOrderParams = {
  orderId: string;
};

export const getOrder = (params: GetOrderParams): Promise<Order> => {
  return api.get(`/v1/orders/${params.orderId}`);
};

export const getOrderQueryOptions = (params: GetOrderParams) => {
  return queryOptions({
    queryKey: ["orders", params.orderId],
    queryFn: () => getOrder(params),
  });
};

type UseOrderOptions = {
  queryConfig?: QueryConfig<typeof getOrderQueryOptions>;
  params: GetOrderParams;
};

export const useOrder = ({ queryConfig, params }: UseOrderOptions) => {
  return useQuery({
    ...queryConfig,
    ...getOrderQueryOptions(params),
  });
};
