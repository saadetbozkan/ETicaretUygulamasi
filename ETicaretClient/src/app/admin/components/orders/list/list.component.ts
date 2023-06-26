import { OrderDetailDialogState } from './../../../../dialogs/order-detail-dialog/order-detail-dialog.component';
import { DialogService } from 'src/app/services/common/dialog.service';
import { OrderService } from './../../../../services/common/models/order.service';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { List_Order } from 'src/app/contracts/order/list_order';

import { OrderDetailDialogComponent } from 'src/app/dialogs/order-detail-dialog/order-detail-dialog.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})

export class ListComponent extends BaseComponent implements OnInit {
  constructor(spinner: NgxSpinnerService,
    private orderService: OrderService,
    private alertifyService: AlertifyService,
    private dialogService: DialogService) {
    super(spinner)
   }

  displayedColumns: string[] = ['orderCode', 'userName', 'totalPrice', 'createdDate', 'viewDetail' ,'delete'];
  dataSource: MatTableDataSource<List_Order> = null;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  async getOrders(){
    this.showSpinner(SpinnerType.JellyBox);
    const allOrders: {totalOrderCount: number; orders: List_Order[]} = await this.orderService.getAll(
      this.paginator ? this.paginator.pageIndex : 0,this.paginator ? this.paginator.pageSize : 5, 
      () => this.hideSpinner(SpinnerType.JellyBox),
      errorMessage => this.alertifyService.message(errorMessage,{
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.BottomLeft
    }));

    this.dataSource = new MatTableDataSource<List_Order>(allOrders.orders);
    this.paginator.length = allOrders.totalOrderCount;
  }

  async pageChanged(){
    await this.getOrders()
  }

  async ngOnInit() {
    await this.getOrders();
  }

  showDetail(id:string){
    this.dialogService.openDialog({
      componentType: OrderDetailDialogComponent,
      data: id,
      options:{
        width :"750px"
      }
    });
  }
}