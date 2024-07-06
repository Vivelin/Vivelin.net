import type { LinksFunction, MetaFunction } from '@remix-run/node';
import { useSearchParams } from '@remix-run/react';
import { SearchLink } from '~/components/SearchLink';
import { constrain } from '~/util/strings';
import styles from './styles.css?url';

export const links: LinksFunction = () => [{ rel: 'stylesheet', href: styles }];

export const meta: MetaFunction = () => {
    return [{ title: 'Vivelin.net' }];
};

export default function Index() {
    const [search] = useSearchParams();
    const mode =
        constrain(search.get('mode'), ['personal', 'online']) ?? 'online';

    return (
        <main>
            <h1>
                <SearchLink
                    searchTo={{ mode: 'online' }}
                    replace
                    preventScrollReset
                >
                    Vivelin
                </SearchLink>
                <span> / </span>
                <SearchLink
                    searchTo={{ mode: 'personal' }}
                    replace
                    preventScrollReset
                >
                    Laura Verdoes
                </SearchLink>
            </h1>
            {mode === 'online' ?
                <article>
                    <p>Text about me online goes here.</p>
                </article>
            :   <article>
                    <p>Text about me in person goes here.</p>
                </article>
            }
        </main>
    );
}

