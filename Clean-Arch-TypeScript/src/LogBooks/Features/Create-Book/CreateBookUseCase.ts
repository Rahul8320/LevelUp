import { IUseCase } from "../../../Shared/IUseCase";
import logger from "../../../Shared/Logger";
import { LogBook } from "../../Entities/LogBook";
import { LogBookName } from "../../Entities/ValueObjects/LogBookName";
import { ILogBookRepository } from "../../Infrastructures/ILogBookRepository";
import { ICreateBookRequest } from "./CreateBookRequest";
import { CreateBookResponse, ICreateBookResponse } from "./CreateBookResponse";

export class CreateBookUseCase
  implements IUseCase<ICreateBookRequest, ICreateBookResponse>
{
  public constructor(private readonly _bookRepository: ILogBookRepository) {}

  public async handle(
    request: ICreateBookRequest
  ): Promise<ICreateBookResponse> {
    try {
      logger.info(
        `Checking book with same name '${request.name}' is present into the db.`
      );
      var isBookExists = await this._bookRepository.isBookExists(request.name);

      if (isBookExists) {
        logger.info(`Book '${request.name}' is already exists.`);
        return CreateBookResponse.AlreadyExists(request.name);
      }

      logger.info("Creating new book.");
      let logBook = new LogBook(
        LogBookName.create(request.name),
        request.userId
      );

      logger.info("Saving newly created book.");
      let result = await this._bookRepository.save(logBook);

      if (result === false) {
        logger.error("Failed to create log book!");
        return CreateBookResponse.ServerError(`Failed to create log book!`);
      }

      return CreateBookResponse.Success(logBook.id.getValue());
    } catch (err: any) {
      logger.error(err.message, err);
      return CreateBookResponse.ServerError(err.message);
    }
  }
}
