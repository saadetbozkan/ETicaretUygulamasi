import { Component, Inject, OnInit } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-complate-order-dialog',
  templateUrl: './complate-order-dialog.component.html',
  styleUrls: ['./complate-order-dialog.component.scss']
})
export class ComplateOrderDialogComponent extends BaseDialog<ComplateOrderDialogComponent> {

  constructor(dialogRef: MatDialogRef<ComplateOrderDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ComplateOrderState) {
    super(dialogRef);
  }
  
  complate(){

  }

}
 export enum ComplateOrderState{
  Yes,
  No
 }