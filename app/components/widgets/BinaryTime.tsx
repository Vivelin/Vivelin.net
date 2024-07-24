import { HTMLAttributes } from 'react';
import { BinaryDisplay } from './BinaryDisplay';

export type BinaryTimeProps = {
    date?: Date;
    utc?: boolean;
} & HTMLAttributes<HTMLDivElement>;

/**
 * Displays the local time with the hours, minutes and seconds components as binary numbers.
 */
export function BinaryTime({
    date = new Date(),
    utc = false,
    className = 'binaryClock',
    ...props
}: BinaryTimeProps) {
    const hours = utc ? date.getUTCHours() : date.getHours();
    const minutes = utc ? date.getUTCMinutes() : date.getMinutes();
    const seconds = utc ? date.getUTCSeconds() : date.getSeconds();

    return (
        <div className={className} {...props}>
            <div className="hours">
                <BinaryDisplay value={hours} length={6} on="●" off="○" />
            </div>
            <div className="minutes">
                <BinaryDisplay value={minutes} length={6} on="●" off="○" />
            </div>
            <div className="seconds">
                <BinaryDisplay value={seconds} length={6} on="●" off="○" />
            </div>
        </div>
    );
}
