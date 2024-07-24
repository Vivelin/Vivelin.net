import { useState } from 'react';
import { useTimer } from '~/hooks/useTimer';
import UnixTime, { UnixTimeProps } from './UnixTime';

/**
 * Displays an automatically-updated clock as Unix timestamp.
 */
export function UnixClock({ ...props }: Omit<UnixTimeProps, 'date'>) {
    const [date, setDate] = useState(new Date());
    useTimer(() => {
        setDate(new Date());
    }, 1000);

    return <UnixTime suppressHydrationWarning date={date} {...props} />;
}
