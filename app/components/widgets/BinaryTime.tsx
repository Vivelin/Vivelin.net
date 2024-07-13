import { HTMLAttributes } from 'react';
import { BinaryDisplay } from './BinaryDisplay';
import styles from './binaryClock.module.css';

export type BinaryTimeProps = {
    date?: Date;
} & HTMLAttributes<HTMLDivElement>;

/**
 * Displays the local time with the hours, minutes and seconds components as binary numbers.
 */
export function BinaryTime({
    date = new Date(),
    className = styles.binaryClock,
    ...props
}: BinaryTimeProps) {
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const seconds = date.getSeconds();

    return (
        <div className={className} {...props}>
            <div className={styles.binaryHours}>
                <BinaryDisplay value={hours} length={5} />
            </div>
            <div className={styles.binaryMinutes}>
                <BinaryDisplay value={minutes} length={6} />
            </div>
            <div className={styles.binarySeconds}>
                <BinaryDisplay value={seconds} length={6} />
            </div>
        </div>
    );
}
