// bot-icon.service.ts
import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class BotIconService {
  private allowedRoutes = ['Overview', 'CreateTicket', 'TicketList', 'TicketDetails', 'Admin'];
  showBotIcon = false;

  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateBotIconVisibility();
      }
    });
    // Initial check for visibility
    this.updateBotIconVisibility();
  }

  private updateBotIconVisibility(): void {
    const currentRoute = this.getCurrentRoute();
    this.showBotIcon = this.allowedRoutes.includes(currentRoute);
  }

  private getCurrentRoute(): string {
    const url = this.router.url;
    const routeSegments = url.split('/');
    return routeSegments[1];
  }
}
