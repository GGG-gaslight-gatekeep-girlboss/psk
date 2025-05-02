import { Anchor, Button, Container, Paper, PasswordInput, Stack, Text, TextInput, Title } from "@mantine/core";
import { useForm } from "@mantine/form";
import { Link, useNavigate } from "react-router-dom";
import { RegisterInput, useRegister } from "../features/users/api/register";
import { paths } from "../shared/config/paths";
import { usePageTitle } from "../shared/hooks/use-page-title";
import { showSuccessNotification } from "../shared/utils/notifications";

export const RegisterRoute = () => {
  usePageTitle({ title: "Register" });
  const navigate = useNavigate();

  const registerForm = useForm<RegisterInput>({
    mode: "uncontrolled",
    initialValues: {
      firstName: "",
      lastName: "",
      phoneNumber: "",
      email: "",
      password: "",
    },
  });

  const registerMutation = useRegister({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Hello mate!" });
        navigate(paths.login.getHref());
      },
    },
  });

  const handleRegister = (data: RegisterInput) => {
    registerMutation.mutate({ data });
  };

  return (
    <Container size={420} my={60}>
      <Title ta="center" size="h2">
        Coffee Shop
      </Title>
      <Text ta="center" c="dimmed">
        Join us!
      </Text>

      <Paper withBorder p="xl" mt="xl" radius="md">
        <form onSubmit={registerForm.onSubmit(handleRegister)}>
          <Stack gap="xs">
            <TextInput
              label="First name"
              placeholder="Your first name"
              required
              key={registerForm.key("firstName")}
              {...registerForm.getInputProps("firstName")}
            />
            <TextInput
              label="Last name"
              placeholder="Your last name"
              required
              key={registerForm.key("lastName")}
              {...registerForm.getInputProps("lastName")}
            />
            <TextInput
              label="Phone number"
              placeholder="Your phone number"
              required
              key={registerForm.key("phoneNumber")}
              {...registerForm.getInputProps("phoneNumber")}
            />
            <TextInput
              label="Email"
              placeholder="Your email"
              type="email"
              required
              key={registerForm.key("email")}
              {...registerForm.getInputProps("email")}
            />
            <PasswordInput
              label="Password"
              placeholder="Your password"
              required
              key={registerForm.key("password")}
              {...registerForm.getInputProps("password")}
            />
          </Stack>

          <Button fullWidth mt="xl" type="submit" loading={registerMutation.isPending}>
            Register
          </Button>
        </form>
      </Paper>

      <Text c="dimmed" size="sm" ta="center" mt="lg">
        Already have an account?{" "}
        <Anchor size="sm" component={Link} to={paths.login.getHref()}>
          Log in
        </Anchor>
      </Text>
    </Container>
  );
};

export default RegisterRoute;
