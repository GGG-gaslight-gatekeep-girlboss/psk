import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { QueryConfig } from "../../../shared/config/react-query";
import { User } from "../types";

type GetEmployeeParams = {
  employeeId: string;
};

export const getEmployee = (params: GetEmployeeParams): Promise<User> => {
  return api.get(`/v1/users/employees/${params.employeeId}`);
};

export const getEmployeeQueryOptions = (params: GetEmployeeParams) => {
  return queryOptions({
    queryKey: ["employees", params.employeeId],
    queryFn: () => getEmployee(params),
  });
};

type UseEmployeeOptions = {
  queryConfig?: QueryConfig<typeof getEmployeeQueryOptions>;
  params: GetEmployeeParams;
};

export const useEmployee = ({ queryConfig, params }: UseEmployeeOptions) => {
  return useQuery({
    ...queryConfig,
    ...getEmployeeQueryOptions(params),
  });
};
