import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { QueryConfig } from "../../../shared/config/react-query";
import { User } from "../types";

export const getEmployees = (): Promise<User[]> => {
  return api.get("/v1/users/employees");
};

export const getEmployeesQueryOptions = () => {
  return queryOptions({
    queryKey: ["employees"],
    queryFn: () => getEmployees(),
  });
};

type UseEmployeesOptions = {
  queryConfig?: QueryConfig<typeof getEmployeesQueryOptions>;
};

export const useEmployees = ({ queryConfig }: UseEmployeesOptions) => {
  return useQuery({
    ...queryConfig,
    ...getEmployeesQueryOptions(),
  });
};
