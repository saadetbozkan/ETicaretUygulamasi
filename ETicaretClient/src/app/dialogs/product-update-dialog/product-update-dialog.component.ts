import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseDialog } from '../base/base-dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { ProductService } from 'src/app/services/common/models/product.service';
import { SpinnerType } from 'src/app/base/base.component';
import { Product_Detail } from 'src/app/contracts/product_detail';

@Component({
  selector: 'app-product-update-dialog',
  templateUrl: './product-update-dialog.component.html',
  styleUrls: ['./product-update-dialog.component.scss']
})
export class ProductUpdateDialogComponent extends BaseDialog<ProductUpdateDialogComponent> implements OnInit {

  product: Product_Detail;
  constructor(dialogRef: MatDialogRef<ProductUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string,
    private spinner: NgxSpinnerService,
    private alertify: AlertifyService,
    private productService: ProductService) {
    super(dialogRef);

  }
  async ngOnInit() {
    this.product = await this.productService.getById(this.data)
  }

  async update(txtprice: HTMLInputElement, txtStock:HTMLInputElement) {
    this.spinner.show(SpinnerType.JellyBox);
    const updateProduct =
    {
      id: this.data,
      name: this.product.name,
      stock: parseInt(txtStock.value)!=0 ? parseInt(txtStock.value) : this.product.stock ,
      price: parseInt(txtprice.value)!=0 ? parseInt(txtprice.value) : this.product.price ,
    };

    await this.productService.update(updateProduct, () => {

      this.alertify.message(`${updateProduct.name} ürününün stock bilgisi güncellenmişir.`,
        {
          messageType: MessageType.Success,
          position: Position.TopRight
        });
    });
    this.spinner.hide(SpinnerType.JellyBox);
    this.dialogRef.close();
  }
}