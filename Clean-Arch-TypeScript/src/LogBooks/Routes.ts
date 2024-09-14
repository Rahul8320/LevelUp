import { Router } from "express";
import { CreateBookController } from "./Features/Create-Book/CreateBookController";
import { CreateBookUseCase } from "./Features/Create-Book/CreateBookUseCase";
import { InMemoryLogBookRepository } from "./Infrastructures/InMemoryLogBookRepo";
import { GetBookUseCase } from "./Features/Get-Book/GetBookUseCase";
import { GetBookController } from "./Features/Get-Book/GetBookController";

export const router = Router();

const inMemoryBookRepo = new InMemoryLogBookRepository();
const createBookUseCase = new CreateBookUseCase(inMemoryBookRepo);
const createBookController = new CreateBookController(createBookUseCase);

const getBookUseCase = new GetBookUseCase(inMemoryBookRepo);
const getBookController = new GetBookController(getBookUseCase);

router.post("/", (req, res) => createBookController.execute(req, res));
router.get("/:id", (req, res) => getBookController.execute(req, res));
