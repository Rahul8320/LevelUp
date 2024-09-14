import { Request, Response } from "express";
import { CreateBookRequest, ICreateBookRequest } from "./CreateBookRequest";
import { IUseCase } from "../../../Shared/IUseCase";
import { CreateBookResponse, ICreateBookResponse } from "./CreateBookResponse";

export class CreateBookController {
  constructor(
    private readonly _useCase: IUseCase<ICreateBookRequest, ICreateBookResponse>
  ) {}

  public async execute(req: Request, res: Response) {
    const { name, userId } = req.body;
    const createBookRequest = new CreateBookRequest(name, userId);

    const validateResults = this.validate(createBookRequest);

    if (validateResults.length > 0) {
      const response = CreateBookResponse.ValidationErrors(validateResults);
      return res.status(response.statusCode).json(response);
    }

    const response = await this._useCase.handle(createBookRequest);

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
