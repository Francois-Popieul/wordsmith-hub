export default class SignupUser {
    firstName?: string | null;
    lastName?: string | null;
    email: string;
    password: string;
    passwordConfirmation: string;

    public constructor(
        email: string, password: string, passwordConfirmation: string, firstName?: string, lastName?: string
    ) {
        this.firstName = firstName ?? null;
        this.lastName = lastName ?? null;
        this.email = email;
        this.password = password;
        this.passwordConfirmation = passwordConfirmation;
    }
}