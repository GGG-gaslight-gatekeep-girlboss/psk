import { Button, Container, Group, Title } from "@mantine/core";
import { Link } from "react-router-dom";
import { paths } from "../shared/config/paths";
import { usePageTitle } from "../shared/hooks/use-page-title";

export const EmployeesRoute = () => {
  usePageTitle({ title: "Employees" });

  return (
    <Container>
      <Group justify="space-between">
        <Title order={5}>Employees</Title>
        <Button component={Link} to={paths.addEmployee.getHref()}>
          Add employee
        </Button>
      </Group>
    </Container>
  );
};
