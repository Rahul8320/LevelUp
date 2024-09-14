import { LogBook } from "../Entities/LogBook";
import { LogBookName } from "../Entities/ValueObjects/LogBookName";

export interface ILogBookRepository {
  save(logBook: LogBook): Promise<boolean>;
  getBookByName(name: LogBookName): Promise<LogBook | null>;
  isBookExists(name: string): Promise<boolean>;
}
