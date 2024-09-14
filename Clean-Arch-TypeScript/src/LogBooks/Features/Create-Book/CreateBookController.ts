import { Request, Response } from "express";
import { CreateBookRequest, ICreateBookRequest } from "./CreateBookRequest";
import { IUseCase } from "../../../Shared/IUseCase";
import { CreateBookResponse, ICreateBookResponse } from "./CreateBookResponse";
import logger from "../../../Shared/Logger";

export class CreateBookController {
  constructor(
    private readonly _useCase: IUseCase<ICreateBookRequest, ICreateBookResponse>
  ) {}

  public async execute(req: Request, res: Response) {
    logger.info("Executing create book request.");

    const { name, userId } = req.body;
    const createBookRequest = new CreateBookRequest(name, userId);

    logger.info("Validating request data.");
    const validateResults = this.validate(createBookRequest);

    if (validateResults.length > 0) {
      logger.info("Request validation failed.");
      const response = CreateBookResponse.ValidationErrors(validateResults);
      return res.status(response.statusCode).json(response);
    }

    logger.info("Request validation succeed.");

    const response = await this._useCase.handle(createBookRequest);

    logger.info("Create book request succeed.");
    return res.status(response.statusCode).json(response);
  }

  private validate(request: ICreateBookRequest): string[] {
    let errors: string[] = [];

    if (
      request.name === null ||
      request.name === undefined ||
      request.name.trim().length === 0
    ) {
      errors.push("Book Name is required!");
    }

    if (
      request.userId === null ||
      request.userId === undefined ||
      request.userId.trim().length === 0
    ) {
      errors.push("Book UserId is required!");
    }

    return errors;
  }
}
