export interface IUseCase<TRequest, TResponse> {
  handle(request: TRequest): Promise<TResponse>;
}
