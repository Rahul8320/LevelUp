export type IGetBookRequest = {
  bookId: string;
};

export class GetBookRequest implements IGetBookRequest {
  public constructor(public readonly bookId: string) {}
}
