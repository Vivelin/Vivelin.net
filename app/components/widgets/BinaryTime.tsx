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
    className = 'binaryClock',
    ...props
}: BinaryTimeProps) {
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const seconds = date.getSeconds();

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
