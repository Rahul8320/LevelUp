import express, { Request, Response } from "express";
import { router as bookRouter } from "./LogBooks/Routes";
import logger from "./Shared/Logger";

export class ApiServer {
  public static run(port: number): void {
    const app = express();

    app.use(express.json());

    app.get("/health-check", (_: Request, res: Response) => {
      logger.info("Health check passed.");

      res.status(200).json({
        statuscode: 200,
        message: "Server in running",
        timestamp: new Date().toLocaleString(),
      });
    });

    app.use("/api/books", bookRouter);

    app.listen(port, () => {
      logger.info(`Server is running at port ${port}`);
    });
  }
}
