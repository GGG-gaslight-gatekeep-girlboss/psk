import { z } from "zod";
import { api } from "../../../config/api-client";
import { User } from "../types";
import { MutationConfig } from "../../../config/react-query";
import { useMutation } from "@tanstack/react-query";

export const registerInputSchema = z.object({
  firstName: z.string().min(1, "First name is required."),
  lastName: z.string().min(1, "Last name is required."),
  phoneNumber: z.string().min(1, "Phone number is required."),
  email: z.string().min(1, "Email is required."),
  password: z.string().min(1, "Password is required."),
});

export type RegisterInput = z.infer<typeof registerInputSchema>;

export const register = ({ data }: { data: RegisterInput }): Promise<User> => {
  return api.post("/v1/users/clients", data);
};

type UseRegisterOptions = {
  mutationConfig?: MutationConfig<typeof register>;
};

export const useRegister = ({ mutationConfig }: UseRegisterOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: register,
  });
};
