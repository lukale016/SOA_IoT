import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { DataHubService } from '../cardhand-hub/data-hub.service';

@Injectable({
  providedIn: 'root'
})
export class ChartdataService {

  public chartData1 : Observable<Array<number>>; 
  public chartData2 : Array<number>; 
  public chartData3 : Observable<Array<number>>;
  constructor(
    private router: Router,
    private http: HttpClient,
    private cardDataHubService : DataHubService,
    
  ) { 
    // this.cardDataHubService.beginConnection();

    // this.chartData1 = of(this.cardDataHubService.cardHand1);
    // this.chartData1.subscribe((data)=>{
    //   console.log("chartdataservice chartdata1 received this",data);
    // });
    // this.chartData2 = this.cardDataHubService.cardHand2;
    // this.chartData3 = of(this.cardDataHubService.cardHand3);
  }

    loadData() {//: Observable<Array<number>> {
      try{
      // const data : Observable<Array<number>> = this.http.get<Array<number>>(`${environment}/getData`,{headers:{

      // }}).pipe(retry(1),catchError(this.handleError))

      // data.subscribe((numbers : number[]) =>{
      //   this.chartData1 = of(numbers);
      // })
    
      // return data;
      }
      catch(error)
      {
        console.log("loadData failed:",error);
      }
    }

    
  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` + `body was: ${error.error}`
      );
    }
    // Return an observable with a user-facing error message.
    return throwError('Something bad happened; please try again later.');
  }


}