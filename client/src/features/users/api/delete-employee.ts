import { useMutation } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";

type DeleteEmployeeParams = {
  employeeId: string;
};

export const deleteEmployee = (params: DeleteEmployeeParams): Promise<void> => {
  return api.delete(`/v1/users/employees/${params.employeeId}`);
};

type UseDeleteEmployeeOptions = {
  mutationConfig?: MutationConfig<typeof deleteEmployee>;
};

export const useDeleteEmployee = ({ mutationConfig }: UseDeleteEmployeeOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: deleteEmployee,
  });
};
