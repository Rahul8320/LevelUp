import { IUseCase } from "../../../Shared/IUseCase";
import { LogBook } from "../../Entities/LogBook";
import { LogBookName } from "../../Entities/ValueObjects/LogBookName";
import { ILogBookRepository } from "../../Infrastructures/ILogBookRepository";
import { ICreateBookRequest } from "./CreateBookRequest";
import {
  CreateBookResponse,
  CreateBookSuccessResponse,
  ICreateBookResponse,
} from "./CreateBookResponse";

export class CreateBookUseCase
  implements IUseCase<ICreateBookRequest, ICreateBookResponse>
{
  public constructor(private readonly _bookRepository: ILogBookRepository) {}

  public async handle(
    request: ICreateBookRequest
  ): Promise<ICreateBookResponse> {
    try {
      var isBookExists = await this._bookRepository.isBookExists(request.name);

      if (isBookExists) {
        return new CreateBookResponse(
          400,
          `Book with name: '${request.name}' already exists!`
        );
      }

      let logBook = new LogBook(
        LogBookName.create(request.name),
        request.userId
      );

      let result = await this._bookRepository.save(logBook);

      if (result === false) {
        return new CreateBookResponse(500, `Failed to create log book!!`);
      }

      return new CreateBookSuccessResponse(logBook.id.getValue());
    } catch (err: any) {
      return new CreateBookResponse(
        500,
        err.message ?? "Error: Create book use case failed!"
      );
    }
  }
}
