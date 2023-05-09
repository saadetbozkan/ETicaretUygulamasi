import { MessageType, Position } from 'src/app/services/admin/alertify.service';
import { AlertifyService } from 'src/app/services/admin/alertify.service';
import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit {

  constructor(private alertify: AlertifyService, spinner: NgxSpinnerService) {
    super(spinner);
   }

  ngOnInit(): void {
    //this.showSpinner(SpinnerType.ScaleMuliple);
  }

  m(){
    this.alertify.message("Merhaba", {
      messageType: MessageType.Error,
      position: Position.BottomRight, 
      delay: 5,
      dismissOthers: false,
    });
  }

  d(){
    this.alertify.dismissAll();
  }

}
