export type Role = "Client" | "Employee" | "BusinessOwner";

export type User = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  role: Role;
};
