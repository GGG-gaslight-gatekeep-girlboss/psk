export type Role = "Client" | "Employee" | "BusinessOwner";

export type User = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  role: Role;
};

export type Token = {
  token: string;
  expiresAt: string;
};

export type TokenMetadata = {
  expiresAt: string;
};

export type LoginResponse = {
  user: User;
  accessToken: Token;
};
