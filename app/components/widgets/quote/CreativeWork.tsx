export type CreativeWork = {
    id: number;
    slug: string;
    author?: string;
    title?: string;
    series?: string;
    publisher?: string;
    publicationDate?: string;
    uri?: string;
};

export function isOtherCreativeWork(work: CreativeWork): work is CreativeWork {
    return !('type' in work);
}
