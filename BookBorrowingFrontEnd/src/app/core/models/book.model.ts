export interface Book {
    id: string;
    name: string;
    description: string;
    rating: number;
    coverImage: string;
    images?: [string];
    author: string;
    genre: string;
    is_Book_Available: boolean;
    lent_By_User_id: string;
    currently_Borrowed_By_User_Id: string | null;
    createdAt: string;
    lastUpdated: string;
}