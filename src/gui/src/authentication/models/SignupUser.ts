export default class SignupUser {
    firstName?: string | null;
    lastName?: string | null;
    email: string;
    password: string;
    passwordConfirmation: string;

    public constructor(
        firstName: string, lastName: string, email: string, password: string, passwordConfirmation: string
    ) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.passwordConfirmation = passwordConfirmation;
    }
}