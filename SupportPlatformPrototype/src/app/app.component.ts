import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SupportPlatformPrototype';
  isChatbotVisible = false;

  constructor(private router: Router) {}

  toggleChatbot() {
    this.isChatbotVisible = !this.isChatbotVisible;
  }

  shouldShowChatbot(): boolean {
    const currentPage = this.router.url;
    return (
      currentPage.includes('/Overview/Alpha') ||
      currentPage.includes('/CreateTicket') ||
      currentPage.includes('/TicketList') ||
      currentPage.includes('/TicketDetails') ||
      currentPage.includes('/Admin')
    );
  }
}
