import { NgxSpinnerService } from 'ngx-spinner';
import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/common/models/product.service';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { Create_Product } from 'src/app/contracts/create_product';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, private productService: ProductService, private alertify: AlertifyService) {
    super(spinner)
   }

  ngOnInit(): void {
  }

  create(name: HTMLInputElement, stock:HTMLInputElement, price:HTMLInputElement){
    this.showSpinner(SpinnerType.JellyBox)
    const create_product: Create_Product = new Create_Product();
    create_product.name = name.value;
    create_product.price = parseFloat(price.value);
    create_product.stock = parseInt(stock.value);


    this.productService.create(create_product, () => {
      this.hideSpinner(SpinnerType.JellyBox);
      this.alertify.message("Ürün başarıyla eklenmiştir.",{
        messageType : MessageType.Success,
        position:Position.BottomLeft,
        dismissOthers: true
      });
    }, errorMessage => {
      this.alertify.message(errorMessage,
        {
        messageType : MessageType.Error,
        position:Position.BottomLeft,
        dismissOthers: true
      });
    });
  }
}
