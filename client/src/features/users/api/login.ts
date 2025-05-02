import { useMutation } from "@tanstack/react-query";
import { z } from "zod";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";
import { User } from "../types";

export const loginInputSchema = z.object({
  email: z.string().min(1, "Email is required."),
  password: z.string().min(1, "Password is required."),
});

export type LoginInput = z.infer<typeof loginInputSchema>;

export const login = ({ data }: { data: LoginInput }): Promise<User> => {
  return api.post("/v1/users/login", data);
};

type UseLoginOptions = {
  mutationConfig?: MutationConfig<typeof login>;
};

export const useLogin = ({ mutationConfig }: UseLoginOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: login,
  });
};
