import { IUseCase } from "../../../Shared/IUseCase";
import logger from "../../../Shared/Logger";
import { ILogBookRepository } from "../../Infrastructures/ILogBookRepository";
import { IGetBookRequest } from "./GetBookRequest";
import { GetBookResponse, IGetBookResponse } from "./GetBookResponse";

export class GetBookUseCase
  implements IUseCase<IGetBookRequest, IGetBookResponse>
{
  public constructor(private readonly _bookRepository: ILogBookRepository) {}

  public async handle(request: IGetBookRequest): Promise<IGetBookResponse> {
    try {
      var book = await this._bookRepository.findById(request.bookId);

      if (book === null) {
        return GetBookResponse.NotFound(request.bookId);
      }

      return GetBookResponse.Success(book);
    } catch (err: any) {
      logger.error(err.message, err);
      return GetBookResponse.ServerError(err.message);
    }
  }
}
