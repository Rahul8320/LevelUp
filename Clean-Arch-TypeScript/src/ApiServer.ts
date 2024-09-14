import express, { Request, Response } from "express";
import { router as bookRouter } from "./LogBooks/Routes";

export class ApiServer {
  public static run(port: number): void {
    const app = express();

    app.use(express.json());

    app.get("/health-check", (_: Request, res: Response) => {
      res.status(200).json({
        statuscode: 200,
        message: "Server in running",
        timestamp: new Date().toLocaleString(),
      });
    });

    app.use("/api/books", bookRouter);

    app.listen(port, () => {
      console.log(`Server is running at port ${port}`);
    });
  }
}
