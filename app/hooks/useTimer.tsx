import { useEffect } from 'react';

export function useTimer(callback: () => void, interval: number) {
    useEffect(() => {
        const id = window.setInterval(callback, interval);

        return () => {
            window.clearInterval(id);
        };
    }, [callback, interval]);
}
