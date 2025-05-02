import { notifications } from "@mantine/notifications";

export const showErrorNotification = ({ message }: { message: string }) => {
  notifications.show({
    title: "Something went wrong...",
    message,
    color: "red",
    withBorder: true,
  });
};

export const showSuccessNotification = ({ message }: { message: string }) => {
  notifications.show({
    title: "Success",
    message,
    color: "green",
    withBorder: true,
  });
};
