import { Button, Group, Modal, Paper, Stack, Text, TextInput, Title } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useDisclosure } from "@mantine/hooks";
import { zodResolver } from "mantine-form-zod-resolver";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { paths } from "../../../shared/config/paths";
import { showSuccessNotification } from "../../../shared/utils/notifications";
import { useDeleteEmployee } from "../api/delete-employee";
import { EditEmployeeInput, editEmployeeInputSchema, useEditEmployee } from "../api/edit-employee";
import { useEmployee } from "../api/get-employee";

export const EditEmployee = (props: { employeeId: string }) => {
  const employeeQuery = useEmployee({ params: { employeeId: props.employeeId } });
  const [isDeleteModalOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);
  const navigate = useNavigate();

  const editEmployeeForm = useForm<EditEmployeeInput>({
    mode: "uncontrolled",
    initialValues: {
      firstName: "",
      lastName: "",
      phoneNumber: "",
      email: "",
    },
    validate: zodResolver(editEmployeeInputSchema),
  });

  const editEmployeeMutation = useEditEmployee({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Employee updated successfully" });
      },
    },
  });

  const deleteEmployeeMutation = useDeleteEmployee({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Employee deleted successfully" });
        navigate(paths.employees.getHref());
      },
    },
  });

  const handleDeleteEmployee = () => {
    deleteEmployeeMutation.mutate({ employeeId: props.employeeId });
  };

  useEffect(() => {
    if (employeeQuery.data) {
      editEmployeeForm.setValues(employeeQuery.data);
    }
  }, [employeeQuery.data]);

  if (employeeQuery.isLoading) {
    return <Text>Loading...</Text>;
  }

  if (!employeeQuery.data) {
    return <Text>Something went wrong...</Text>;
  }

  const handleEditEmployee = (data: EditEmployeeInput) => {
    editEmployeeMutation.mutate({ employeeId: props.employeeId, data });
  };

  return (
    <>
      <Modal opened={isDeleteModalOpen} onClose={closeDeleteModal} centered title="Delete employee">
        <Text>Are you sure you want to delete the employee?</Text>

        <Group mt="lg" justify="end">
          <Button variant="default" onClick={closeDeleteModal}>
            Cancel
          </Button>
          <Button color="red" onClick={handleDeleteEmployee}>
            Delete
          </Button>
        </Group>
      </Modal>

      <Paper withBorder p="xl" radius="md">
        <Title order={4} mb="md">
          Edit employee
        </Title>

        <form onSubmit={editEmployeeForm.onSubmit(handleEditEmployee)}>
          <Stack gap="xs">
            <TextInput
              label="First name"
              placeholder="Employee first name"
              required
              key={editEmployeeForm.key("firstName")}
              {...editEmployeeForm.getInputProps("firstName")}
            />
            <TextInput
              label="Last name"
              placeholder="Employee last name"
              required
              key={editEmployeeForm.key("lastName")}
              {...editEmployeeForm.getInputProps("lastName")}
            />
            <TextInput
              label="Phone number"
              placeholder="Employee phone number"
              required
              key={editEmployeeForm.key("phoneNumber")}
              {...editEmployeeForm.getInputProps("phoneNumber")}
            />
            <TextInput
              label="Email"
              placeholder="Employee email"
              type="email"
              required
              key={editEmployeeForm.key("email")}
              {...editEmployeeForm.getInputProps("email")}
            />
          </Stack>

          <Group mt="xl" justify="space-between">
            <Button color="red" onClick={openDeleteModal}>
              Delete
            </Button>

            <Group gap="xs">
              <Button variant="default" onClick={() => navigate(paths.employees.getHref())}>
                Cancel
              </Button>
              <Button color="teal" type="submit">
                Save
              </Button>
            </Group>
          </Group>
        </form>
      </Paper>
    </>
  );
};
