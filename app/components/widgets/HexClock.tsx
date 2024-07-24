import { useState } from 'react';
import HexTime, { HexTimeProps } from './HexTime';
import { useTimer } from '~/hooks/useTimer';

export default function HexClock(props: Omit<HexTimeProps, 'date'>) {
    const [date, setDate] = useState(new Date());
    useTimer(() => {
        setDate(new Date());
    }, 1318);

    return <HexTime suppressHydrationWarning date={date} {...props} />;
}
