import { Title } from "@solidjs/meta";
import { useParams } from "@solidjs/router";
import { HttpStatusCode } from "@solidjs/start";
import {
  createMemo,
  createResource,
  ErrorBoundary,
  Match,
  Show,
  Suspense,
  Switch,
} from "solid-js";
import { apiFetch } from "~/api/fetch";
import { Page } from "~/api/Page";
import { DateTime } from "~/components/DateTime";
import { Marked } from "~/components/Marked";

export default function CatchAllPage() {
  const params = useParams();
  const [page] = createResource(
    () => params.page,
    (page) => apiFetch<Page>(`/pages/${page}`)
  );

  return (
    <ErrorBoundary fallback={(error) => <NotFound error={error} />}>
      <main>
        <Suspense fallback={<div>Loading...</div>}>
          <Show
            when={!page.error && page()}
            fallback={<NotFound error={page.error} />}
          >
            <Article page={page()!} />
          </Show>
        </Suspense>
      </main>
    </ErrorBoundary>
  );
}

function Article(props: { page: Page }) {
  const publishedOn = createMemo(() =>
    props.page.publishedOn ? new Date(props.page.publishedOn) : undefined
  );

  const updatedOn = createMemo(() =>
    props.page.updatedOn ? new Date(props.page.updatedOn) : undefined
  );

  return (
    <article data-page-id={props.page.id}>
      <header>
        <Title>{props.page.title}</Title>
        <h1>{props.page.title}</h1>
        <Switch>
          <Match when={updatedOn()}>
            <p>
              Last updated <DateTime>{updatedOn()}</DateTime>
            </p>
          </Match>
          <Match when={publishedOn()}>
            <p>
              Posted <DateTime>{publishedOn()}</DateTime>
            </p>
          </Match>
        </Switch>
      </header>
      <Marked content={props.page.content} />
    </article>
  );
}

function NotFound(props: { error?: Error }) {
  return (
    <main>
      <Title>Not Found</Title>
      <HttpStatusCode code={404} />
      <h1>Page Not Found</h1>
      <Show when={props.error}>
        {(error) => (
          <>
            {error().name}: {error().message}
            <pre>
              <code>
                {error().stack}

                <Show when={error().cause}>
                  {JSON.stringify(error().cause)}
                </Show>
              </code>
            </pre>
          </>
        )}
      </Show>
    </main>
  );
}
