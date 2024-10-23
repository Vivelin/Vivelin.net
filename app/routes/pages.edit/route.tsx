import { ActionFunctionArgs, json, LoaderFunctionArgs } from '@remix-run/node';
import { sendApiRequest } from '~/apiClient.server';
import { Page } from '../$/page';
import { BackendError } from '~/errors';
import { Form, redirect, useLoaderData } from '@remix-run/react';
import TextField from '~/components/forms/TextField';
import Button from '~/components/forms/Button';

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

export async function action({ request }: ActionFunctionArgs) {
    const formData = await request.formData();

    const originalSlug = formData.get('page');

    return redirect(`/${originalSlug}`);
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

            <Form method="POST">
                <input type="hidden" name="page" value={page.slug} />
                <TextField
                    label="Title"
                    name="title"
                    defaultValue={page.title}
                />
                <TextField label="Slug" name="slug" defaultValue={page.slug} />
                <TextField
                    multiLine
                    label="Description"
                    name="description"
                    defaultValue={page.description}
                />
                <TextField
                    multiLine
                    label="Content"
                    name="content"
                    defaultValue={page.content}
                />
                <Button type="submit">Save</Button>
            </Form>
        </main>
    );
}
