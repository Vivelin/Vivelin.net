import { Await, defer, useLoaderData } from '@remix-run/react';
import { Suspense } from 'react';
import { sendApiRequest } from '~/apiClient.server';
import { Quote } from '~/components/widgets/quote/Quote';
import QuoteArticle from './QuoteArticle';

export async function loader() {
    const quotesPromise = sendApiRequest<Quote[]>('GET', 'quotes');

    return defer({ quotes: quotesPromise });
}

export default function Quotes() {
    const { quotes } = useLoaderData<typeof loader>();

    return (
        <main>
            <section>
                <header>
                    <h1>All quotes</h1>
                    {/* TODO: Add quote link */}
                </header>
                <Suspense>
                    <Await resolve={quotes}>
                        {(quotes) =>
                            quotes.map((quote) => (
                                <QuoteArticle key={quote.slug} quote={quote} />
                            ))
                        }
                    </Await>
                </Suspense>
            </section>
        </main>
    );
}
