import { randomUUID } from "crypto";
import { IValueObject } from "../../../Shared/IValueObject";

export class LogBookId implements IValueObject<string> {
  private constructor(private readonly _value: string) {}

  public static create(value?: string): LogBookId {
    const id = value || randomUUID();

    this.validate(id);
    return new LogBookId(id);
  }

  public getValue(): string {
    return this._value;
  }

  public toString(): string {
    return this._value;
  }

  private static validate(id: string): void {
    // Basic validation for GUID format
    const uuidRegex =
      /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/;

    if (!uuidRegex.test(id)) {
      throw new Error("Invalid UUID format");
    }
  }
}
