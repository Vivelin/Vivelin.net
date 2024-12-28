import { Link } from '@remix-run/react';
import BlockQuote from '~/components/widgets/quote/BlockQuote';
import { Quote } from '~/components/widgets/quote/Quote';

type QuoteArticleProps = {
    quote: Quote;
};

export default function QuoteArticle({ quote }: QuoteArticleProps) {
    return (
        <article>
            <header>
                <div className="heading">
                    <h2>Quote {quote.id}</h2>
                    <small className="slug">
                        <Link to={quote.slug}>#{quote.slug}</Link>
                    </small>
                </div>
                {/* TODO: Edit quote link */}
            </header>
            <BlockQuote quote={quote} />
        </article>
    );
}
