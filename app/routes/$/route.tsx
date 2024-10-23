import { LoaderFunctionArgs } from '@remix-run/node';
import {
    isRouteErrorResponse,
    json,
    Link,
    MetaFunction,
    useLoaderData,
    useRouteError,
} from '@remix-run/react';
import Markdown from 'react-markdown';
import rehypeRaw from 'rehype-raw';
import { sendApiRequest } from '~/apiClient.server';
import type { Page } from './page';
import { BackendError } from '~/errors';

export async function loader({ params }: LoaderFunctionArgs) {
    const slug = params['*'];

    const page = await sendApiRequest<Page>('GET', `/pages/${slug}`);
    if (!page) {
        throw new BackendError(
            `Unexpected empty response from GET /pages/${slug}`,
        );
    }

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
            <article>
                <header>
                    <hgroup>
                        <h1>{page.title}</h1>
                    </hgroup>
                    <p>
                        Published
                        <time dateTime={page.publishedOn}>
                            {page.publishedOn}
                        </time>
                    </p>
                    <p>
                        <Link
                            to={`/pages/edit?page=${encodeURIComponent(page.slug)}`}
                        >
                            Edit this page
                        </Link>
                    </p>
                </header>
                <section>
                    <Markdown
                        rehypePlugins={[rehypeRaw]}
                        remarkRehypeOptions={{ allowDangerousHtml: true }}
                    >
                        {page.content}
                    </Markdown>
                </section>
            </article>
        </main>
    );
}

export function ErrorBoundary() {
    const error = useRouteError();

    // TODO: Shouldn't most of this (outside of 404 and maybe 401/403) be in the root?

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
