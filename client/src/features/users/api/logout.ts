import { useMutation } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";

export const logout = (): Promise<void> => {
  return api.post("/v1/users/logout");
};

type UseLogoutOptions = {
  mutationConfig?: MutationConfig<typeof logout>;
};

export const useLogout = ({ mutationConfig }: UseLogoutOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: logout,
  });
};
