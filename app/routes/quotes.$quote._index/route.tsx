import { Link, useRouteLoaderData } from '@remix-run/react';
import BlockQuote from '~/components/widgets/quote/BlockQuote';
import { loader as quotePageLoader } from '../quotes.$quote/route';

export default function QuotePageDefault() {
    const data = useRouteLoaderData<typeof quotePageLoader>(
        'routes/quotes.$quote',
    );

    if (!data?.quote) {
        return <p>Quote not found.</p>; // Callout?
    }

    return (
        <section>
            <BlockQuote quote={data.quote} />
            <footer>
                <Link to="edit">Edit</Link>
            </footer>
        </section>
    );
}
