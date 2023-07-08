import { FileUploadModule } from 'src/app/services/common/file-upload/file-upload.module';
import { MatButtonModule } from '@angular/material/button';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { SelectProductImageDialogComponent } from './select-product-image-dialog/select-product-image-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { SafePipeModule } from 'safe-pipe';
import { BasketItemRemoveDialogComponent } from './basket-item-remove-dialog/basket-item-remove-dialog.component';
import { ShoppingComplateDialogComponent } from './shopping-complate-dialog/shopping-complate-dialog.component';
import { OrderDetailDialogComponent } from './order-detail-dialog/order-detail-dialog.component';
import { MatTableModule } from '@angular/material/table';
import {MatToolbarModule} from '@angular/material/toolbar';
import { ComplateOrderDialogComponent } from './complate-order-dialog/complate-order-dialog.component';


@NgModule({
  declarations: [
    DeleteDialogComponent,
    SelectProductImageDialogComponent,
    BasketItemRemoveDialogComponent,
    ShoppingComplateDialogComponent,
    OrderDetailDialogComponent,
    ComplateOrderDialogComponent
      ],
  imports: [
    CommonModule,
    MatDialogModule, MatButtonModule, MatCardModule,
    FileUploadModule,
    SafePipeModule,
    MatTableModule,
    MatToolbarModule
  ]
})
export class DialogModule { }
