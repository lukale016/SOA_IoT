import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

let port = 6002;  //change port based on microservice

@Injectable({
  providedIn: 'root'
})
export class WarningHubService {

  public connection ;
  public messages: Array<string> = [];
  constructor() {}

  beginConnection() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:${port}/hub/data`, {
        transport: signalR.HttpTransportType.ServerSentEvents,
      })
      .configureLogging(signalR.LogLevel.Debug)
      .build();

    const start = async () => {
      try {
        if (this.connection.state === signalR.HubConnectionState.Disconnected) {
          await this.connection.start().catch((err) => console.log(err));
          console.log('SignalR re-Connected for warning-hub.');
        }
        console.log('SignalR still in Connected state. for warning-hub');
      } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
      }
    };

    //this.connection.onclose(start);

    // Start the connection.
    start();

    this.connection.on(
      'receivedData',
      (type: string, message: string) => {
        console.log('warning hub came with params:', type, message);
        if (type === "card1" || type === "card2") {
          this.messages.push("Hand1andHand2 sensor: " + message);
        }
        else if (type === "card3"){
          this.messages.push("Hand3 sensor: " + message);
        }
      }
    );
  }
}
