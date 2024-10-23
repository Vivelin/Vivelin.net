import { MetaDescriptor } from '@remix-run/react';

export type MetaData = {
    description: string;
    publishedOn?: string | Date | undefined;
    updatedOn?: string | Date | undefined;
    expiresOn?: string | Date | undefined;
};

export function* getMetaTags(
    title: string,
    meta: MetaData,
): Generator<MetaDescriptor> {
    yield { title: `${title} — Vivelin.net` };
    yield { name: 'description', content: meta.description };

    const publishDate =
        meta.publishedOn ? new Date(meta.publishedOn) : undefined;
    const updateDate = meta.updatedOn ? new Date(meta.updatedOn) : undefined;
    const expireDate = meta.expiresOn ? new Date(meta.expiresOn) : undefined;

    // OpenGraph: https://ogp.me/
    yield { name: 'og:site_name', content: 'Vivelin.net' };
    yield { name: 'og:locale', content: 'en_US' };
    yield { name: 'og:title', content: title };
    yield { name: 'og:description', content: meta.description };
    yield { name: 'og:type', content: 'article' };
    yield { name: 'article:profile:first_name', content: 'Laura' };
    yield { name: 'article:profile:last_name', content: 'Verdoes' };
    yield { name: 'article:profile:username', content: 'Vivelin' };
    yield { name: 'article:profile:gender', content: 'female' };

    if (publishDate) {
        yield {
            name: 'article:published_time',
            content: publishDate?.toISOString(),
        };
    }

    if (updateDate) {
        yield {
            name: 'article:modified_time',
            content: updateDate?.toISOString(),
        };
    }

    if (expireDate) {
        yield {
            name: 'article:expiration_time',
            content: expireDate?.toISOString(),
        };
    }
}
