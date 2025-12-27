import { Title } from "@solidjs/meta";
import { useParams } from "@solidjs/router";
import { HttpStatusCode } from "@solidjs/start";
import { createResource, ErrorBoundary, Show, Suspense } from "solid-js";
import { Marked } from "~/components/Marked";
import { Page } from "~/types/Page";

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

export default function ContentPage() {
  const params = useParams();
  const [page] = createResource(
    () => params.page,
    async (page) => {
      const response = await fetch(`https://localhost:7072/pages/${page}`);

      const json = await response.json();
      if (!response.ok) {
        throw new Error(
          `The call to ${response.url} returned ${response.status} ${response.statusText}`,
          { cause: json }
        );
      }

      return json as Page;
    }
  );

  return (
    <ErrorBoundary fallback={(error) => <NotFound error={error} />}>
      <Suspense fallback={<div>Loading...</div>}>
        <Show when={!page.error} fallback={<NotFound error={page.error} />}>
          <main data-page-id={page()?.id}>
            <Title>{page()?.title}</Title>
            <h1>{page()?.title}</h1>
            <Marked content={page()?.content} />
          </main>
        </Show>
      </Suspense>
    </ErrorBoundary>
  );
}
