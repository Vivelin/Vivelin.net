export class BackendError extends Error {}

export class FieldRequiredError extends Error {
    public field: string;

    public constructor(field: string) {
        super(`${field} is required`);

        this.field = field;
    }
}
