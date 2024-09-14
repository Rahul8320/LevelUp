import { Request, Response } from "express";
import { GetBookRequest, IGetBookRequest } from "./GetBookRequest";
import { IUseCase } from "../../../Shared/IUseCase";
import { IGetBookResponse } from "./GetBookResponse";

export class GetBookController {
  public constructor(
    private readonly _useCase: IUseCase<IGetBookRequest, IGetBookResponse>
  ) {}

  public async execute(req: Request, res: Response) {
    const { id } = req.params;

    const getBookRequest = new GetBookRequest(id);

    const response = await this._useCase.handle(getBookRequest);

    return res.status(response.statusCode).json(response);
  }
}
