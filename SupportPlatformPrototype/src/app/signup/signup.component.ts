import { Component } from '@angular/core';
import { User } from '../../../shared/user.model';
import { MessageService } from 'primeng/api';
import { UserService } from '../../../shared/user-service.service';
import { Router } from '@angular/router';
import { SessionService } from '../../../utils/SessionService';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {

  constructor(public serviceM: MessageService, public serviceUser: UserService, private router: Router, public ServiceS: SessionService ) { 
    
  } // Corrected variable names

  user: User = new User(); // Added parentheses to instantiate the User object
  buttonSeverity: string = 'primary'; // Default severity is success

  verif() {
    if ((this.user.email === "" &&  this.user.username === "") || this.user.password === "") {
      this.serviceM.add({ severity: 'error', summary: 'Champs obligatoires', detail: 'All fields are mandatory.' });
    } else {
      // Make HTTP POST request to the C# backend API endpoint
      this.serviceUser.login(this.user).subscribe({
        next: (response) => {
          console.log(response);
          if (response === "User was not found") {
            this.serviceM.add({ severity: 'error', summary: 'Error', detail: 'User was not found' });
            this.buttonSeverity = 'danger'; // Change button severity to danger
          } else if (response === "wrong password") {
            this.serviceM.add({ severity: 'error', summary: 'Error', detail: 'Wrong Password.' });
            this.buttonSeverity = 'danger'; // Change button severity to danger
          } else {
            // Assuming the response contains the JWT token
            // Store the token securely and perform necessary actions
            
            this.serviceUser.storeToken(response);
            
            this.ServiceS.sessionStart(this.user);
            this.router.navigate(['/Tenant']);
            this.ServiceS.sessionStart(this.user)
          }
        },
        error: (error) => {
          console.error(error);
          this.serviceM.add({ severity: 'error', summary: 'Error', detail: 'Connection to the server failed.' });
          this.buttonSeverity = 'danger'; // Change button severity to danger
        }
      });
    }
  }


  


}
