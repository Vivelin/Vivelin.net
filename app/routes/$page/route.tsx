import { LoaderFunctionArgs } from '@remix-run/node';
import { json, MetaFunction, useLoaderData } from '@remix-run/react';

export async function loader({ params }: LoaderFunctionArgs) {
    const page = params.page;
    // ...
    return json({ page }); // TODO: I shouldn't just reply with the input page here. If the page doesn't exist, we should simply throw a 404 here.
}

export const meta: MetaFunction<typeof loader> = ({ data }) => {
    if (!data) return [{ title: 'Vivelin.net' }];

    return [{ title: `${data.page} — Vivelin.net` }];
};

export default function Page() {
    const { page } = useLoaderData<typeof loader>();
    return (
        <main>
            <h1>{page}</h1>
        </main>
    );
}
