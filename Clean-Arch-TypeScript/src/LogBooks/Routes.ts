import { Router } from "express";
import { CreateBookController } from "./Features/Create-Book/CreateBookController";
import { CreateBookUseCase } from "./Features/Create-Book/CreateBookUseCase";
import { InMemoryLogBookRepository } from "./Infrastructures/InMemoryLogBookRepo";

export const router = Router();

const inMemoryBookRepo = new InMemoryLogBookRepository();
const createBookUSeCase = new CreateBookUseCase(inMemoryBookRepo);
const createBookController = new CreateBookController(createBookUSeCase);

router.post("/", (req, res) => createBookController.execute(req, res));
