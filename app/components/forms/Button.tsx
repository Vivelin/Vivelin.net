import { Button as AriaButton } from 'react-aria-components';
import type { ButtonProps as AriaButtonProps } from 'react-aria-components';

export type ButtonProps = AriaButtonProps;

export default function Button(props: ButtonProps) {
    return <AriaButton {...props} />;
}
