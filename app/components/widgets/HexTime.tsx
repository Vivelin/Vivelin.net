import { differenceInMilliseconds, startOfDay } from 'date-fns';
import { HTMLAttributes } from 'react';

const msPerDay = 86400000;

export type HexTimeProps = {
    date?: Date;
} & HTMLAttributes<HTMLDivElement>;

export default function HexTime({ date = new Date(), ...props }: HexTimeProps) {
    const dayStart = startOfDay(date);
    const elapsedMs = differenceInMilliseconds(date, dayStart);
    const time = elapsedMs / msPerDay;
    const hexTime = time.toString(16).slice(1, 6).toUpperCase();

    return (
        <div className="hex-clock" {...props}>
            <span>{hexTime}</span>
        </div>
    );
}
