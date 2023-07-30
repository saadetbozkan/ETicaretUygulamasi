import { DialogService } from 'src/app/services/common/dialog.service';
import { Create_Product } from 'src/app/contracts/create_product';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { ListComponent } from './list/list.component';
import { QrcodeReadingDialogComponent } from 'src/app/dialogs/qrcode-reading-dialog/qrcode-reading-dialog.component';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService,
    private dialog:DialogService) {
    super(spinner);
   }

  ngOnInit(): void {
   // this.showSpinner(SpinnerType.ScaleMuliple);
  }

  @ViewChild(ListComponent) listComponents: ListComponent
  createdProduct( createdProduct: Create_Product){
    this.listComponents.getProducts();
  }

  showProductQRCodeReading(){
    this.dialog.openDialog({
      componentType: QrcodeReadingDialogComponent,
      data:null,
      options: {
        width: "800px"
      },
      afterClosed: () => {}
    });
  }
}