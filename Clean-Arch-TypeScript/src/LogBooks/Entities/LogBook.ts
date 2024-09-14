import { LogBookName } from "./ValueObjects/LogBookName";
import { LogBookId } from "./ValueObjects/LogBookId";

export class LogBook {
  public constructor(
    public readonly name: LogBookName,
    public readonly userId: string,
    public readonly id: LogBookId = LogBookId.create()
  ) {}
}
