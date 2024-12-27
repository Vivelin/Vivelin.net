import { durationToSeconds, parseDuration } from '~/util/duration';
import { CreativeWork } from './CreativeWork';
import { Quote } from './Quote';
import { isVideo } from './Video';
import './citation.css';

type CitationProps = {
    work: CreativeWork;
    quote: Quote;
};

function Cite({ work, quote }: CitationProps) {
    const content = (
        <>
            {work.title && <span className="title">“{work.title}”</span>}
            {work.title && work.series && ', '}
            {work.series && <span className="series">{work.series}</span>}
        </>
    );

    let uri = work.uri;
    if (
        uri &&
        quote.timestamp &&
        uri.match(/^https?:\/\/(www\.)?(youtu\.be|youtube\.com)\//)
    ) {
        const duration = parseDuration(quote.timestamp);
        if (duration) {
            const seconds = durationToSeconds(duration);

            const newUri = new URL(uri);
            newUri.searchParams.set('t', `${seconds}s`);
            uri = newUri.toString();
        }
    }

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

export default function Citation({ work, quote }: CitationProps) {
    return (
        <span className="citation">
            {work.author && <span className="author">{work.author}</span>}

            {work.author && (work.title || work.series) && ', '}
            <Cite work={work} quote={quote} />

            {isVideo(work) ?
                <>
                    {(work.author || work.title || work.series) && ' '}
                    {work.season && (
                        <span className="season">Season {work.season}</span>
                    )}
                    <span className="episode">Episode {work.episode}</span>
                </>
            :   undefined}

            {(work.publisher || work.publicationDate) && (
                <span className="publisher">
                    {' '}
                    ({[work.publisher, work.publicationDate].join(', ')})
                </span>
            )}
        </span>
    );
}
