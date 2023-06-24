import { SignalRService } from './../../../services/common/signalr.service';
import { MessageType, Position } from 'src/app/services/admin/alertify.service';
import { AlertifyService } from 'src/app/services/admin/alertify.service';
import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ReceiveFunctions } from 'src/app/constants/receive-functions';
import { HubUrls } from 'src/app/constants/hub-urls';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit {

  constructor(private alertify: AlertifyService, spinner: NgxSpinnerService, private signalRService: SignalRService) {
    super(spinner);
    signalRService.start(HubUrls.OrderHub);
    signalRService.start(HubUrls.ProductHub);
    
   }

  ngOnInit(): void {
    //this.showSpinner(SpinnerType.ScaleMuliple);
    this.signalRService.on(ReceiveFunctions.ProductAddedMessageReceiveFunction, message =>{
      this.alertify.message(message,{
        messageType: MessageType.Notify,
        position: Position.TopRight
      })
    });

    this.signalRService.on(ReceiveFunctions.OrderAddedMessageReceiveFunction, message =>{
      this.alertify.message(message,{
        messageType: MessageType.Notify,
        position: Position.TopCenter
      })
    });
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
