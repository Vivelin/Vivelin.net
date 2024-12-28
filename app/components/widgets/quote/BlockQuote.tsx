import Markdown from 'react-markdown';
import Citation from './Citation';
import { Quote } from './Quote';
import rehypeRaw from 'rehype-raw';
import classNames from 'classnames';

type BlockQuoteProps = {
    quote: Quote | undefined;
};

export default function BlockQuote({ quote }: BlockQuoteProps) {
    if (!quote) {
        return null;
    }

    return (
        <figure className={classNames('blockquote', quote.slug)}>
            <blockquote>
                <Markdown rehypePlugins={[rehypeRaw]}>{quote.text}</Markdown>
            </blockquote>
            <figcaption>
                <p>
                    —{' '}
                    {quote.context && (
                        <span className="context">{quote.context}. </span>
                    )}
                    <Citation work={quote.source} quote={quote} />
                </p>
                {quote.example && (
                    <p>
                        See: <Citation work={quote.example} quote={quote} />
                    </p>
                )}
            </figcaption>
        </figure>
    );
}
