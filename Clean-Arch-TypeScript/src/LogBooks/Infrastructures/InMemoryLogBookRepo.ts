import { LogBook } from "../Entities/LogBook";
import { LogBookId } from "../Entities/ValueObjects/LogBookId";
import { LogBookName } from "../Entities/ValueObjects/LogBookName";
import { ILogBookRepository } from "./ILogBookRepository";

type IBook = {
  id: string;
  name: string;
  userId: string;
};

export class InMemoryLogBookRepository implements ILogBookRepository {
  private _books: IBook[] = [];

  public async save(logBook: LogBook): Promise<boolean> {
    const book: IBook = {
      id: logBook.id.getValue(),
      name: logBook.name.getValue(),
      userId: logBook.userId,
    };

    this._books.push(book);

    return true;
  }

  public async isBookExists(name: string): Promise<boolean> {
    const book = this._books.find(
      (b) => b.name.toLowerCase() === name.toLowerCase()
    );

    return book === undefined ? false : true;
  }

  public async getBookByName(name: LogBookName): Promise<LogBook | null> {
    const book = this._books.find(
      (b) => b.name.toLowerCase() === name.getValue().toLowerCase()
    );

    if (book === undefined) {
      return null;
    }

    return new LogBook(
      LogBookName.create(book.name),
      book.userId,
      LogBookId.create(book.id)
    );
  }
}
