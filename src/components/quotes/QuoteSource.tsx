import { Show } from "solid-js";
import { CreativeWork, isVideo, Video } from "~/api/CreativeWork";
import { Quote } from "~/api/Quote";

export function QuoteSource(props: { source: CreativeWork }) {
  return (
    <>
      <Show when={props.source.author}>
        <span class="author">{props.source.author}</span>,{" "}
      </Show>
      <cite>
        <a href={props.source.uri} target="_blank" rel="external">
          <Show when={props.source.title}>
            <span class="title">“{props.source.title}”</span>,{" "}
          </Show>
          <Show when={props.source.series}>
            <span class="series">{props.source.series}</span>
          </Show>
        </a>
      </cite>
      <Show when={isVideo(props.source)}>
        <>
          {" "}
          <Show when={(props.source as Video).season}>
            {(season) => <span class="season">Season {season()}</span>}
          </Show>
          <Show when={(props.source as Video).episode}>
            {(episode) => <span class="episode">Episode {episode()}</span>}
          </Show>
        </>
      </Show>
      <Show when={props.source.publisher}>
        <span class="published">
          {" "}
          ({props.source.publisher}, {props.source.publicationDate})
        </span>
      </Show>
    </>
  );
}
