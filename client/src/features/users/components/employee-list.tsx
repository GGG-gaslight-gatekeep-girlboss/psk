import { Table } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import { paths } from "../../../shared/config/paths";
import { useEmployees } from "../api/get-employees";

export const EmployeeList = () => {
  const employeesQuery = useEmployees({});
  const navigate = useNavigate();

  const navigateToEmployee = (employeeId: string) => navigate(paths.editEmployee.getHref(employeeId));

  if (employeesQuery.isLoading) {
    return <div>Loading...</div>;
  }

  if (!employeesQuery.data) {
    return <div>Something went wrong...</div>;
  }

  const rows = employeesQuery.data.map((e) => (
    <Table.Tr key={e.id} onClick={() => navigateToEmployee(e.id)}>
      <Table.Td>{e.firstName}</Table.Td>
      <Table.Td>{e.lastName}</Table.Td>
    </Table.Tr>
  ));

  return (
    <Table withTableBorder striped highlightOnHover>
      <Table.Thead>
        <Table.Tr>
          <Table.Th>First name</Table.Th>
          <Table.Th>Last name</Table.Th>
        </Table.Tr>
      </Table.Thead>

      <Table.Tbody>{rows}</Table.Tbody>
    </Table>
  );
};
