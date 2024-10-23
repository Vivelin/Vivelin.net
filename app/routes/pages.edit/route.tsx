import { json, LoaderFunctionArgs } from '@remix-run/node';
import { sendApiRequest } from '~/apiClient.server';
import { Page } from '../$/page';
import { BackendError } from '~/errors';
import { useLoaderData } from '@remix-run/react';

export async function loader({ request }: LoaderFunctionArgs) {
    const url = new URL(request.url);
    const slug = url.searchParams.get('page');

    const page = await sendApiRequest<Page>('GET', `/pages/${slug}`);
    if (!page) {
        throw new BackendError(
            `Unexpected empty response from GET /pages/${slug}`,
        );
    }

    return json({ page });
}

export default function EditPage() {
    const { page } = useLoaderData<typeof loader>();

    return (
        <main>
            <hgroup>
                <h1>
                    Editing <i>{page.title}</i>
                </h1>
                <p>
                    <code>{page.slug}</code>
                </p>
            </hgroup>
        </main>
    );
}
