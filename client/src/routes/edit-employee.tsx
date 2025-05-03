import { Container } from "@mantine/core";
import { Navigate, useParams } from "react-router-dom";
import { EditEmployee } from "../features/users/components/edit-employee";
import { paths } from "../shared/config/paths";

export const EditEmployeeRoute = () => {
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
