/**
 * Constrains a string value to be one of a set of strings.
 * @param value The value to constrain.
 * @param oneOf An array of strings that `value` is allowed to be.
 * @returns `value`, or `undefined` if it is not in `oneOf`.
 */
export function constrain(
    value: string | null | undefined,
    oneOf: string[],
): string | undefined {
    if (typeof value === 'string' && oneOf.includes(value)) {
        return value;
    }

    return undefined;
}
