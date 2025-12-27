import { createResource, ErrorBoundary, Show, Suspense } from "solid-js";
import { getQuote } from "~/api/Quote";
import { Marked } from "../Marked";
import { QuoteSource } from "./QuoteSource";
import { QuoteDisplay } from "./QuoteDisplay";

export function RandomQuote() {
  const [quote, { refetch }] = createResource("random", getQuote);
  return (
    <ErrorBoundary fallback={<></>}>
      <Suspense>
        <Show when={!quote.error}>
          <QuoteDisplay quote={quote()!} />
        </Show>
      </Suspense>
    </ErrorBoundary>
  );
}
