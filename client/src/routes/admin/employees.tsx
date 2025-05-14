import { Button, Container, Group, Title } from "@mantine/core";
import { Link } from "react-router-dom";
import { usePageTitle } from "../../shared/hooks/use-page-title";
import { paths } from "../../shared/config/paths";
import { EmployeeList } from "../../features/users/components/employee-list";

export const EmployeesAdminRoute = () => {
  usePageTitle({ title: "Employees" });

  return (
    <Container size="xl">
      <Group justify="space-between" mb="md">
        <Title order={5}>Employees</Title>
        <Button component={Link} to={paths.admin.addEmployee.getHref()}>
          Add employee
        </Button>
      </Group>

      <EmployeeList />
    </Container>
  );
};
