import { HTMLAttributes, ReactNode } from 'react';
import './binaryClock.css';

export type BinaryDisplayProps = {
    value: number;
    length: number;
    className?: string;
    on?: ReactNode;
    off?: ReactNode;
} & HTMLAttributes<HTMLSpanElement>;

/**
 * Displays a value as a binary number (most significant bit first) with separate elements for each bit.
 */
export function BinaryDisplay({
    value,
    length,
    on = '1',
    off = '0',
    className = 'binary',
    ...props
}: BinaryDisplayProps) {
    const bits = [...Array(length).keys()].map((x) => (value & (1 << x)) != 0);

    return (
        <span className={className} {...props}>
            {bits.reverse().map((bit, index) => (
                <Bit key={index} bit={bit} index={index} />
            ))}
        </span>
    );

    function Bit({ bit, index }: { bit: boolean; index: number }) {
        const n = length - index - 1;
        return (
            <span
                className="bit"
                data-bit-n={n}
                data-bit-decimal={Math.pow(2, n)}
                data-bit={bit ? 1 : 0}
            >
                {bit ? on : off}
            </span>
        );
    }
}
