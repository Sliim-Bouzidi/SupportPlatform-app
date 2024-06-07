import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chatbot2',
  templateUrl: './chatbot2.component.html',
  styleUrls: ['./chatbot2.component.css']
})
export class Chatbot2Component implements OnInit {

  constructor() { }

  ngOnInit() {
    const styleOptions: any = {
      bubbleBackground: 'rgba(173, 216, 230, 1)', // Light blue color
      bubbleFromUserBackground: 'rgba(0, 87, 249)',
      bubbleFromUserBorderRadius: 25,
      rootHeight: '100%',
      rootWidth: '100%',
      backgroundColor: 'rgb(230, 236, 243)',
      bubbleBorderRadius: 12,
      bubbleFromUserTextColor: 'white',
      sendBoxHeight: 60, // Adjusting the height of the send box
      sendBoxBackground: 'rgba(255, 255, 255, 0.9)',
      sendBoxTextWrap: true,
      sendBoxButtonTextColor: 'white',
      sendBoxPlaceholderColor: 'rgba(0, 0, 0, 0.5)',
      sendBoxBorderTop: 'solid 1px rgba(0, 0, 0, 0.1)',
      sendBoxMaxHeight: 100, // Adjusting the maximum height of the send box
      paddingRegular: 8, // Default padding used in most visual components
      paddingWide: 16, // Padding used for suggestedAction buttons
    };

    const avatarOptions = {
      botAvatarImage: '../../assets/chatboticon.png',
      botAvatarInitials: 'BF',
      userAvatarImage: 'https://cdn-icons-png.flaticon.com/512/9131/9131529.png',
      userAvatarInitials: 'WC',
      botAvatarBackgroundColor: 'rgb(1, 133, 255)'
    };

    (window as any).WebChat.renderWebChat({
      directLine: (window as any).WebChat.createDirectLine({
        token: 'noK6z3wdLMo.D0VD1pqpW8yY3ymCK40PJWWwNGYTax44WH9f83sz31k'
      }),
      styleOptions: { ...styleOptions, ...avatarOptions }
    }, document.getElementById('webchat'));
  }

}
