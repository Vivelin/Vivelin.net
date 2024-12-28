import {
    json,
    Links,
    Meta,
    MetaFunction,
    Outlet,
    Scripts,
    ScrollRestoration,
    useRouteLoaderData,
} from '@remix-run/react';
import { sendApiRequest } from './apiClient.server';
import BlockQuote from './components/widgets/quote/BlockQuote';
import { Quote } from './components/widgets/quote/Quote';
import Clock from './components/widgets/time/Clock';
import './root.css';

export const meta: MetaFunction = () => {
    // Note: prefer adding meta tags as everything here will be lost if a route has its own meta export.
    // The title is just here for routes that don't have a meta export.
    return [{ title: 'Vivelin.net' }];
};

export async function loader() {
    try {
        const qotd = await sendApiRequest<Quote>('GET', '/quotes/random'); // TODO: change back to qotd once sufficiently tested

        return json({
            quote: qotd,
        });
    } catch (err) {
        console.error('root#loader: Caught unexpected', err);
        return json({ quote: undefined });
    }
}

export function Layout({ children }: { children: React.ReactNode }) {
    const data = useRouteLoaderData<typeof loader>('root');

    return (
        <html lang="en-US">
            <head>
                <meta charSet="utf-8" />
                <meta
                    name="viewport"
                    content="width=device-width, initial-scale=1"
                />
                <Meta />
                <Links />
            </head>
            <body>
                <h1>Vivelin.net</h1>
                {children}
                <footer>
                    <Clock />
                    {data && <BlockQuote quote={data.quote} />}
                </footer>
                <ScrollRestoration />
                <Scripts />
            </body>
        </html>
    );
}

export default function App() {
    return <Outlet />;
}
