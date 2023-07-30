import { Update_Product } from 'src/app/contracts/update_product';
import { ProductService } from 'src/app/services/common/models/product.service';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { NgxScannerQrcodeComponent } from 'ngx-scanner-qrcode';
import { Component, ElementRef, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-qrcode-reading-dialog',
  templateUrl: './qrcode-reading-dialog.component.html',
  styleUrls: ['./qrcode-reading-dialog.component.scss']
})
export class QrcodeReadingDialogComponent extends BaseDialog<QrcodeReadingDialogComponent> implements OnInit, OnDestroy {

  constructor(dialogRef: MatDialogRef<QrcodeReadingDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string,
    private spinner: NgxSpinnerService,
    private alertify: AlertifyService,
    private productService: ProductService) {
    super(dialogRef);
  }
  @ViewChild("scanner", { static: true }) scanner: NgxScannerQrcodeComponent;
  @ViewChild("txtStock", { static: true }) txtStock: ElementRef;

  ngOnInit(): void {
    this.scanner.start();
  }

  ngOnDestroy(): void {
    this.scanner.stop();
  }

  updateProduct: Update_Product;

  async onEvent(e) {
    this.spinner.show(SpinnerType.JellyBox);
    const data: any = (e as { data: string }).data;

    if (data != null && data != "") {
      this.dialogRef.close();
      const jsonData = JSON.parse(data);
      const stockValue = (this.txtStock.nativeElement as HTMLInputElement).value;
      this.updateProduct =
      {
        id: jsonData.Id,
        name: jsonData.Name,
        stock: parseInt(stockValue),
        price: jsonData.Price
      };

       await this.productService.update(this.updateProduct, () => {
        
        this.alertify.message(`${jsonData.Name} ürününün stock bilgisi güncellenmişir.`,
          {
            messageType: MessageType.Success,
            position: Position.TopRight
          });
      });
      this.spinner.hide(SpinnerType.JellyBox);
    }
   
  }
}