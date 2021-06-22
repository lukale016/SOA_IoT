import { Injectable } from '@angular/core';
import { merge, Observable, of } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import * as internal from 'stream';
import { MapOperator } from 'rxjs/internal/operators/map';
import { mergeAll, tap } from 'rxjs/operators';

let port = 6002;


@Injectable({
  providedIn: 'root',
})
export class DataHubService {
  public connection ;
  public cardHand1: Array<number> = [];
  public cardHand2: Array<number> = [];
  public cardHand3: Array<number> = [];
  constructor() {}

  beginConnection() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`/hub/data`, {
        transport: signalR.HttpTransportType.ServerSentEvents,
      })
      .configureLogging(signalR.LogLevel.Debug)
      .build();

    const start = async () => {
      try {
        if (this.connection.state === signalR.HubConnectionState.Disconnected) {
          await this.connection.start().catch((err) => console.log(err));
          console.log('SignalR re-Connected for data-hub.');
        }
        console.log('SignalR still in Connected state. for data-hub');
      } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
      }
    };

    //this.connection.onclose(start);

    // Start the connection.
    start();

    this.connection.on(
      "sendData",
      (type: string, value: number, timestamp: string) => {
        console.log('data came with params:', type, value, timestamp);
        if (type === "card1") {
          this.cardHand1.push(value);
          console.log('this.cardhand1:', this.cardHand1);
        } else if (type === "card2") {
          this.cardHand2.push(value);
          console.log('this.cardhand2:', this.cardHand2);
        }
        else if (type === "card3"){
          this.cardHand3.push(value);
          console.log('this.cardhand3:', this.cardHand3);
        }
      }
    );
  }
}
