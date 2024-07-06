import type { MetaFunction } from '@remix-run/node';
import { Link, useSearchParams } from '@remix-run/react';
import classNames from 'classnames';
import { SearchLink } from '~/components/SearchLink';
import { constrain } from '~/util/strings';

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
        </main>
    );
}

