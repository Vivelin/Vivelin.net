import { LoaderFunctionArgs } from '@remix-run/node';
import { json, MetaFunction, useLoaderData } from '@remix-run/react';
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

    return (
        <main>
            <article dangerouslySetInnerHTML={{ __html: page.content }} />
        </main>
    );
}
