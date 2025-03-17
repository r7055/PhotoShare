export class UserRegister {
    constructor(
        public firstName: string,
        public lastName: string,
        public email: string,
        public passwordHash: string,
        // public birthDate: Date
    ) { }
}

export class UserLogin {
    constructor(
        public email: string,
        public password: string
    ) { }
}
