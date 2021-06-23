import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { WarningHubService } from 'src/app/services/warning-hub/warning-hub.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public showDivNotifications : boolean = false;
  private $messages : Observable<Array<string>> ;
  public warnings : Array<string> = ["no", "current", "warnings", "to", "display"];
  constructor(private warningHubService : WarningHubService) {
    this.warningHubService.beginConnection();
    this.$messages = of(this.warningHubService.messages);
    this.$messages.subscribe((data :Array<string>) =>{
       this.warnings = data;
       console.log("warnings subbed to:",data,this.warnings); 
    })
   }

  ngOnInit(): void {
  }

  showNotifications(){
    this.showDivNotifications = ! this.showDivNotifications;
  }
}
