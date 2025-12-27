import { CreativeWork } from "./CreativeWork";
import { apiFetch } from "./fetch";

export type Quote = {
  id: number;
  slug: string;
  text: string;
  context?: string;
  sourceId: number;
  source?: CreativeWork;
  exampleId: number;
  example?: CreativeWork;
  page?: number;
  location?: number;
  timestamp?: string;
};

export async function getQuote(id: number | "random" | "qotd") {
  const quote = await apiFetch<Quote>(`/quotes/${id}`);
  return quote;
}
