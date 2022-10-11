import { MessageType, Position } from 'src/app/services/admin/alertify.service';
import { AlertifyService } from 'src/app/services/admin/alertify.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(private alertify: AlertifyService) { }

  ngOnInit(): void {
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
