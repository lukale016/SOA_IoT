import { Component, Input, OnInit, Output } from '@angular/core';
import { Chart, LinearScale, } from 'chart.js';

@Component({
  selector: 'app-sensor',
  templateUrl: './sensor.component.html',
  styleUrls: ['./sensor.component.css'],
})
export class SensorComponent implements OnInit {
  private _started: boolean = false;
  @Input()
  public title :string = "title of sensor"
  @Input() @Output()
  public numValues :number;
  public get started(): boolean {
    return this._started;
  }
  public set started(value: boolean) {
    this._started = value;
  }
  constructor() {}

  ngOnInit(): void {}

  ngAfterViewChecked(){}
}