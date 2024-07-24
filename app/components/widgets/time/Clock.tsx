import { useState } from 'react';
import HexClock from './HexClock';
import { BinaryClock } from './BinaryClock';
import { UnixClock } from './UnixClock';

export default function Clock() {
    const [mode, setMode] = useState(0);
    const clocks = [
        <BinaryClock key={0} />,
        <BinaryClock utc key={1} />,
        <HexClock key={2} />,
        <HexClock utc key={3} />,
        <UnixClock key={4} />,
    ];

    return (
        <div
            className="clock"
            onClick={() => setMode((x) => (x + 1) % clocks.length)}
        >
            {clocks[mode]}
        </div>
    );
}
