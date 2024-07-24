import { HTMLAttributes } from 'react';

export type UnixTimeProps = {
    date?: Date;
} & HTMLAttributes<HTMLDivElement>;

export default function UnixTime({
    date = new Date(),
    ...props
}: UnixTimeProps) {
    const time = Math.floor(date.getTime() / 1000);
    return (
        <div className="unixClock" {...props}>
            <span className="time">{time}</span>
        </div>
    );
}
