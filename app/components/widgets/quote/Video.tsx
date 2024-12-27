import { CreativeWork } from './CreativeWork';

export type Video = CreativeWork & {
    type: 'video';
    season?: number;
    episode?: number;
};

export function isVideo(work: CreativeWork): work is Video {
    return 'type' in work && work.type === 'video';
}
