import {
    TextField as AriaTextField,
    TextArea as AriaTextArea,
    Label as AriaLabel,
    Input as AriaInput,
    FieldError as AriaFieldError,
    Text as AriaText,
} from 'react-aria-components';
import type {
    TextFieldProps as AriaTextFieldProps,
    ValidationResult,
} from 'react-aria-components';

export type TextFieldProps = AriaTextFieldProps & {
    label: string;
    description?: string;
    multiLine?: boolean;
    errorMessage?: string | ((validation: ValidationResult) => string);
};

export default function TextField({
    label,
    description,
    multiLine,
    errorMessage,
    ...props
}: TextFieldProps) {
    return (
        <AriaTextField {...props}>
            <AriaLabel>{label}</AriaLabel>
            {description && (
                <AriaText slot="description">{description}</AriaText>
            )}
            {multiLine ?
                <AriaTextArea />
            :   <AriaInput />}
            <AriaFieldError>{errorMessage}</AriaFieldError>
        </AriaTextField>
    );
}
