import dotenv from "dotenv";
import { ApiServer } from "./ApiServer";
import { env } from "process";

function main(): void {
  // Load .env file
  dotenv.config();

  const port = Number(env.PORT);

  ApiServer.run(port);
}

main();
