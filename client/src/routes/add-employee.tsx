import { Button, Container, Paper, PasswordInput, Stack, TextInput, Title } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useNavigate } from "react-router-dom";
import { AddEmployeeInput, useAddEmployee } from "../features/users/api/add-employee";
import { paths } from "../shared/config/paths";
import { usePageTitle } from "../shared/hooks/use-page-title";
import { showSuccessNotification } from "../shared/utils/notifications";

export const AddEmployeeRoute = () => {
  usePageTitle({ title: "Add employee" });
  const navigate = useNavigate();

  const addEmployeeForm = useForm<AddEmployeeInput>({
    mode: "uncontrolled",
    initialValues: {
      firstName: "",
      lastName: "",
      phoneNumber: "",
      email: "",
      password: "",
    },
  });

  const addEmployeeMutation = useAddEmployee({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Employee added successfully" });
        navigate(paths.employees.getHref());
      },
    },
  });

  const handleAddEmployee = (data: AddEmployeeInput) => {
    addEmployeeMutation.mutate({ data });
  };

  return (
    <Container maw={600} mt="lg">
      <Paper withBorder p="xl" radius="md">
        <Title order={4} mb="md">
          Add employee
        </Title>

        <form onSubmit={addEmployeeForm.onSubmit(handleAddEmployee)}>
          <Stack gap="xs">
            <TextInput
              label="First name"
              placeholder="Your first name"
              required
              key={addEmployeeForm.key("firstName")}
              {...addEmployeeForm.getInputProps("firstName")}
            />
            <TextInput
              label="Last name"
              placeholder="Your last name"
              required
              key={addEmployeeForm.key("lastName")}
              {...addEmployeeForm.getInputProps("lastName")}
            />
            <TextInput
              label="Phone number"
              placeholder="Your phone number"
              required
              key={addEmployeeForm.key("phoneNumber")}
              {...addEmployeeForm.getInputProps("phoneNumber")}
            />
            <TextInput
              label="Email"
              placeholder="Your email"
              type="email"
              required
              key={addEmployeeForm.key("email")}
              {...addEmployeeForm.getInputProps("email")}
            />
            <PasswordInput
              label="Password"
              placeholder="Your password"
              required
              key={addEmployeeForm.key("password")}
              {...addEmployeeForm.getInputProps("password")}
            />
          </Stack>

          <Button fullWidth mt="xl" type="submit" loading={addEmployeeMutation.isPending}>
            Add employee
          </Button>
        </form>
      </Paper>
    </Container>
  );
};
