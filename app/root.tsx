import {
    Await,
    json,
    Links,
    Meta,
    MetaFunction,
    Outlet,
    Scripts,
    ScrollRestoration,
    useRouteLoaderData,
} from '@remix-run/react';
import { Suspense } from 'react';
import { sendApiRequest } from './apiClient.server';
import BlockQuote from './components/widgets/quote/BlockQuote';
import { Quote } from './components/widgets/quote/Quote';
import Clock from './components/widgets/time/Clock';

export const meta: MetaFunction = () => {
    // Note: prefer adding meta tags as everything here will be lost if a route has its own meta export.
    // The title is just here for routes that don't have a meta export.
    return [{ title: 'Vivelin.net' }];
};

export async function loader() {
    const qotd = await sendApiRequest<Quote>('GET', '/quotes/random');

    return json({
        quote: qotd,
    });
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
