import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';

@Component({
  selector: 'app-TicketDetails',
  templateUrl: './TicketDetails.component.html',
  styleUrls: ['./TicketDetails.component.css']
})
export class TicketDetailsComponent implements OnInit {
  tenantname: string = '';
  ticketID: string = '';
  visible : boolean = false
  closable : boolean = false
  htmlEditor: string = '';
  usePrimeNGQuill: boolean;
  currentContentIndex: number = 0;
  maxIndex : number = 2


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    // Check if running on server to determine whether to use PrimeNG Quill
    this.usePrimeNGQuill = !isPlatformServer(this.platformId);
  }

  ngOnInit() {
  console.log(this.tenantname)
    this.route.params.subscribe(params => {
      this.ticketID = params['ticketID']; // corrected
      this.tenantname = params['tenantname'];
    });




  }

  navigateToTicketList(tenantname: string) {
    console.log(tenantname)
    this.router.navigate(['/TicketList', tenantname]);
  }

  /*copyTicketID() {
    const ticketIDElement = document.createElement('textarea');
    ticketIDElement.value = this.ticketID;
    document.body.appendChild(ticketIDElement);
    ticketIDElement.select();
    document.execCommand('copy');
    document.body.removeChild(ticketIDElement);
  }*/

  showTicketInfo(){
    this.visible = !this.visible
  }


  // Method to show the previous content
  showPreviousContent() {
    if (this.currentContentIndex > 0) {
        this.currentContentIndex--;
    }
}

// Method to show the next content
showNextContent() {
  
    // Add conditions if you have multiple contents
    if (this.currentContentIndex < this.maxIndex) {
        this.currentContentIndex++;
    }
}
}
