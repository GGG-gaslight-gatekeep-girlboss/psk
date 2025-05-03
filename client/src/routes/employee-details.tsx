import { useParams } from "react-router-dom";

export const EmployeeDetailsRoute = () => {
  const { employeeId } = useParams();

  return <div>Edit {employeeId} TODO</div>;
};
