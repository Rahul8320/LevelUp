from fastapi import FastAPI
from src.books.routes import books_router

version = "v1"

app = FastAPI(
    title= "Bookly",
    description= "A REST API for book review web service",
    version= version
)

app.include_router(books_router, prefix= f"/api/{version}/books", tags= ["books"])