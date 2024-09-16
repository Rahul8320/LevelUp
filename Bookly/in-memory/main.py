from typing import List
from fastapi import FastAPI, status
from fastapi.exceptions import HTTPException
from pydantic import BaseModel

app = FastAPI()

# in memory books data
books = [{
  "id": 1,
  "title": "Kids of Survival",
  "author": "Joli Kilmurray",
  "publisher": "Stratasys, Ltd.",
  "published_date": "26/07/2021",
  "page_count": 8433,
  "language": "Bengali"
}, {
  "id": 2,
  "title": "Vincere",
  "author": "Dara Harpham",
  "publisher": "E-TRADE Financial Corporation",
  "published_date": "13/11/2010",
  "page_count": 642,
  "language": "Tswana"
}, {
  "id": 3,
  "title": "Dream Land",
  "author": "Livvie Bardsley",
  "publisher": "The ONE Group Hospitality, Inc.",
  "published_date": "05/03/2005",
  "page_count": 848,
  "language": "Japanese"
}, {
  "id": 4,
  "title": "December 7th",
  "author": "Hervey Ochterlonie",
  "publisher": "Lipocine Inc.",
  "published_date": "03/01/2008",
  "page_count": 208,
  "language": "Romanian"
}, {
  "id": 5,
  "title": "One Way Boogie Woogie",
  "author": "Geordie Thompkins",
  "publisher": "Enterprise Bancorp Inc",
  "published_date": "19/06/2015",
  "page_count": 327,
  "language": "Danish"
}, {
  "id": 6,
  "title": "Utamaro and His Five Women (Utamaro o meguru gonin no onna)",
  "author": "Giusto Killick",
  "publisher": "Kaman Corporation",
  "published_date": "10/08/2001",
  "page_count": 903,
  "language": "Kannada"
}, {
  "id": 7,
  "title": "The Indian Fighter",
  "author": "Townie Whorlton",
  "publisher": "Fortress Investment Group LLC",
  "published_date": "27/10/2019",
  "page_count": 948,
  "language": "Afrikaans"
}, {
  "id": 8,
  "title": "Welcome to Dongmakgol",
  "author": "Royal Trimmell",
  "publisher": "Federal Agricultural Mortgage Corporation",
  "published_date": "25/02/2006",
  "page_count": 875,
  "language": "Hebrew"
}, {
  "id": 9,
  "title": "Here Comes Cookie",
  "author": "Jamima Yellop",
  "publisher": "Seres Therapeutics, Inc.",
  "published_date": "02/06/2024",
  "page_count": 6231,
  "language": "Tamil"
}, {
  "id": 10,
  "title": "Single Shot, A",
  "author": "Lark Sheppey",
  "publisher": "CHS Inc",
  "published_date": "15/04/2018",
  "page_count": 993,
  "language": "Hungarian"
}, {
  "id": 11,
  "title": "Short Night of the Glass Dolls (La corta notte delle bambole di vetro)",
  "author": "Theobald Walkling",
  "publisher": "Regional Management Corp.",
  "published_date": "15/02/2020",
  "page_count": 7912,
  "language": "Azeri"
}, {
  "id": 12,
  "title": "Barbarella",
  "author": "Jenica Twopenny",
  "publisher": "Nautilus Group, Inc. (The)",
  "published_date": "21/09/2005",
  "page_count": 5449,
  "language": "Romanian"
}, {
  "id": 13,
  "title": "The Overnight",
  "author": "Anatola Caught",
  "publisher": "Equity Residential",
  "published_date": "03/07/2017",
  "page_count": 375,
  "language": "Portuguese"
}, {
  "id": 14,
  "title": "Perfect Getaway, A",
  "author": "Corbett Sturgis",
  "publisher": "Immuron Limited",
  "published_date": "10/04/2005",
  "page_count": 1609,
  "language": "Punjabi"
}, {
  "id": 15,
  "title": "Adonis Factor, The",
  "author": "Laureen Claesens",
  "publisher": "Hudson Global, Inc.",
  "published_date": "11/02/2014",
  "page_count": 518,
  "language": "English"
}, {
  "id": 16,
  "title": "Fate (Yazgi)",
  "author": "Piotr Dossantos",
  "publisher": "Playa Hotels & Resorts N.V.",
  "published_date": "24/07/2006",
  "page_count": 2172,
  "language": "Icelandic"
}, {
  "id": 17,
  "title": "Brassed Off",
  "author": "Durant Zanussii",
  "publisher": "Eaton Vance Tax-Managed Global Diversified Equity Income Fund",
  "published_date": "17/05/2015",
  "page_count": 846,
  "language": "Khmer"
}, {
  "id": 18,
  "title": "St. Valentine's Day Massacre, The",
  "author": "Jordon Falloon",
  "publisher": "BLDRS Europe 100 ADR Index Fund",
  "published_date": "16/04/2007",
  "page_count": 9245,
  "language": "Marathi"
}, {
  "id": 19,
  "title": "Good Mother, The",
  "author": "Rachele Willimott",
  "publisher": "Southern Company (The)",
  "published_date": "02/11/2014",
  "page_count": 4345,
  "language": "Hebrew"
}, {
  "id": 20,
  "title": "Star Trek: Generations",
  "author": "Iona Heaysman",
  "publisher": "Arch Capital Group Ltd.",
  "published_date": "09/08/2011",
  "page_count": 88788,
  "language": "Hebrew"
}]


class Book(BaseModel):
    id: int
    title: str
    author: str
    publisher: str
    published_date: str
    page_count: int
    language: str

class UpdateBookModel(BaseModel):
    title: str
    author: str
    publisher: str
    page_count: int
    language: str


@app.get('/api/books', response_model = List[Book])
async def get_all_books():
    return books

@app.post('/api/books', status_code = status.HTTP_201_CREATED)
async def create_new_book(request:Book) -> dict:
    newBook = request.model_dump()
    
    books.append(newBook)

    return newBook

@app.get('/api/books/{book_id}')
async def get_book(book_id:int) -> dict:
    for book in books:
        if book['id'] == book_id:
            return book
        
    raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="Book not found!")

@app.put('/api/books/{book_id}')
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

@app.delete('/api/books/{book_id}', status_code = status.HTTP_204_NO_CONTENT)
async def get_all_books(book_id:int):
    for book in books:
        if book['id'] == book_id:
            books.remove(book)

            return {}
        
    raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="Book not found!")