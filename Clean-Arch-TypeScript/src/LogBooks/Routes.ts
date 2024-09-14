import { Router } from "express";
import { CreateBookController } from "./Features/Create-Book/CreateBookController";
import { CreateBookUseCase } from "./Features/Create-Book/CreateBookUseCase";
import { GetBookUseCase } from "./Features/Get-Book/GetBookUseCase";
import { GetBookController } from "./Features/Get-Book/GetBookController";
import { PrismaLogBookRepository } from "./Infrastructures/PrismaLogBookRepository";
import { PrismaClient } from "@prisma/client";

export const router = Router();

const prismaClient = new PrismaClient();
const prismaBookRepo = new PrismaLogBookRepository(prismaClient);

const createBookUseCase = new CreateBookUseCase(prismaBookRepo);
const createBookController = new CreateBookController(createBookUseCase);

const getBookUseCase = new GetBookUseCase(prismaBookRepo);
const getBookController = new GetBookController(getBookUseCase);

router.post("/", (req, res) => createBookController.execute(req, res));
router.get("/:id", (req, res) => getBookController.execute(req, res));
