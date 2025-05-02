import { z } from "zod";
import { User } from "../types";
import { api } from "../../../config/api-client";
import { MutationConfig } from "../../../config/react-query";
import { useMutation } from "@tanstack/react-query";

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
