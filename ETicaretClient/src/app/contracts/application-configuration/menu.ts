export class Menu{
    name:string;
    actions: Actions[];
}
export class Actions{
    actionType: string;
    httpType: string;
    definition: string;
    code: string;
}