import { useMutation } from "@tanstack/react-query";
import { z } from "zod";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";
import { User } from "../types";

export const editEmployeeInputSchema = z.object({
  firstName: z.string().optional(),
  lastName: z.string().optional(),
  phoneNumber: z.string().optional(),
  email: z.string().optional(),
});

export type EditEmployeeInput = z.infer<typeof editEmployeeInputSchema>;

export const editEmployee = ({ employeeId, data }: { employeeId: string; data: EditEmployeeInput }): Promise<User> => {
  return api.patch(`/v1/users/employees/${employeeId}`, data);
};

type UseEditEmployeeOptions = {
  mutationConfig?: MutationConfig<typeof editEmployee>;
};

export const useEditEmployee = ({ mutationConfig }: UseEditEmployeeOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: editEmployee,
  });
};
