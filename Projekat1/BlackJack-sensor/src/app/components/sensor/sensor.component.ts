import { Component, Input, OnInit, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Chart, LinearScale, } from 'chart.js';

@Component({
  selector: 'app-sensor',
  templateUrl: './sensor.component.html',
  styleUrls: ['./sensor.component.css'],
})
export class SensorComponent implements OnInit {
  private _started: boolean = false;
  @Input()
  public title :string = "title of sensor";
  @Input()
  public isChart :boolean = false;
  public get started(): boolean {
    return this._started;
  }
  public set started(value: boolean) {
    this._started = value;
  }
  constructor(private http : HttpClient ) {}

  ngOnInit(): void {
    // console.log("SENSOR COMOPENNT isChart",this.isChart)
    // setTimeout(() => {
    //   console.log("SENSOR COMPNENT TIMEOUT ischart",this.isChart)
    // }, 2000);
  }

  ngAfterViewChecked(){}

  btnClicked(){
    this.started = !this.started
    let response;
    if(this.title ==="HandOneAndTwo sensor"){
     console.log("6000 calling");
      this.http.post<any>('http://localhost:6000/api/sensor/turnonoffsensor',response);
    }
    if(this.title ==="HandThree sensor"){
      console.log("6001 calling");
      this.http.post<any>('http://localhost:6001/api/sensor/turnonoffsensor',response);
    }
  }
}