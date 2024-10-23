export type Page = {
    id: number;

    slug: string;

    title: string;

    content: string;

    description?: string;

    publishedOn?: Date;

    updatedOn?: Date;

    expiresOn?: Date;
};
