import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './signup/signup.component';
import { TenantComponent } from './tenant/tenant.component';
import { AuthenticationGuard } from '../../guards/authentication.guard';
import { OverviewComponent } from './Overview/Overview.component';
import { CreateTicketComponent } from './CreateTicket/CreateTicket.component';
import { TicketListComponent } from './ticket-list/ticket-list.component';
import { TicketDetailsComponent } from './TicketDetails/TicketDetails.component';




const routes: Routes = [
  // Other routes if any

  { path: '', component: SignupComponent },
  { path: 'Tenant', component: TenantComponent, canActivate: [AuthenticationGuard] },
  { path: 'Overview/:tenantname', component: OverviewComponent, canActivate: [AuthenticationGuard] },
  { path: 'CreateTicket/:tenantname', component: CreateTicketComponent, canActivate: [AuthenticationGuard] },
  { path: 'TicketList/:tenantname', component: TicketListComponent, canActivate: [AuthenticationGuard] },
  { path: 'TicketDetails/:tenantname/:ticketID', component: TicketDetailsComponent, canActivate: [AuthenticationGuard] }



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
