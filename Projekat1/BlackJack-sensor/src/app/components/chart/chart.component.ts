import { Component, Input, SimpleChange } from '@angular/core';
// For MDB Angular Free
import { ChartsModule, WavesModule } from 'angular-bootstrap-md';
import { Observable, of } from 'rxjs';
import { DataHubService } from 'src/app/services/cardhand-hub/data-hub.service';
import { ChartdataService } from 'src/app/services/chartdata/chartdata.service';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss'],
})
export class ChartComponent {
  public chartType: string = 'line';
  private $arrayValue1: Observable<Array<number>>;
  private $arrayValue2: Observable<Array<number>>;
  private $arrayValue3: Observable<Array<number>>;
  public arrayValues1: Array<number> = [];
  public arrayValues2: Array<number> = [];
  public arrayValues3: Array<number> = [];
  @Input()
  public numValues: number;
  @Input()
  public title: string = 'title of sensor';

  constructor(
    private chartDataService: ChartdataService,
    private dataHub: DataHubService
  ) {
    this.dataHub.beginConnection();
    this.$arrayValue1 = of(this.dataHub.cardHand1);
    this.$arrayValue2 = of(this.dataHub.cardHand2);
    this.$arrayValue3 = of(this.dataHub.cardHand3);

    setTimeout(() => {
      
    
    this.$arrayValue1.subscribe((numbers) => {
      this.arrayValues1 = numbers;
      console.log(
        '$1',
        this.arrayValues1,
        numbers,
        'this.chartDatasets[0].data',
        this.chartDatasets[0].data
      );
      if(this.title==="HandOneAndTwo sensor")
        this.chartDatasets[0].data = this.arrayValues1;
    });
    this.$arrayValue2.subscribe((numbers) => {
      this.arrayValues2 = numbers;
      console.log('$2', this.arrayValues2, numbers);
      if(this.chartDatasets[1])
        this.chartDatasets[1].data = this.arrayValues2;
    });
    this.$arrayValue3.subscribe((numbers) => {
      this.arrayValues3 = numbers;
      console.log('$3', this.arrayValues2, numbers);
      if(this.title==="HandThree sensor")
        this.chartDatasets[0].data = this.arrayValues3;
    });
  }, 10000);
    this.function();

  }

  public chartDatasets: Array<any> = [
    { data: this.arrayValues1, label: 'My First hand' },
  ];

  public function = () => {

      if (this.title === "HandThree sensor") {
        this.chartDatasets = [
          { data: this.arrayValues3, label: 'My Third hand' },
        ];
        console.log(
          'FUNCTION handThree WORKING ITS MAGIC',
          this.chartDatasets,
          this.chartDatasets[0].data);
      } else if(this.title === "HandOneAndTwo sensor"){
        if(this.chartDatasets.length<2)
          this.chartDatasets.push({data:this.arrayValues2, label:"My Second Hand"});
        this.chartDatasets = [
          { data: this.arrayValues1, label: 'My First hand' }, // data: this.arrayValues;
          { data: this.arrayValues2, label: 'My Second hand' },
        ];
        console.log(
          'FUNCTION hand1and2 WORKING ITS MAGIC',
          this.chartDatasets,
          this.chartDatasets[0].data,
          this.chartDatasets[1].data
        );
      }
    setTimeout(() => {
      this.function();
    }, 5000);
  };
  public chartLabels: Array<any> = ['timeline', '1', '3', '5', '8', '11', '14'];

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

// setTimeout(() => {
// this.dataHub.cardHand1.subscribe(data => {
//   this.arrayValues1 = data;
//   this.chartDatasets[0].data = this.arrayValues1;

//   const func = () => {
//     setTimeout(() => {
//       console.log("chartdata1 subscribtion",this.arrayValues1,data)
//       console.log("radi subscribtion funcija",this.arrayValues1,data)
//       func();
//     }, 3000);
//   } ;

//   func();
// });
// this.arrayValues2 = this.dataHub.cardHand2;
// console.log("chartdata2",this.arrayValues2);

// }, 30000);

// // chartDataService.chartData2.subscribe((data: Array<number>) => {
// //   this.arrayValues2 = data;
// //   console.log("chartdata2",data);
// //   this.chartDatasets[1].data = this.arrayValues2;
// // });

// //if (this.numValues === 2)
//   // this.chartDatasets = [
//   //   { data: this.arrayValues1, label: 'My First hand' }, // data: this.arrayValues;
//   //   { data: this.arrayValues2, label: 'My Second hand' },
//   // ];
// //else
//   // this.chartDatasets = [
//   //   { data: this.arrayValues3, label: 'My Third hand' }, // data: this.arrayValues;
//   // ];
// console.log(this.numValues);
// setTimeout(() => {
//   console.log("done",this.numValues);
// }, 2000);

//this.chartDataService.loadData();
// this.chartDataService.loadData().pipe(() => {
//   chartDataService.chartData.subscribe((data: Array<number>) => {
//     this.arrayValues = data;
//   });
//   return of(this.arrayValues);
// });
