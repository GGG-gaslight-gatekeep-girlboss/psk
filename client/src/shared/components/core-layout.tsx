import { AppShell, Avatar, Button, Group, Menu, Title } from "@mantine/core";
import { Link, Outlet } from "react-router-dom";
import { useLogout } from "../../features/users/api/logout";
import { useUserStore } from "../../features/users/user-store";
import { paths } from "../config/paths";
import { showSuccessNotification } from "../utils/notifications";

export function CoreLayout() {
  const user = useUserStore((state) => state.user);
  const setUser = useUserStore((state) => state.setUser);

  const logoutMutation = useLogout({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Goodbye mate!" });
        setUser(null);
      },
    },
  });

  const logout = () => {
    logoutMutation.mutate(undefined);
  };

  return (
    <AppShell header={{ height: 60 }} padding="md">
      <AppShell.Header>
        <Group h="100%" px="md" justify="space-between">
          <Title order={5}>Coffee Shop</Title>

          {user ? (
            <Menu withArrow width={200} shadow="md">
              <Menu.Target>
                <Avatar radius="xl" color="initials" name={`${user.firstName} ${user.lastName}`} />
              </Menu.Target>

              <Menu.Dropdown>
                <Menu.Label>
                  Hello, {user.firstName} {user.lastName}!
                </Menu.Label>
                <Menu.Item onClick={logout}>Log out</Menu.Item>
              </Menu.Dropdown>
            </Menu>
          ) : (
            <Button component={Link} to={paths.login.getHref()}>
              Log in
            </Button>
          )}
        </Group>
      </AppShell.Header>
      <AppShell.Main>
        <Outlet />
      </AppShell.Main>
    </AppShell>
  );
}
