import { CreativeWork } from './CreativeWork';

export type Song = CreativeWork & {
    type: 'song';
};

export function isSong(work: CreativeWork): work is Song {
    return 'type' in work && work.type === 'song';
}
