import { Duration } from 'date-fns';

export function parseDuration(value: string): Duration | undefined {
    const matches = value.match(
        /((\d+):)?(\d{2,}):(\d{2,}):(\d{2,})(\.(\d+))?/,
    );

    if (matches) {
        const [, , dd, hh, mm, ss] = matches;
        return {
            days: parseInt(dd, 10) || undefined,
            hours: parseInt(hh, 10),
            minutes: parseInt(mm, 10),
            seconds: parseInt(ss, 10),
        };
    }

    return undefined;
}

export function durationToSeconds(duration: Duration) {
    return (
        (duration.seconds ?? 0) +
        60 *
            ((duration.minutes || 0) +
                60 * ((duration.hours || 0) + 24 * (duration.days || 0)))
    );
}
