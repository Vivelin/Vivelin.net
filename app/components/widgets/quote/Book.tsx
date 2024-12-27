import { CreativeWork } from './CreativeWork';

export type Book = CreativeWork & {
    type: 'book';
    isbn?: string;
};

export function isBook(work: CreativeWork): work is Book {
    return 'type' in work && work.type === 'book';
}
