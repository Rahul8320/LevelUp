export interface RegisterRequest {
  name: string;
  userName: string;
  email: string;
  password: string;
}

export interface RegisterResponse {
  statusCode: string;
  message: string;
}
