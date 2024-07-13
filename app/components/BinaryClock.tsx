import { useState } from 'react';
import { useTimer } from '~/hooks/useTimer';
import { BinaryTime, BinaryTimeProps } from './BinaryTime';

/**
 * Displays an automatically-updated binary clock with separate hours, minutes
 * and seconds components.
 */
export function BinaryClock({ ...props }: Omit<BinaryTimeProps, 'date'>) {
    const [date, setDate] = useState(new Date());
    useTimer(() => {
        setDate(new Date());
    }, 1000);

    return <BinaryTime date={date} {...props} />;
}
