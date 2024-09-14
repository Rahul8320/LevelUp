export interface IValueObject<T> {
  getValue(): T;
  toString(): string;
}
