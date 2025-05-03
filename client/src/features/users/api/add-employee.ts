import { useMutation } from "@tanstack/react-query";
import { z } from "zod";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";
import { User } from "../types";

export const addEmployeeInputSchema = z.object({
  firstName: z.string().min(1, "First name is required."),
  lastName: z.string().min(1, "Last name is required."),
  phoneNumber: z.string().min(1, "Phone number is required."),
  email: z.string().min(1, "Email is required.").email("Email is invalid."),
  password: z.string().min(1, "Password is required."),
});

export type AddEmployeeInput = z.infer<typeof addEmployeeInputSchema>;

export const addEmployee = ({ data }: { data: AddEmployeeInput }): Promise<User> => {
  return api.post("/v1/users/employees", data);
};

type UseAddEmployeeOptions = {
  mutationConfig?: MutationConfig<typeof addEmployee>;
};

export const useAddEmployee = ({ mutationConfig }: UseAddEmployeeOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: addEmployee,
  });
};
