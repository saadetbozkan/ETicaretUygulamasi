import { ComplateOrderDialogComponent, ComplateOrderState } from './../complate-order-dialog/complate-order-dialog.component';
import { DialogService } from 'src/app/services/common/dialog.service';
import { Order_Detail } from 'src/app/contracts/order/order_detail';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseDialog } from './../base/base-dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/common/models/order.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';


@Component({
  selector: 'app-order-detail-dialog',
  templateUrl: './order-detail-dialog.component.html',
  styleUrls: ['./order-detail-dialog.component.scss']
})
export class OrderDetailDialogComponent extends BaseDialog<OrderDetailDialogComponent> implements OnInit {

  constructor(
    dialogRef: MatDialogRef<OrderDetailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OrderDetailDialogState | string, 
    private orderService: OrderService,
    private dialogService: DialogService,
    private spinner: NgxSpinnerService,
    private alertify: AlertifyService) {
    super(dialogRef);
  }
  orderDetail: Order_Detail;
  
  displayedColumns: string[] = ['name', 'price', 'quantity', 'totalPrice'];
  dataSource = [];
  clickedRows = new Set<any>();
  totalPrice: number;

  async ngOnInit(): Promise<void> {
    this.orderDetail = await this.orderService.getById(this.data as string);
    this.dataSource = this.orderDetail.basketItem;
    this.totalPrice = this.orderDetail.basketItem
    .map((basketItem, index) => basketItem.price * basketItem.quantity)
    .reduce((price,current)=> price + current);
  }

  complateOrder(){
    this.dialogService.openDialog({
      componentType: ComplateOrderDialogComponent,
      data: ComplateOrderState.Yes,
      afterClosed: async () =>{
         this.spinner.show(SpinnerType.JellyBox);
        await this.orderService.complateOrder(this.data as string, () =>{
          this.alertify.message("Sipariş tamamlandı.",{
            position: Position.TopRight,
            messageType: MessageType.Success
          });
        });
        this.spinner.hide(SpinnerType.JellyBox);
      }
    });
  }
}

export enum OrderDetailDialogState{
  Close,
  OrderComplate
 }