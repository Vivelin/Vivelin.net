import { json, LoaderFunctionArgs } from '@remix-run/node';
import { Link, Outlet, useLoaderData } from '@remix-run/react';
import { sendApiRequest } from '~/apiClient.server';
import { Quote } from '~/components/widgets/quote/Quote';

export async function loader({ params }: LoaderFunctionArgs) {
    const quote = await sendApiRequest<Quote>('GET', `quotes/${params.quote}`);
    return json({ quote });
}

export default function QuotePage() {
    const { quote } = useLoaderData<typeof loader>();

    return (
        <main>
            <article>
                <header>
                    <nav>
                        <Link to=".." relative="path">
                            Back to all quotes
                        </Link>
                    </nav>

                    <h1>Quote {quote?.id}</h1>
                </header>
                <Outlet />
            </article>
        </main>
    );
}
