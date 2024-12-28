import classNames from 'classnames';
import { formatDuration } from 'date-fns';
import { ReactNode } from 'react';
import { durationToSeconds, parseDuration } from '~/util/duration';
import { CreativeWork } from './CreativeWork';
import { Quote } from './Quote';
import { isVideo } from './Video';
import './citation.css';

type CitationProps = {
    work: CreativeWork;
    quote: Quote;
};

function shouldApplyLocation(work: CreativeWork, quote: Quote): boolean {
    if (quote.source && quote.example) {
        // If a quote has both a source and an example, page/location should only apply to the source
        return work.id === quote.sourceId;
    }

    return true;
}

function shouldApplyTimestamp(work: CreativeWork, quote: Quote): boolean {
    if (quote.source && quote.example) {
        // If a quote has both a source and an example, timestamps should only apply to the example
        return work.id === quote.exampleId;
    }

    return true;
}

function formatUrl(work: CreativeWork, quote: Quote): string | undefined {
    const uri = work.uri;

    if (
        uri && // is there a URI to format?
        shouldApplyTimestamp(work, quote) && // should we format it?
        quote.timestamp && // do we have something to format it with?
        uri.match(/^https?:\/\/(www\.)?(youtu\.be|youtube\.com)\//) // can we format it?
    ) {
        const duration = parseDuration(quote.timestamp);
        if (duration) {
            const seconds = durationToSeconds(duration);

            const newUri = new URL(uri);
            newUri.searchParams.set('t', `${seconds}s`);
            return newUri.toString();
        }
    }

    return uri;
}

function Author({ work }: CitationProps) {
    if (!work.author) {
        return null;
    }

    return <span className="author">{work.author}</span>;
}

function Cite({ work, quote }: CitationProps) {
    const content = (
        <>
            {work.title && <span className="title">“{work.title}”</span>}
            {work.title && work.series && ', '}
            {work.series && <span className="series">{work.series}</span>}
        </>
    );

    const uri = formatUrl(work, quote);

    return (
        <cite>
            {uri ?
                <a href={uri} target="_blank" rel="noreferrer">
                    {content}
                </a>
            :   content}
        </cite>
    );
}

function Episode({ work }: CitationProps) {
    if (!isVideo(work)) {
        return null;
    }

    return (
        <>
            {work.season && (
                <span className="season">Season {work.season}</span>
            )}
            <span className="episode">Episode {work.episode}</span>
        </>
    );
}

function Publisher({ work }: CitationProps) {
    if (!work.publisher && !work.publicationDate) {
        return null;
    }

    return (
        <span className="publisher">
            ({[work.publisher, work.publicationDate].join(', ')})
        </span>
    );
}

function Location({ work, quote }: CitationProps) {
    if (shouldApplyLocation(work, quote)) {
        if (quote.page) {
            return <span className="page">p. {quote.page}</span>;
        } else if (quote.location) {
            return <span className="location">location {quote.location}</span>;
        }
    } else if (quote.timestamp && shouldApplyTimestamp(work, quote)) {
        const didFormat = formatUrl(work, quote) !== work.uri;
        if (!didFormat) {
            const duration = parseDuration(quote.timestamp);
            if (duration) {
                return (
                    <span className="timestamp">
                        {formatDuration(duration)}
                    </span>
                );
            }
        }
    }

    return null;
}

export default function Citation(props: CitationProps) {
    const parts = [
        { component: Author },
        { component: Cite, separator: ', ' },
        { component: Episode, separator: ' ' },
        { component: Publisher, separator: ' ' },
        { component: Location, separator: ', ' },
    ];

    return (
        <span className={classNames('citation', props.work.slug)}>
            {parts.reduce<ReactNode>((accumulator, next) => {
                const children = next.component(props);
                if (!children) {
                    return accumulator;
                }

                return accumulator ?
                        <>
                            {accumulator}
                            {next.separator}
                            {children}
                        </>
                    :   children;
            }, null)}
        </span>
    );
}
