import { Component, Renderer2, ElementRef, HostListener } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SupportPlatformPrototype';
  isChatbotVisible = false;
  position = { top: 0, left: 0, fixed: true }; // Initial position is fixed

  private dragging = false;
  private lastX = 0;
  private lastY = 0;

  constructor(private router: Router, private renderer: Renderer2, private el: ElementRef) {}

  toggleChatbot() {
    this.isChatbotVisible = !this.isChatbotVisible;
    if (this.isChatbotVisible) {
      this.setInitialPosition();
    }
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

  setInitialPosition() {
    this.position.top = window.innerHeight - 625; // 550px height + 75px bottom offset
    this.position.left = window.innerWidth - 400; // 380px width + 20px right offset
    this.position.fixed = true; // Start with fixed positioning
  }

  onDragStart(event: MouseEvent) {
    this.dragging = true;
    this.lastX = event.clientX;
    this.lastY = event.clientY;
    this.position.fixed = false; // Switch to absolute positioning

    // Listen for mousemove and mouseup events on the document
    this.renderer.listen('document', 'mousemove', this.onDragMove.bind(this));
    this.renderer.listen('document', 'mouseup', this.onDragEnd.bind(this));
  }

  onDragMove(event: MouseEvent) {
    if (!this.dragging) return;

    const deltaX = event.clientX - this.lastX;
    const deltaY = event.clientY - this.lastY;

    this.position.left += deltaX;
    this.position.top += deltaY;

    this.lastX = event.clientX;
    this.lastY = event.clientY;
  }

  onDragEnd() {
    this.dragging = false;
  }
}
