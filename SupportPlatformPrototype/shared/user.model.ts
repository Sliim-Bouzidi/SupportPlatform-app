import { tenant } from "./tenant.model";
import { Ticket } from "./ticket.model";
import { Role}   from "./Role.model"
export class User {

      public username: string = "";
      public email:string ="";
      public password: string = "";
      public tenant?: tenant;
      public tickets?: Ticket[];
      public roles!: Role[];
    
      
  }
  