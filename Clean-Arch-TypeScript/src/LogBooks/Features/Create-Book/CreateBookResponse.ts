export type ICreateBookResponse = {
  statusCode: number;
  message: string;
  data?: ICreateBookResponseData | null;
  errors?: string[] | null;
};

type ICreateBookResponseData = {
  id: string;
};

export class CreateBookResponse implements ICreateBookResponse {
  public statusCode: number;
  public message: string;
  public data?: ICreateBookResponseData | null;
  public errors?: string[] | null;

  private constructor(
    statusCode: number,
    message: string,
    data?: ICreateBookResponseData | null,
    errors?: string[] | null
  ) {
    this.statusCode = statusCode;
    this.message = message;
    this.data = data;
    this.errors = errors;
  }

  public static Success(bookId: string) {
    const data = {
      id: bookId,
    };

    return new CreateBookResponse(201, "Book created successfully.", data);
  }

  public static ValidationErrors(errors: string[]): CreateBookResponse {
    return new CreateBookResponse(400, "Validation failed!", null, errors);
  }

  public static AlreadyExists(name: string): CreateBookResponse {
    return new CreateBookResponse(
      400,
      `Book with name: '${name}' already exists!`
    );
  }

  public static ServerError(errorMessage?: string): CreateBookResponse {
    return new CreateBookResponse(
      500,
      errorMessage ?? "An unknown error occurred!"
    );
  }
}
