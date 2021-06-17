import { Component, Input } from '@angular/core';
// For MDB Angular Free
import { ChartsModule, WavesModule } from 'angular-bootstrap-md';
import { of } from 'rxjs';
import { ChartdataService } from 'src/app/services/chartdata/chartdata.service';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss'],
})
export class ChartComponent {
  public chartType: string = 'line';
  public arrayValues1: Array<number> = [];
  public arrayValues2: Array<number> = [];
  public arrayValues3: Array<number> = [];
  @Input()
  public numValues: number ;

  constructor(private chartDataService: ChartdataService) {
    // this.chartDataService.loadData().pipe(() => {
    //   chartDataService.chartData.subscribe((data: Array<number>) => {
    //     this.arrayValues = data;
    //   });
    //   return of(this.arrayValues);
    // });
    chartDataService.chartData1.subscribe((data: Array<number>) => {
      this.arrayValues1 = data;
    });
    chartDataService.chartData2.subscribe((data: Array<number>) => {
      this.arrayValues2 = data;
    });
    

    //if (this.numValues === 2)
      this.chartDatasets = [
        { data: this.arrayValues1, label: 'My First hand' }, // data: this.arrayValues;
        { data: this.arrayValues2, label: 'My Second hand' },
      ];
    //else
      // this.chartDatasets = [
      //   { data: this.arrayValues3, label: 'My Third hand' }, // data: this.arrayValues;
      // ];
    console.log(this.numValues);
    setTimeout(() => {
      console.log("done",this.numValues);
    }, 2000);
  }

  public chartDatasets: Array<any> = [
    { data: this.arrayValues1, label: 'My First hand' }, // data: this.arrayValues;
    { data: this.arrayValues2, label: 'My Second hand' },
  ];

  public chartLabels: Array<any> = [
    'timeline',
    '',
    '',
    '',
    '',
    '',
    '',
  ];

  public chartColors: Array<any> = [
    {
      backgroundColor: 'rgba(105, 0, 132, .2)',
      borderColor: 'rgba(200, 99, 132, .7)',
      borderWidth: 2,
    },
    {
      backgroundColor: 'rgba(0, 137, 132, .2)',
      borderColor: 'rgba(0, 10, 130, .7)',
      borderWidth: 2,
    },
  ];

  public chartOptions: any = {
    responsive: true,
  };
  public chartClicked(e: any): void {}
  public chartHovered(e: any): void {}
}
