from fastapi import FastAPI
from pydantic import BaseModel 

app = FastAPI()

@app.get('/')
async def read_root():
    return {"message": "Hello world!"}

@app.get("/greet/{name}")
async def greet_user(name:str, age:int) -> dict:
    return {"message": f"Hello {name}", "age": age}

class CreateBookModel(BaseModel):
    title: str
    author: str

@app.post("/create_book", status_code = 201)
async def create_book(request:CreateBookModel) -> dict:
    return {
        "title": request.title,
        "author": request.author
    }