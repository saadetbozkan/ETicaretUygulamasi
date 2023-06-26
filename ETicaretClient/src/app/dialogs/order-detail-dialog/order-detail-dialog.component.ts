import { Order_Detail } from 'src/app/contracts/order/order_detail';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseDialog } from './../base/base-dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/common/models/order.service';


@Component({
  selector: 'app-order-detail-dialog',
  templateUrl: './order-detail-dialog.component.html',
  styleUrls: ['./order-detail-dialog.component.scss']
})
export class OrderDetailDialogComponent extends BaseDialog<OrderDetailDialogComponent> implements OnInit {

  constructor(
    dialogRef: MatDialogRef<OrderDetailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OrderDetailDialogState | string,private orderService: OrderService) {
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

}
export enum OrderDetailDialogState{
  Close,
  OrderComplate
 }