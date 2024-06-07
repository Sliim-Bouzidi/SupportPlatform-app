import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './signup/signup.component';
import { TenantComponent } from './tenant/tenant.component';
import { AuthenticationGuard } from '../../guards/authentication.guard';
import { OverviewComponent } from './Overview/Overview.component';
import { CreateTicketComponent } from './CreateTicket/CreateTicket.component';
import { TicketListComponent } from './ticket-list/ticket-list.component';
import { TicketDetailsComponent } from './TicketDetails/TicketDetails.component';
import { AdminInterfaceComponent } from './AdminInterface/AdminInterface.component';
import { UserProfileComponent } from './UserProfile/UserProfile.component';
import { ChatbotComponent } from './Chatbot/Chatbot.component';
import { Chatbot2Component } from './chatbot2/chatbot2.component';




const routes: Routes = [
  // Other routes if any

  { path: '', component: SignupComponent },
  { path: 'Tenant', component: TenantComponent, canActivate: [AuthenticationGuard] },
  { path: 'Overview/:tenantname', component: OverviewComponent, canActivate: [AuthenticationGuard] },
  { path: 'CreateTicket/:tenantname', component: CreateTicketComponent, canActivate: [AuthenticationGuard] },
  { path: 'TicketList/:tenantname', component: TicketListComponent, canActivate: [AuthenticationGuard] },
  { path: 'TicketDetails/:tenantname/:ticketID', component: TicketDetailsComponent, canActivate: [AuthenticationGuard] },
  { path: 'Admin/:tenantname', component: AdminInterfaceComponent, canActivate: [AuthenticationGuard] },
  { path: 'UserProfile/:tenantname/:userID', component: UserProfileComponent, canActivate: [AuthenticationGuard] },
  { path: 'Chatbot/:tenantname', component: ChatbotComponent, canActivate: [AuthenticationGuard] },
  { path: 'Chatbot2/:tenantname', component: Chatbot2Component, canActivate: [AuthenticationGuard] }
 



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
