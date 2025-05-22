import { useMutation } from "@tanstack/react-query";
import { api } from "../../../shared/config/api-client";
import { MutationConfig } from "../../../shared/config/react-query";
import { CardPayment } from "../types";

export const confirmPayment = ({ paymentIntentId }: { paymentIntentId: string }): Promise<CardPayment> => {
  return api.post(`/v1/payments/card/${paymentIntentId}/confirm`);
};

type UseConfirmPaymentOptions = {
  mutationConfig?: MutationConfig<typeof confirmPayment>;
};

export const useConfirmPayment = ({ mutationConfig }: UseConfirmPaymentOptions) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: confirmPayment,
  });
};
