import { CreativeWork } from './CreativeWork';

export type Game = CreativeWork & {
    type: 'game';
    igdbId?: number;
};

export function isGame(work: CreativeWork): work is Game {
    return 'type' in work && work.type === 'game';
}
