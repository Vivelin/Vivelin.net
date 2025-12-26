import { Title } from "@solidjs/meta";
import { useParams } from "@solidjs/router";
import { HttpStatusCode } from "@solidjs/start";
import { createResource, ErrorBoundary, Show, Suspense } from "solid-js";
import { Page } from "~/types/Page";

function NotFound(props: { error: Error }) {
  return (
    <main>
      <Title>Not Found</Title>
      <HttpStatusCode code={404} />
      <h1>Page Not Found</h1>
      {props.error.name}: {props.error.message}
      <pre>
        <code>
          {props.error.stack}
          <Show when={"error" in props.error}>{props.error.error}</Show>
        </code>
      </pre>
    </main>
  );
}

export default function ContentPage() {
  const params = useParams();
  const [page] = createResource(async () => {
    const response = await fetch(`https://localhost:7072/pages/${params.page}`);
    const json = await response.json();
    if (!response.ok) throw new Error("Not found", { error: json });
    return json as Page;
  });

  return (
    <ErrorBoundary fallback={(error) => <NotFound error={error} />}>
      <Title>{page()?.title}</Title>
      <Suspense fallback={<div>Loading...</div>}>
        <main data-page-id={page()?.id}>
          <Title>{page()?.title}</Title>
          <h1>{page()?.title}</h1>
          {page()?.content}
        </main>
      </Suspense>
    </ErrorBoundary>
  );
}
