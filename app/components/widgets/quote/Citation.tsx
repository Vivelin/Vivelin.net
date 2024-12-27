import { CreativeWork } from './CreativeWork';
import { isVideo } from './Video';
import './citation.css';

type CitationProps = {
    work: CreativeWork;
};

export default function Citation({ work }: CitationProps) {
    return (
        <span className="citation">
            {work.author && <span className="author">{work.author}</span>}

            {work.author && (work.title || work.series) && ', '}
            <cite>
                {work.title && <span className="title">“{work.title}”</span>}
                {work.title && work.series && ', '}
                {work.series && <span className="series">{work.series}</span>}
            </cite>

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
