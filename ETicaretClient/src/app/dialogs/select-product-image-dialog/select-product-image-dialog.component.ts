import { DeleteDialogComponent, DeleteState } from 'src/app/dialogs/delete-dialog/delete-dialog.component';
import { DialogService } from 'src/app/services/common/dialog.service';
import { SpinnerType } from 'src/app/base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductService } from 'src/app/services/common/models/product.service';
import { List_Product_Image } from './../../contracts/list_product_image';
import { FileUploadOptions } from 'src/app/services/common/file-upload/file-upload.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseDialog } from './../base/base-dialog';
import { Component, Inject, Output, OnInit } from '@angular/core';

declare var $: any;
@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrls: ['./select-product-image-dialog.component.scss']
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> implements OnInit{

  constructor(dialogRef: MatDialogRef<SelectProductImageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState | string,
    private productService: ProductService,
    private spinner: NgxSpinnerService,
    private dialogService: DialogService) {
    super(dialogRef);
  }

  @Output() options: Partial<FileUploadOptions> = {
    accept: ".png, .jpg, .jpeg, .gif",
    action: "upload",
    controller: "products",
    explanation: "Ürün resmini seçin veya buraya sürükleyin.",
    isAdminPage: true,
    queryString: `id=${this.data}`
  }

  images: List_Product_Image[];
  
  async ngOnInit(){
    this.spinner.show(SpinnerType.BallTriangle)
    this.images = await this.productService.readImages(this.data as string, () => this.spinner.hide(SpinnerType.BallTriangle));  
  }
  
  async deleteImage(imageId: string, event: any) {
    this.dialogService.openDialog({
      componentType: DeleteDialogComponent,
      data: DeleteState.Yes,
      afterClosed: async () => {
        this.spinner.show(SpinnerType.BallTriangle)
        await this.productService.deleteImage(this.data as string, imageId, () => {
          this.spinner.hide(SpinnerType.BallTriangle);
          var card = $(event.srcElement).parent().parent();
          card.fadeOut(500);
        });
      }
    })
  }
}
export enum SelectProductImageState{
  Close
 }
