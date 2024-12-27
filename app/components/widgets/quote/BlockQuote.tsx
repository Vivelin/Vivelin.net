import Markdown from 'react-markdown';
import Citation from './Citation';
import { Quote } from './Quote';
import rehypeRaw from 'rehype-raw';

type BlockQuoteProps = {
    quote: Quote | undefined;
};

export default function BlockQuote({ quote }: BlockQuoteProps) {
    if (!quote) {
        return null;
    }

    return (
        <figure>
            <blockquote>
                <Markdown rehypePlugins={[rehypeRaw]}>{quote.text}</Markdown>
            </blockquote>
            <figcaption>
                <p>
                    — <span className="context">{quote.context}. </span>
                    <Citation work={quote.source} />
                </p>
                {quote.example && (
                    <p>
                        See: <Citation work={quote.example} />
                    </p>
                )}
            </figcaption>
        </figure>
    );
}
