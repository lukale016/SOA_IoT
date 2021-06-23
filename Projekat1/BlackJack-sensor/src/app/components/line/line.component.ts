import { Component, Input, SimpleChange, OnInit } from '@angular/core';
// For MDB Angular Free
import { ChartsModule, WavesModule } from 'angular-bootstrap-md';
import { Observable, of } from 'rxjs';
import { DataHubService } from 'src/app/services/cardhand-hub/data-hub.service';
import { ChartdataService } from 'src/app/services/chartdata/chartdata.service';

@Component({
  selector: 'app-line',
  templateUrl: './line.component.html',
  styleUrls: ['./line.component.css']
})
export class LineComponent implements OnInit {
  public chartType: string = 'line';
  private $arrayValue3: Observable<Array<number>>;
  public arrayValues3: Array<number> = [];
  @Input()
  public title: string = 'title of sensor';

  constructor(
    private chartDataService: ChartdataService,
    private dataHub: DataHubService
  ) {
    this.dataHub.beginConnection();
    this.$arrayValue3 = of(this.dataHub.cardHand3);
    //console.log("LINE COMPONENTTTTT");
    setTimeout(() => {
      
      this.$arrayValue3.subscribe((numbers) => {
        this.arrayValues3 = numbers;
        console.log('$3', this.arrayValues3, numbers);
          this.chartDatasets[0].data = this.arrayValues3;
      });
    }, 10000);
  }

  public chartDatasets: Array<any> = [
    { data: this.arrayValues3, label: 'My Third hand' },
  ];


  ngOnInit(): void {
  }

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
