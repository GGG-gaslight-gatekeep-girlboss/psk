import { Container } from "@mantine/core";
import { AddEmployee } from "../../features/users/components/add-employee";
import { usePageTitle } from "../../shared/hooks/use-page-title";

export const AddEmployeeAdminRoute = () => {
  usePageTitle({ title: "Add employee" });

  return (
    <Container maw={600} mt="lg">
      <AddEmployee />
    </Container>
  );
};
