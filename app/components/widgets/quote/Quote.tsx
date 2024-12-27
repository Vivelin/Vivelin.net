import { CreativeWork } from './CreativeWork';

export type Quote = {
    id: number;
    slug: string;
    text: string;
    context?: string;
    sourceId: number;
    source: CreativeWork;
    exampleId?: number;
    example?: CreativeWork;
    page?: number;
    location?: number;
    timestamp?: string;
};
