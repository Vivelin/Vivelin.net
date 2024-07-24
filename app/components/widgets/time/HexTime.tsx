import { HTMLAttributes } from 'react';
import { getTimeFloat, MinutesPerDay } from '~/util/dates';

export type HexTimeProps = {
    date?: Date;
    utc?: boolean;
} & HTMLAttributes<HTMLDivElement>;

export default function HexTime({
    date = new Date(),
    utc = false,
    ...props
}: HexTimeProps) {
    const time = getTimeFloat(date, utc);
    const hexTime = time.toString(16).slice(1, 6).toUpperCase();

    const offset = utc ? 0 : -date.getTimezoneOffset();
    const offsetSign = Math.sign(offset);
    const hexOffset = Math.abs(offset / MinutesPerDay)
        .toString(16)
        .slice(1, 6)
        .toUpperCase();

    return (
        <div className="hexClock" {...props}>
            <span className="time">{hexTime}</span>
            {!utc && (
                <span className="offset">
                    {' '}
                    {offsetSign === -1 ? '-' : '+'}
                    {hexOffset}
                </span>
            )}
        </div>
    );
}
