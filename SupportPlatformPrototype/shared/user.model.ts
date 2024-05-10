import { tenant } from "./tenant.model";
import { Ticket } from "./ticket.model";
import { Role}   from "./Role.model"
import { UserRoles } from "./UserRoles.model";

export class User {
      public userID!: string;
      public username: string = "";
      public email:string ="";
      public password: string = "";
      public tenant?: tenant;
      public tickets?: Ticket[];
      public roles!: UserRoles[];
        
      
  }
  