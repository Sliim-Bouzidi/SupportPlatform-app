import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Ticket } from '../../../shared/ticket.model';
import { TicketService } from '../../../shared/ticket.service';
import { response } from 'express';
import { Status } from '../../../shared/Status.model';
import { StatusService } from '../../../shared/Status.service';
import { Priority } from '../../../shared/Priority.model';
import { PriorityService } from '../../../shared/Priority.service';
import { ProcessFlowService } from '../../../shared/ProcessFlow.service';
import { ProcessFlow } from '../../../shared/ProcessFlow.model';
import { User } from '../../../shared/user.model';
import { UserService } from '../../../shared/user-service.service';
import { SeverityService } from '../../../shared/Severity.service';
import { Severity } from '../../../shared/Severity.model';
import { PrimeIcons } from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';
import { SessionService } from '../../../utils/SessionService';
import { MessageService } from 'primeng/api';
import { ReturnStatement } from '@angular/compiler';
import { tick } from '@angular/core/testing';
import { TagsService } from '../../../shared/Tags.service';
import { Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-CreateTicket',
  templateUrl: './CreateTicket.component.html',
  styleUrls: ['./CreateTicket.component.css']
})
export class CreateTicketComponent implements OnInit,AfterViewInit {
 
  htmlEditor: string = '';
  usePrimeNGQuill: boolean;


  constructor(
    public serviceT:TicketService,
    public serviceS:StatusService,
    public serviceP:PriorityService,
    public servicePF:ProcessFlowService,
    public serviceU:UserService,
    public serviceSeverity:SeverityService,
    public router: Router,
    private route: ActivatedRoute,
    public ServiceSession:SessionService,
    public serviceM: MessageService,
    public serviceTags: TagsService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    // Check if running on server to determine whether to use PrimeNG Quill
    this.usePrimeNGQuill = !isPlatformServer(this.platformId);
  }

  severity: Severity[] = [];
  severityOptions: {name: string} [] = [];

  users: User[] = [];
  userOptions: {name: string} [] = []
  AssignTo: any
  PriorityNames: any
  ProcessFlowNames:any
  Status: any
  Severity:any


  ticket : Ticket = new Ticket



  
  nodes = [

];


 tags: string[] = [];


  firstLevelProcessFlow: ProcessFlow[] = [];



  statuses: Status[] = []; // Array to hold the list of statuses
  StatusOptions: { name: string }[] = [];



  priority: Priority[] = []; // Array to hold the list of statuses
  PriorityOptions: { name: string }[] = [];

  ngAfterViewInit(): void {
  // @ts-ignore
    new FroalaEditor("#editor")
  }



  tenantname :string = ""

 // nodes: any[] = [];

  ngOnInit() {

    this.route.params.subscribe(params => {
      this.tenantname = params['tenantname']; // corrected
    });

    this.loadFirstLevelProcessFlow();
      
       
    this.serviceTags.getTagNames().subscribe({
      next: (response: string[]) => {
        this.tags = response;
        console.log(this.tags)
      }
    });





  
   



    
    this.serviceSeverity.getAllSeverityNames().subscribe({
      next:(respone:any) => {
        for (let i =0; i < respone.length; i++){
          this.severityOptions.push({ name: respone[i]});
        }
      },

      error: (error) => {
        console.error('Error fetching Severity:', error);
      }

    });





    this.serviceS.getAllStatusNames().subscribe(
      (response: any) => {
        // Loop over the response array and populate options array
        for (let i = 0; i < response.length; i++) {
          this.StatusOptions.push({ name: response[i] });
        }
      },
      (error) => {
        console.error('Error fetching statuses:', error);
      }
    );


    this.serviceP.getAllPrioritiesNames().subscribe(

      (response: any) => {
        // Loop over the response array and populate options array
        for (let i = 0; i < response.length; i++) {
          this.PriorityOptions.push({ name: response[i] });
        }
      },
      (error) => {
        console.error('Error fetching priorities:', error);
      }
    );



    

  this.serviceU.getAllUsers().subscribe({
    next:(response: any) => {
      for (let i = 0; i < response.length; i++) {
        this.userOptions.push({ name: response[i].email });
      }
    },
    error: (error) => {
      console.error('Error fetching users:', error);
    }
  });



  
  //console.log(this.firstLevelProcessFlow)



  }





  loadFirstLevelProcessFlow(): void {
    this.servicePF.getFirstLevelProcessFlow().subscribe({
      next: (response) => {
        if (response && response.length > 0) {
          response.forEach(processFlowName => {
            this.loadListOfChildrenRecursive(processFlowName, this.nodes);
          });
        } else {
          console.log("No first level process flows found.");
        }
      }
    });
  }
  
  loadListOfChildrenRecursive(parentProcessFlowName: string, treeData: any[]): void {
    this.servicePF.getListOfChildrenOfParent(parentProcessFlowName).subscribe({
      next: (children) => {
        if (children && children.length > 0) {
          const parentNode = {
            label: parentProcessFlowName,
            icon: PrimeIcons.FOLDER_OPEN, // PrimeNG icon for parent nodes
            children: []
          };
  
          children.forEach(childName => {
            this.loadListOfChildrenRecursive(childName, parentNode.children);
          });
  
          treeData.push(parentNode);
        } else {
          // If no more children found, push the parentProcessFlowName to the treeData
          treeData.push({
            label: parentProcessFlowName,
            icon: PrimeIcons.FILE, // PrimeNG icon for leaf nodes
            selectable: true
          });
        }
      }
    });
   // console.log(treeData)
  }

  
  




/*removeHtmlTags(input: string): string {
  // Create a temporary div element
  const tempDiv = document.createElement('div');
  
  // Set the innerHTML of the div to the input string
  tempDiv.innerHTML = input;
  
  // Return the text content of the div, which will strip out any HTML tags
  return tempDiv.textContent || tempDiv.innerText || '';
}*/



AddTicket() {
  // Check if any required field is empty
  if (
    !this.ticket.title ||
    !this.AssignTo ||
    !this.Status ||
    !this.Severity ||
    !this.PriorityNames ||
    !this.ticket.tags ||
    !this.ProcessFlowNames ||
    !this.tenantname
  ) {
    // If any required field is empty, display an error toast
    this.serviceM.add({ severity: 'error', summary: 'Error', detail: 'Please fill in all the fields' });
    return; // Stop execution
  }

  // Populate ticket properties
  console.log(this.AssignTo)
  this.ticket.assignTo = this.AssignTo.name;
  this.ticket.tags = this.ticket.tags;
  this.ticket.severityName = this.Severity.name;
  this.ticket.statusName = this.Status.name;
  this.ticket.processflowName = this.ProcessFlowNames.label;
  this.ticket.priorirtyName = this.PriorityNames.name;
  this.ticket.tenantname = this.tenantname;
  //this.ticket.description = this.removeHtmlTags(this.ticket.description)
  if (this.ServiceSession.User?.username) {
    this.ticket.username = this.ServiceSession.User?.username;
  }

  this.serviceT.createTicket(this.ticket).subscribe({
    next: (response) => { 
      console.log(response);
      
      if (response === 'ticket created succesfully') {
        // If server response indicates success, display a success toast
        this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket created successfully' }); 
      } else {
        // If server response indicates failure, display an error toast
        this.serviceM.add({ severity: 'error', summary: 'Error', detail: response });
      }
    },
    error: (error) => {
     
      this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket created successfully' }); 
    }
  });
 
}




Tags:string[] = [];
filteredSuggestions:  string[] = []

search(event: any) {
    this.serviceTags.getTagNames().subscribe({
      next: (response: string[]) => {
        this.Tags = response;
      }
    });

    let query = event.query; // Keep the query as it is without converting to lowercase

    // Filter the suggestions based on the query
    this.filteredSuggestions = this.Tags.filter(suggestion =>
      typeof suggestion === 'string' &&
      suggestion.toLowerCase().startsWith(query.toLowerCase())
    );

    // If the searched suggestion doesn't exist in filteredSuggestions and the array is not empty, add it to the beginning
    if (this.filteredSuggestions.length === 0 && !this.filteredSuggestions.includes(query)) {
      this.filteredSuggestions.unshift(query);
        // If the searched suggestion doesn't exist in filteredSuggestions and the array is not empty, add it to the beginning
    if (this.filteredSuggestions.length === 0 && !this.filteredSuggestions.includes(query)) {
      this.filteredSuggestions.unshift(query);
    }
    }
  }
 


}
