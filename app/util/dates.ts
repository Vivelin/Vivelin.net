import { UTCDateMini } from '@date-fns/utc';
import { differenceInMilliseconds, startOfDay } from 'date-fns';

export const MillisecondsPerDay = 86400000;
export const SecondsPerDay = 86400;
export const MinutesPerDay = 1440;

export function getTimeFloat(date = new Date(), utc = false) {
    const dateInternal = utc ? new UTCDateMini(date) : date;
    const dayStart = startOfDay(dateInternal);
    const elapsedMs = differenceInMilliseconds(dateInternal, dayStart);
    return elapsedMs / MillisecondsPerDay;
}
