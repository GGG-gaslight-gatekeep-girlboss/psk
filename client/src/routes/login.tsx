import { Anchor, Button, Container, Paper, PasswordInput, Stack, Text, TextInput, Title } from "@mantine/core";
import { useForm } from "@mantine/form";
import { Link, useNavigate } from "react-router-dom";
import { LoginInput, useLogin } from "../features/users/api/login";
import { useUserStore } from "../features/users/user-store";
import { paths } from "../shared/config/paths";
import { usePageTitle } from "../shared/hooks/use-page-title";
import { showSuccessNotification } from "../shared/utils/notifications";

export const LoginRoute = () => {
  usePageTitle({ title: "Log in" });
  const setUser = useUserStore((state) => state.setUser);
  const navigate = useNavigate();

  const loginForm = useForm<LoginInput>({
    mode: "uncontrolled",
    initialValues: {
      email: "",
      password: "",
    },
  });

  const loginMutation = useLogin({
    mutationConfig: {
      onSuccess: (user) => {
        showSuccessNotification({ message: "Welcome back mate!" });
        setUser(user);
        navigate(paths.home.getHref());
      },
    },
  });

  const handleLogin = (data: LoginInput) => {
    loginMutation.mutate({ data });
  };

  return (
    <Container size={420} my={60}>
      <Title ta="center" size="h2">
        Coffee Shop
      </Title>
      <Text ta="center" c="dimmed">
        Welcome back!
      </Text>

      <Paper withBorder p="xl" mt="xl" radius="md">
        <form onSubmit={loginForm.onSubmit(handleLogin)}>
          <Stack gap="xs">
            <TextInput
              label="Email"
              placeholder="Your email"
              type="email"
              required
              key={loginForm.key("email")}
              {...loginForm.getInputProps("email")}
            />

            <PasswordInput
              label="Password"
              placeholder="Your password"
              required
              key={loginForm.key("password")}
              {...loginForm.getInputProps("password")}
            />
          </Stack>

          <Button fullWidth mt="xl" type="submit" loading={loginMutation.isPending}>
            Log in
          </Button>
        </form>
      </Paper>

      <Text c="dimmed" size="sm" ta="center" mt="lg">
        Don't have an account?{" "}
        <Anchor size="sm" component={Link} to={paths.register.getHref()}>
          Register
        </Anchor>
      </Text>
    </Container>
  );
};

export default LoginRoute;
