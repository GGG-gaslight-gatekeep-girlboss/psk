import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { QueryConfig } from "../../../shared/config/react-query";
import { Product } from "../types";

type GetProductParams = {
  productId: string;
};

export const getProduct = (params: GetProductParams): Promise<Product> => {
  return api.get(`/v1/products/${params.productId}`);
};

export const getProductQueryOptions = (params: GetProductParams) => {
  return queryOptions({
    queryKey: ["product", params.productId],
    queryFn: () => getProduct(params),
  });
};

type UseProductOptions = {
  queryConfig?: QueryConfig<typeof getProductQueryOptions>;
  params: GetProductParams;
};

export const useProduct = ({ queryConfig, params }: UseProductOptions) => {
  return useQuery({
    ...queryConfig,
    ...getProductQueryOptions(params),
  });
};
