import { LoaderFunctionArgs } from '@remix-run/node';
import {
    isRouteErrorResponse,
    json,
    MetaFunction,
    useLoaderData,
    useRouteError,
} from '@remix-run/react';
import type { Page } from './page';

export async function loader({ params }: LoaderFunctionArgs) {
    const slug = params['*'];

    const apiUrl = new URL(`/pages/${slug}`, process.env.API_URL);
    const response = await fetch(apiUrl);

    if (response.status === 404) {
        throw new Response('Not Found', { status: 404 });
    } else if (!response.ok) {
        throw new Response(response.statusText, {
            status: 502,
        });
    }

    const page = (await response.json()) as Page;
    return json({ page });
}

export const meta: MetaFunction<typeof loader> = ({ data }) => {
    if (!data || !data.page) return [{ title: 'Vivelin.net' }];

    return [
        { title: `${data.page.title} — Vivelin.net` },
        { name: 'og:site_name', content: 'Vivelin.net' },
        { name: 'og:locale', content: 'en_US' },
        { name: 'og:type', content: 'website' }, // Maybe should be article once we have all the proper metadata?
        { name: 'og:title', content: data.page.title },
        { name: 'og:description', content: data.page.description },
    ];
};

export default function Page() {
    const { page } = useLoaderData<typeof loader>();

    // TODO: Generic page metadata in a header/hgroup element? Or maybe separate articles and pages?
    return (
        <main>
            <article dangerouslySetInnerHTML={{ __html: page.content }} />
        </main>
    );
}

export function ErrorBoundary() {
    const error = useRouteError();

    if (isRouteErrorResponse(error)) {
        if (error.status === 404) {
            return (
                <main>
                    <hgroup>
                        <h1>Error 404</h1>
                        <p>The page you’re looking for does not exist.</p>
                    </hgroup>
                    <p>This is where the 404 page would be, if I had one.</p>
                </main>
            );
        } else {
            return (
                <main>
                    <hgroup>
                        <h1>Error {error.status}</h1>
                        <p>{error.statusText}</p>
                    </hgroup>
                    <p>
                        Something went wrong, and we don’t have more specific
                        error information for you.
                    </p>
                </main>
            );
        }
    } else if (
        error instanceof Error &&
        process.env.NODE_ENV === 'development'
    ) {
        return (
            <main>
                <hgroup>
                    <h1>{error.name}</h1>
                    <p>{error.message}</p>
                </hgroup>

                {error.stack && (
                    <pre>
                        <samp>{error.stack}</samp>
                    </pre>
                )}

                {error.cause instanceof String && <p>Cause: {error.cause}</p>}
                {error.cause instanceof Error && (
                    <section>
                        <hgroup>
                            <h2>Cause: {error.cause.name}</h2>
                            <p>{error.cause.message}</p>
                        </hgroup>

                        {error.cause.stack && (
                            <pre>
                                <samp>{error.cause.stack}</samp>
                            </pre>
                        )}
                    </section>
                )}
            </main>
        );
    } else {
        return (
            <main>
                <hgroup>
                    <h1>Something went wrong.</h1>
                </hgroup>
                <p>
                    Something went wrong, and we don’t have more specific error
                    information for you.
                </p>
            </main>
        );
    }
}
