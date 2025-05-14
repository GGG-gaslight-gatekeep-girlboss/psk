import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { QueryConfig } from "../../../shared/config/react-query";
import { Order } from "../types";

export const getOrders = (): Promise<Order[]> => {
  return api.get("/v1/orders");
};

export const getOrdersQueryOptions = () => {
  return queryOptions({
    queryKey: ["orders"],
    queryFn: () => getOrders(),
  });
};

type UseOrdersOptions = {
  queryConfig?: QueryConfig<typeof getOrdersQueryOptions>;
};

export const useOrders = ({ queryConfig }: UseOrdersOptions) => {
  return useQuery({
    ...queryConfig,
    ...getOrdersQueryOptions(),
  });
};
