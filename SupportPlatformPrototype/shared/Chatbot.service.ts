import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ChatbotService {
  private conversation: any[] = [];

  constructor() {}

  clearConversation() {
    this.conversation = [];
  }

  getConversation() {
    return this.conversation;
  }

  addMessage(message: any) {
    this.conversation.push(message);
  }
}
