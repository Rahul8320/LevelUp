import { LogBook } from "../Entities/LogBook";
import { LogBookName } from "../Entities/ValueObjects/LogBookName";

export interface ILogBookRepository {
  findById(bookId: string): Promise<LogBook | null>;
  save(logBook: LogBook): Promise<boolean>;
  findByName(name: LogBookName): Promise<LogBook | null>;
  isBookExists(name: string): Promise<boolean>;
}
