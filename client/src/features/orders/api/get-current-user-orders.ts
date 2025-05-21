import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { QueryConfig } from "../../../shared/config/react-query";
import { Order } from "../types";

export const getCurrentUserOrders = (): Promise<Order[]> => {
  return api.get("/v1/orders/me");
};

export const getCurrentUserOrdersQueryOptions = () => {
  return queryOptions({
    queryKey: ["orders", "me"],
    queryFn: () => getCurrentUserOrders(),
  });
};

type UseCurrentUserOrdersOptions = {
  queryConfig?: QueryConfig<typeof getCurrentUserOrdersQueryOptions>;
};

export const useCurrentUserOrders = ({ queryConfig }: UseCurrentUserOrdersOptions) => {
  return useQuery({
    ...queryConfig,
    ...getCurrentUserOrdersQueryOptions(),
  });
};
