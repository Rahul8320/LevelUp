import { PrismaClient } from "@prisma/client";
import { LogBook } from "../Entities/LogBook";
import { ILogBookRepository } from "./ILogBookRepository";
import { LogBookName } from "../Entities/ValueObjects/LogBookName";
import { LogBookId } from "../Entities/ValueObjects/LogBookId";

export class PrismaLogBookRepository implements ILogBookRepository {
  public constructor(private readonly _prismaClient: PrismaClient) {}

  public async findById(bookId: string): Promise<LogBook | null> {
    const book = await this._prismaClient.logBook.findUnique({
      where: {
        id: bookId,
      },
    });

    if (book !== null) {
      return new LogBook(
        LogBookName.create(book.name),
        book.userId,
        LogBookId.create(book.id)
      );
    }

    return book;
  }

  public async save(logBook: LogBook): Promise<boolean> {
    await this._prismaClient.logBook.upsert({
      where: { id: logBook.id.getValue() },
      update: {
        name: logBook.name.getValue(),
        userId: logBook.userId,
      },
      create: {
        id: logBook.id.getValue(),
        name: logBook.name.getValue(),
        userId: logBook.userId,
      },
    });
    return true;
  }

  public async isBookExists(name: string): Promise<boolean> {
    const count = await this._prismaClient.logBook.count({
      where: { name },
    });
    return count > 0;
  }
}
