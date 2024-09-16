from fastapi import APIRouter, status
from fastapi.exceptions import HTTPException
from typing import List
from src.books.books_data import books
from src.books.schemas import Book, UpdateBookModel

books_router = APIRouter()


@books_router.get('/', response_model = List[Book])
async def get_all_books():
    return books

@books_router.post('/', status_code = status.HTTP_201_CREATED)
async def create_new_book(request:Book) -> dict:
    newBook = request.model_dump()
    
    books.append(newBook)

    return newBook

@books_router.get('/{book_id}')
async def get_book(book_id:int) -> dict:
    for book in books:
        if book['id'] == book_id:
            return book
        
    raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="Book not found!")

@books_router.put('/{book_id}')
async def update_book(book_id:int, request: UpdateBookModel) -> dict:
    for book in books:
        if book['id'] == book_id:
            book['title'] = request.title
            book['author'] = request.author
            book['publisher'] = request.publisher
            book['page_count'] = request.page_count
            book['language'] = request.language

            return book

    raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="Book not found!")

@books_router.delete('/{book_id}', status_code = status.HTTP_204_NO_CONTENT)
async def get_all_books(book_id:int):
    for book in books:
        if book['id'] == book_id:
            books.remove(book)

            return {}
        
    raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="Book not found!")