import { LogBook } from "../Entities/LogBook";

export interface ILogBookRepository {
  findById(bookId: string): Promise<LogBook | null>;
  save(logBook: LogBook): Promise<boolean>;
  isBookExists(name: string): Promise<boolean>;
}
