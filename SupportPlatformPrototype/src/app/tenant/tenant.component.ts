import { Component,OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { SessionService } from '../../../utils/SessionService';
import { TenantService } from '../../../shared/tenant.service';
import { tenant } from '../../../shared/tenant.model';
import { response } from 'express';

@Component({
  selector: 'app-tenant',
  templateUrl: './tenant.component.html',
  styleUrl: './tenant.component.css'
})
export class TenantComponent implements OnInit {



  constructor(public serviceM: MessageService, public ServiceS: SessionService, public serviceT:TenantService) { } 

  
  tenants : tenant[] = []

  ngOnInit() {
   // console.log(this.ServiceS.User)
    if (this.ServiceS.hasSession()){
    setTimeout(() => {
      this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Authentication Success' });
    }, 0); // Delay of 0 milliseconds
  }

   // console.log(this.ServiceS.User);




    this.serviceT.getTenants().subscribe({
      next:(response) => {
        this.tenants = response
       // console.log(this.tenants)

      }
    })


  }
  


  
}
