export class Ticket {
  ticketID!: string
  title: string = "";
  description: string = "";
  assignTo: string = "";
  statusName: string = "";
  severityName: string = "";
  priorirtyName: string = "";
  tags: string = "";
  processflowName:string = "";
  username: string = "";
  tenantname: string = "";
 // categories: string="";
 // tickettype: string ="";
  dossierNumber: string = "";
  salesOrderNumber: string = "";
  workingOrderNumber: string = "";
  assistancePlanNumber : string = "";
  createdDate!: Date;
}
