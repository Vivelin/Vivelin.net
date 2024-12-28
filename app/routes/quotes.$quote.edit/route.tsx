import { ActionFunctionArgs } from '@remix-run/node';
import {
    Form,
    json,
    Link,
    redirect,
    useActionData,
    useLoaderData,
    useRouteLoaderData,
} from '@remix-run/react';
import Button from '~/components/forms/Button';
import TextField from '~/components/forms/TextField';
import { Quote } from '~/components/widgets/quote/Quote';
import { loader as quotePageLoader } from '../quotes.$quote/route';
import { FieldRequiredError } from '~/errors';

type QuoteRequest = Omit<Quote, 'source' | 'example'>;

export async function action({ request }: ActionFunctionArgs) {
    try {
        const formData = await request.formData();
        const slug = required(formData, 'slug');
        // TODO: Collect errors and return all errors at once
        const quote: QuoteRequest = {
            id: parseInt(required(formData, 'id'), 10),
            slug: required(formData, 'new-slug'),
            context: optional(formData, 'quote-context'),
            text: required(formData, 'quote-text'),
            sourceId: parseInt(required(formData, 'quote-source'), 10),
            exampleId:
                parseInt(optional(formData, 'quote-example')!, 10) || undefined,
            page: parseInt(optional(formData, 'quote-page')!, 10) || undefined,
            location:
                parseInt(optional(formData, 'quote-location')!, 10) ||
                undefined,
            timestamp: optional(formData, 'quote-timestamp') || undefined,
        };

        console.log(slug, quote);

        // sendApiRequest with body

        return redirect(`/quotes`);
    } catch (err) {
        if (err instanceof FieldRequiredError) {
            return json({ error: err.message, errorField: err.field });
        } else {
            throw new Error('quotes.$quote.edit: Unexpected error in action', {
                cause: err,
            });
        }
    }

    function required(formData: FormData, name: string): string {
        const value = formData.get(name);
        if (typeof value !== 'string') {
            throw new FieldRequiredError(name);
        }

        return value;
    }

    function optional(formData: FormData, name: string): string | undefined {
        const value = formData.get(name);
        if (value && typeof value !== 'string') {
            throw new Error(
                `Expected form data with key '${name}' to be a string, but got '${typeof value}'.`,
            );
        }

        return value ?? undefined;
    }
}

export default function QuotePageEdit() {
    const actionData = useActionData<typeof action>();
    const parentData = useRouteLoaderData<typeof quotePageLoader>(
        'routes/quotes.$quote',
    );

    if (!parentData?.quote) {
        return <p>Quote not found.</p>; // Callout?
    }

    const quote = parentData.quote;
    return (
        <Form method="post">
            <input type="hidden" name="id" value={quote.id} />
            <input type="hidden" name="slug" value={quote.slug} />

            {/* Callout */}
            <p>{actionData?.error}</p>

            <TextField
                label="Slug (required)"
                description="A short, URL-friendly text that is unique to this quote."
                name="new-slug"
                isRequired
                defaultValue={quote.slug}
            />
            <TextField
                label="Context"
                description="Optional. Who is talking in the quote? What else is needed to understand it?"
                name="quote-context"
                defaultValue={quote.context}
            />
            {/* Select source with all works */}
            {/* Select example with all works */}
            <TextField
                multiLine
                label="Quote"
                description="The quote itself. Supports HTML and Markdown."
                name="quote-text"
                defaultValue={quote.text}
            />
            <TextField
                label="Page number"
                description="Optional. The number of the page where the quote can be found."
                name="quote-page"
                inputMode="numeric"
                pattern="\d+"
                defaultValue={quote.page?.toString()}
            />
            <TextField
                label="Location"
                description="Optional. The location instead of page number for e-readers such as Kindle."
                name="quote-location"
                inputMode="numeric"
                pattern="\d+"
                defaultValue={quote.location?.toString()}
            />
            <TextField
                label="Timestamp"
                description="Optional. The time during a video at which the quote can be heard."
                name="quote-timestamp"
                inputMode="numeric"
                defaultValue={quote.timestamp}
            />

            <div className="button-group">
                <Button type="submit">Save</Button>
                <Link to="..">Discard changes</Link>
            </div>
        </Form>
    );
}
