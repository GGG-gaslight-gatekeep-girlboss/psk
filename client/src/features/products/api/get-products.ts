import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { QueryConfig } from "../../../shared/config/react-query";
import { Product } from "../types";

export const getProducts = (): Promise<Product[]> => {
  return api.get("/v1/products");
};

export const getProductsQueryOptions = () => {
  return queryOptions({
    queryKey: ["products"],
    queryFn: () => getProducts(),
  });
};

type UseProductsOptions = {
  queryConfig?: QueryConfig<typeof getProductsQueryOptions>;
};

export const useProducts = ({ queryConfig }: UseProductsOptions) => {
  return useQuery({
    ...queryConfig,
    ...getProductsQueryOptions(),
  });
};
