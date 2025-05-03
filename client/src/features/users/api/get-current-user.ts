import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { QueryConfig } from "../../../shared/config/react-query";
import { User } from "../types";

export const getCurrentUser = (): Promise<User> => {
  return api.get("/v1/users/me");
};

export const getCurrentUserQueryOptions = () => {
  return queryOptions({
    queryKey: ["currentUser"],
    queryFn: getCurrentUser,
  });
};

type UseCurrentUserOptions = {
  queryConfig?: QueryConfig<typeof getCurrentUserQueryOptions>;
};

export const useCurrentUser = ({ queryConfig }: UseCurrentUserOptions) => {
  return useQuery({
    ...queryConfig,
    ...getCurrentUserQueryOptions(),
  });
};
