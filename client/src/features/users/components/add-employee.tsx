import { Button, Paper, PasswordInput, Stack, TextInput, Title } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { useNavigate } from "react-router-dom";
import { paths } from "../../../shared/config/paths";
import { showSuccessNotification } from "../../../shared/utils/notifications";
import { AddEmployeeInput, addEmployeeInputSchema, useAddEmployee } from "../api/add-employee";

export const AddEmployee = () => {
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
    validate: zodResolver(addEmployeeInputSchema),
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
    <Paper withBorder p="xl" radius="md">
      <Title order={4} mb="md">
        Add employee
      </Title>

      <form onSubmit={addEmployeeForm.onSubmit(handleAddEmployee)}>
        <Stack gap="xs">
          <TextInput
            label="First name"
            placeholder="Employee first name"
            required
            key={addEmployeeForm.key("firstName")}
            {...addEmployeeForm.getInputProps("firstName")}
          />
          <TextInput
            label="Last name"
            placeholder="Employee last name"
            required
            key={addEmployeeForm.key("lastName")}
            {...addEmployeeForm.getInputProps("lastName")}
          />
          <TextInput
            label="Phone number"
            placeholder="Employee phone number"
            required
            key={addEmployeeForm.key("phoneNumber")}
            {...addEmployeeForm.getInputProps("phoneNumber")}
          />
          <TextInput
            label="Email"
            placeholder="Employee email"
            type="email"
            required
            key={addEmployeeForm.key("email")}
            {...addEmployeeForm.getInputProps("email")}
          />
          <PasswordInput
            label="Password"
            placeholder="Employee password"
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
  );
};
