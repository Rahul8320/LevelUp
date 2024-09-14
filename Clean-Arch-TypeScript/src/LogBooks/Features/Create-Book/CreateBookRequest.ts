export type ICreateBookRequest = {
  name: string;
  userId: string;
};

export class CreateBookRequest implements ICreateBookRequest {
  public constructor(
    public readonly name: string,
    public readonly userId: string
  ) {}
}
