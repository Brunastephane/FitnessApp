export interface Users {
    id: number;
    name: string;
    email: string;
    password: string;
    profile?: {
        id: string;
        dateOfBirth: string;
        height: number;
        weight: number;
        activityLevel: string;
        gender: string;
    };
}
