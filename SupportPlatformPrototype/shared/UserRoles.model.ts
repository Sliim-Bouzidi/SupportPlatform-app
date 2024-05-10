import { User } from './user.model';
import { Role } from './Role.model';

export class UserRoles {
  public userRolesID!: string;
  public roleValue!: string;
  public user?: User;
  public role?: Role;
}
