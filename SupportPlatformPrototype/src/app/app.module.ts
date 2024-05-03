import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { PasswordModule } from 'primeng/password';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignupComponent } from './signup/signup.component';
import { ToastModule } from 'primeng/toast';
import { TenantComponent } from './tenant/tenant.component';
import { OverviewComponent } from './Overview/Overview.component';
import { NavbarComponent } from './Navbar/Navbar.component';
import { SidebarComponent } from './Sidebar/Sidebar.component';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { BadgeModule } from 'primeng/badge';
import { MenuModule } from 'primeng/menu';
import { CreateTicketComponent } from './CreateTicket/CreateTicket.component';
import { DropdownModule } from 'primeng/dropdown';
import { ChipsModule } from 'primeng/chips';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { EditorModule } from 'primeng/editor';
import { TreeSelectModule } from 'primeng/treeselect';
import {TreeModule} from 'primeng/tree';
import { TableModule } from 'primeng/table';
import { tokenInterceptor } from '../../shared/token.interceptor';
import { TicketListComponent } from './ticket-list/ticket-list.component';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TicketDetailsComponent } from './TicketDetails/TicketDetails.component';
import { SplitterModule } from 'primeng/splitter';
import { PanelModule } from 'primeng/panel';
import { InplaceModule } from 'primeng/inplace';
import { ChipModule } from 'primeng/chip';
import { EditorModule as TinyMCEEditorModule } from '@tinymce/tinymce-angular'; // Alias EditorModule from tinymce-angular
import { TagInputModule } from 'ngx-chips';
import { MultiSelectModule } from 'primeng/multiselect';


@NgModule({
  declarations: [								
    AppComponent,
    SignupComponent,
    TenantComponent,
    OverviewComponent,
    NavbarComponent,
    SidebarComponent,
    CreateTicketComponent,
    TicketListComponent,
      TicketDetailsComponent
   ],
  imports: [
    
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    InputTextModule,
    IconFieldModule,
    InputIconModule,
    InputGroupModule,
    InputGroupAddonModule,
    PasswordModule,
    FloatLabelModule,
    ButtonModule,
    ToastModule,
    HttpClientModule,
    AvatarModule,
    AvatarGroupModule,
    BadgeModule,
    MenuModule,
    DropdownModule,
    ChipsModule,
    TableModule,
    EditorModule,
    TreeSelectModule,
    TreeModule,
    TableModule,
    EditorModule,
    TagModule,
    DialogModule,
    SplitterModule,
    PanelModule,
    InplaceModule,
    ChipModule,
    TinyMCEEditorModule,
    TagInputModule,
    MultiSelectModule
 

  ],
  providers: [
   // provideClientHydration(),
    MessageService,
    provideHttpClient(withFetch()),
    { provide: HTTP_INTERCEPTORS, useClass: tokenInterceptor, multi: true }
 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
