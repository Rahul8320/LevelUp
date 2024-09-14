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
  public constructor(
    public readonly statusCode: number,
    public readonly message: string
  ) {}
}

export class CreateBookSuccessResponse implements ICreateBookResponse {
  public statusCode: number;
  public message: string;
  public data?: ICreateBookResponseData | null;

  public constructor(bookId: string) {
    this.statusCode = 201;
    this.message = "Log Book created successfully.";
    this.data = {
      id: bookId,
    };
  }
}

export class CreateBookValidationErrorResponse implements ICreateBookResponse {
  public statusCode: number;
  public message: string;

  public constructor(public readonly errors: string[]) {
    this.statusCode = 400;
    this.message = "Validation Failed!";
  }
}
