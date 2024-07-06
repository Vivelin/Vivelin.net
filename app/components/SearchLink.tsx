import {
    Link,
    LinkProps,
    useResolvedPath,
    useSearchParams,
} from '@remix-run/react';
import classNames from 'classnames';

export type SearchLinkProps = Omit<LinkProps, 'to'> & {
    to?: LinkProps['to'];
    searchTo: Record<string, string>;
};

export function SearchLink({
    to: baseTo,
    searchTo,
    children,
    ...props
}: SearchLinkProps) {
    const [activeSearchParams] = useSearchParams();
    const { pathname, search, hash } = useResolvedPath(baseTo ?? '');
    const newSearchParams = new URLSearchParams(search);

    let isActive: boolean = true;
    for (const key in searchTo) {
        if (Object.prototype.hasOwnProperty.call(searchTo, key)) {
            newSearchParams.set(key, searchTo[key]);
            if (searchTo[key] !== activeSearchParams.get(key)) {
                isActive = false;
            }
        }
    }

    return (
        <Link
            to={`${pathname}?${newSearchParams}${hash}`}
            className={classNames({ active: isActive })}
            {...props}
        >
            {children}
        </Link>
    );
}
