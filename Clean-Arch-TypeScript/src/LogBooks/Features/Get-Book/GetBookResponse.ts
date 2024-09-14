import { LogBook } from "../../Entities/LogBook";

export type IGetBookResponse = {
  statusCode: number;
  message: string;
  data?: ILogBookModel | null;
};

type ILogBookModel = {
  id: string;
  name: string;
  userId: string;
};

class LogBookModel implements ILogBookModel {
  public constructor(
    public readonly id: string,
    public readonly name: string,
    public readonly userId: string
  ) {}
}

export class GetBookResponse implements IGetBookResponse {
  public statusCode: number;
  public message: string;
  public data?: ILogBookModel;

  private constructor(
    statusCode: number,
    message: string,
    data?: ILogBookModel
  ) {
    this.statusCode = statusCode;
    this.message = message;
    this.data = data;
  }

  public static Success(book: LogBook): GetBookResponse {
    var bookModel = new LogBookModel(
      book.id.getValue(),
      book.name.getValue(),
      book.userId
    );

    return new GetBookResponse(200, "Book fetched successfully", bookModel);
  }

  public static NotFound(bookId: string): GetBookResponse {
    return new GetBookResponse(404, `Book with id ${bookId} was not found!`);
  }

  public static ServerError(errorMessage?: string): GetBookResponse {
    return new GetBookResponse(
      500,
      errorMessage ?? "An unknown error occurred!"
    );
  }
}
