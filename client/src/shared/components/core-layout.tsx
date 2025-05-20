import { ActionIcon, Anchor, AppShell, Avatar, Button, Group, Menu, NavLink, Text } from "@mantine/core";
import { IconBasket } from "@tabler/icons-react";
import { Link, Outlet, useNavigate } from "react-router-dom";
import { useCartStore } from "../../features/orders/cart-store";
import { useLogout } from "../../features/users/api/logout";
import { useUserStore } from "../../features/users/user-store";
import { paths } from "../config/paths";
import { removeLocalStorageItem } from "../utils/local-storage";
import { showSuccessNotification } from "../utils/notifications";

export function CoreLayout() {
  const user = useUserStore((state) => state.user);
  const setUser = useUserStore((state) => state.setUser);
  const showNavbar = user?.role === "Employee" || user?.role === "BusinessOwner";
  const cartItemCount = useCartStore((state) => state.items.reduce((acc, item) => acc + item.quantity, 0));
  const navigate = useNavigate();

  const logoutMutation = useLogout({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Goodbye mate!" });
        setUser(null);
        removeLocalStorageItem({ key: "coffeeshop-access-token-metadata" });
      },
    },
  });

  const logout = () => {
    logoutMutation.mutate(undefined);
  };

  const navigateToCart = () => {
    navigate(paths.cart.getHref());
  };

  return (
    <AppShell
      header={{ height: 60 }}
      navbar={{
        width: showNavbar ? 300 : 0,
        breakpoint: "sm",
      }}
      padding="md"
    >
      <AppShell.Header>
        <Group h="100%" px="md" justify="space-between">
          <Anchor component={Link} to={paths.home.getHref()} fw={600}>
            Coffee Shop
          </Anchor>

          {user ? (
            <Group>
              <div style={{ position: "relative" }}>
                <ActionIcon size="lg" variant="light" radius="xl" onClick={navigateToCart}>
                  <IconBasket style={{ width: "75%", height: "75%" }} stroke={1.5} />
                </ActionIcon>
                <Text size="sm" fw={600} pos="absolute" bottom={22} left={26} c="blue">
                  {cartItemCount}
                </Text>
              </div>

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
            </Group>
          ) : (
            <Button component={Link} to={paths.login.getHref()}>
              Log in
            </Button>
          )}
        </Group>
      </AppShell.Header>

      {user && showNavbar && (
        <AppShell.Navbar>
          {user.role === "BusinessOwner" && (
            <NavLink label="Employee management" component={Link} to={paths.admin.employees.getHref()} />
          )}

          <NavLink label="Product management" component={Link} to={paths.admin.products.getHref()} />
          <NavLink label="Order management" component={Link} to={paths.admin.orders.getHref()} />
        </AppShell.Navbar>
      )}

      <AppShell.Main>
        <Outlet />
      </AppShell.Main>
    </AppShell>
  );
}
