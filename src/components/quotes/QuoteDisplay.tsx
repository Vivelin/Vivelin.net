import { Quote } from "~/api/Quote";
import { Marked } from "../Marked";
import { Show } from "solid-js";
import { QuoteSource } from "./QuoteSource";

export function QuoteDisplay(props: { quote: Quote }) {
  return (
    <figure class="quote" data-id={props.quote?.id}>
      <blockquote>
        <Marked content={props.quote?.text} />
      </blockquote>
      <figcaption>
        <Show when={props.quote?.context}>{props.quote?.context}, </Show>
        <Show when={props.quote?.source}>
          {(source) => <QuoteSource source={source()} />}
        </Show>
        <Show when={props.quote?.example}>
          {(example) => (
            <>
              See: <QuoteSource source={example()} />
            </>
          )}
        </Show>
      </figcaption>
    </figure>
  );
}
