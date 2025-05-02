import {
  Container,
  Title,
  Text,
  Anchor,
  Paper,
  TextInput,
  PasswordInput,
  Button,
  Stack,
} from "@mantine/core";
import { Link } from "react-router-dom";
import { paths } from "../config/paths";
import { usePageTitle } from "../hooks/use-page-title";
import { useForm } from "@mantine/form";
import { LoginInput, useLogin } from "../features/users/api/login";

export const LoginRoute = () => {
  usePageTitle({ title: "Log in" });

  const loginForm = useForm<LoginInput>({
    mode: "uncontrolled",
    initialValues: {
      email: "",
      password: "",
    },
  });

  const loginMutation = useLogin({});

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

          <Button
            fullWidth
            mt="xl"
            type="submit"
            loading={loginMutation.isPending}
          >
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
