import { Container } from "@mantine/core";
import { Navigate, useParams } from "react-router-dom";
import { usePageTitle } from "../../shared/hooks/use-page-title";
import { paths } from "../../shared/config/paths";
import { EditEmployee } from "../../features/users/components/edit-employee";

export const EditEmployeeAdminRoute = () => {
  usePageTitle({ title: "Edit employee" });
  const { employeeId } = useParams();

  if (!employeeId) {
    return <Navigate to={paths.home.getHref()} />;
  }

  return (
    <Container maw={600} mt="lg">
      <EditEmployee employeeId={employeeId} />
    </Container>
  );
};
