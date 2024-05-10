import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { SessionService } from '../utils/SessionService'; // Adjust the path based on your project structure

@Injectable({
  providedIn: 'root',
})
export class AuthenticationGuard implements CanActivate {
  constructor(private sessionService: SessionService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const protectedRoutes: string[] = ['/Tenant', '/Overview', '/CreateTicket', '/TicketList', '/TicketDetails', '/Admin'];

    // Check if the user is authenticated or has the necessary credentials
    let isAuthenticated: boolean;
    try {
      isAuthenticated = this.sessionService.hasSession();
    } catch (error) {
      // Handle the error if sessionStorage is not available
      isAuthenticated = false;
      console.error('Error accessing sessionStorage:', error);
    }

    // Check if the route matches a protected route or a route with dynamic parameters
    if ((protectedRoutes.includes(state.url) || state.url.match(/\/[\w\S]+\/[\w\S]+$/)) && !isAuthenticated) {
      // Redirect to the login page if the user is not authenticated and trying to access a protected route
      this.router.navigate(['']); // Change '/Sign_up' to the actual login page route
      return false;
    }

    // Check if the route is an Admin route and the user has the Admin role
    if (state.url.startsWith('/Admin/') && !this.sessionService.hasAdminRole()) {
      // Redirect to a page indicating unauthorized access
      this.router.navigate(['/unauthorized']); // Change '/unauthorized' to the appropriate route
      return false;
    }

    return true;
  }
}
