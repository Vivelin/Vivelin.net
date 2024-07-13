import { HTMLAttributes } from 'react';
import { BinaryDisplay } from './BinaryDisplay';

export type BinaryTimeProps = {
    date?: Date;
} & HTMLAttributes<HTMLDivElement>;

/**
 * Displays the local time with the hours, minutes and seconds components as binary numbers.
 */
export function BinaryTime({
    date = new Date(),
    className = 'binary-clock',
    ...props
}: BinaryTimeProps) {
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const seconds = date.getSeconds();

    return (
        <div className={className} {...props}>
            <div className="binary-clock-hours">
                <BinaryDisplay value={hours} length={5} />
            </div>
            <div className="binary-clock-minutes">
                <BinaryDisplay value={minutes} length={6} />
            </div>
            <div className="binary-clock-seconds">
                <BinaryDisplay value={seconds} length={6} />
            </div>
        </div>
    );
}
