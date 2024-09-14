import { IValueObject } from "../../../Shared/IValueObject";

export class LogBookName implements IValueObject<string> {
  private constructor(private readonly _value: string) {}

  public static create(value?: string | undefined): LogBookName {
    if (value === undefined || value === null || value.length === 0) {
      throw new Error("Name can not be empty");
    }

    return new LogBookName(value);
  }

  public getValue(): string {
    return this._value;
  }

  public toString(): string {
    return this._value;
  }
}
