export type CreativeWork = {
  type: string;
  id: number;
  slug: string;
  author?: string;
  title?: string;
  series?: string;
  publisher?: string;
  publicationDate?: string;
  uri?: string;
};

export type Book = CreativeWork & {
  type: "book";
  isbn?: string;
};

export type Game = CreativeWork & {
  type: "game";
  igdbId?: number;
};

export type Song = CreativeWork & {
  type: "song";
};

export type Video = CreativeWork & {
  type: "video";
  season?: number;
  episode?: number;
};

export function isCreativeWork(obj: unknown): obj is CreativeWork {
  return (
    typeof obj === "object" &&
    obj !== null &&
    "type" in obj &&
    typeof obj.type === "string" &&
    "id" in obj &&
    typeof obj.id === "number" &&
    "slug" in obj &&
    typeof obj.slug === "string"
  );
}

export function isBook(obj: unknown): obj is Book {
  return isCreativeWork(obj) && obj.type === "book";
}

export function isGame(obj: unknown): obj is Game {
  return isCreativeWork(obj) && obj.type === "game";
}

export function isSong(obj: unknown): obj is Song {
  return isCreativeWork(obj) && obj.type === "song";
}

export function isVideo(obj: unknown): obj is Video {
  return isCreativeWork(obj) && obj.type === "video";
}
