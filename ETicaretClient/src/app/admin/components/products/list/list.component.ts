import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { List_Product } from 'src/app/contracts/list_product';
import { ProductService } from 'src/app/services/common/models/product.service';
import { MatPaginator } from '@angular/material/paginator';
import { DialogService } from 'src/app/services/common/dialog.service';
import { SelectProductImageDialogComponent } from 'src/app/dialogs/select-product-image-dialog/select-product-image-dialog.component';
import { QrcodeDialogComponent } from 'src/app/dialogs/qrcode-dialog/qrcode-dialog.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent extends BaseComponent implements OnInit {
  constructor(spinner: NgxSpinnerService,
    private productService: ProductService,
    private alertifyService: AlertifyService,
    private dialogService: DialogService) {
    super(spinner)
   }

  displayedColumns: string[] = ['name', 'stock', 'price', 'createDate','updateDate', 'photos', 'qrCode', 'edit', 'delete',];
  dataSource: MatTableDataSource<List_Product> = null;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  async getProducts(){
    this.showSpinner(SpinnerType.JellyBox);
    const allProdutcs: {totalProductCount: number; products: List_Product[]} = await this.productService.read(this.paginator ? this.paginator.pageIndex : 0,this.paginator ? this.paginator.pageSize : 5, () =>this.hideSpinner(SpinnerType.JellyBox), errorMessage => this.alertifyService.message(errorMessage,{
      dismissOthers: true,
      messageType: MessageType.Error,
      position: Position.BottomLeft
    }));
    this.dataSource = new MatTableDataSource<List_Product>(allProdutcs.products);
    this.paginator.length = allProdutcs.totalProductCount;
  }

  addProductImage(id: string){
    this.dialogService.openDialog({
      componentType: SelectProductImageDialogComponent,
      data : id,
      options:{
        width: "800px"
      }
    })
  }
  
  async pageChanged(){
    await this.getProducts()
  }

  async ngOnInit() {
    await this.getProducts();
  }

 showQRCode(productId: string){
    this.dialogService.openDialog({
      componentType: QrcodeDialogComponent,
      data : productId
    })

  }
}