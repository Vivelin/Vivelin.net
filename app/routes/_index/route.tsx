import type { LinksFunction, MetaFunction } from '@remix-run/node';
import { Link, useSearchParams } from '@remix-run/react';
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
                <Link to={{ search: 'mode=online' }} replace preventScrollReset>
                    Vivelin
                </Link>
                <span> / </span>
                <Link
                    to={{ search: 'mode=personal' }}
                    replace
                    preventScrollReset
                >
                    Laura Verdoes
                </Link>
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
