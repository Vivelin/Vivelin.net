import { Form, Link, useRouteLoaderData } from '@remix-run/react';
import Button from '~/components/forms/Button';
import TextField from '~/components/forms/TextField';
import { loader as quotePageLoader } from '../quotes.$quote/route';

export default function QuotePageEdit() {
    const data = useRouteLoaderData<typeof quotePageLoader>(
        'routes/quotes.$quote',
    );

    if (!data?.quote) {
        return <p>Quote not found.</p>; // Callout?
    }

    const quote = data.quote;
    return (
        <Form>
            <input type="hidden" name="id" value={quote.id} />
            <input type="hidden" name="slug" value={quote.slug} />

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
