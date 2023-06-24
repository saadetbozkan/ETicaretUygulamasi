import { CustomToastrService, ToasterMessageType, ToasterPosition } from 'src/app/services/ui/custom-toastr.service';
import { OrderService } from './../../../services/common/models/order.service';
import { Update_Basket_Item } from './../../../contracts/basket/update_basket_item';
import { BasketService } from 'src/app/services/common/models/basket.service';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { List_Basket_Item } from 'src/app/contracts/basket/list_basket_item';
import { Create_Order } from 'src/app/contracts/order/create_order';
import { Router } from '@angular/router';

declare var $ : any;
@Component({
  selector: 'app-baskets',
  templateUrl: './baskets.component.html',
  styleUrls: ['./baskets.component.scss']
})
export class BasketsComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, private basketService: BasketService, private orderService: OrderService, private toastrService: CustomToastrService, private router: Router) {
    super(spinner);
   }
   basketItems: List_Basket_Item[];

  async ngOnInit(): Promise<void> {
    this.showSpinner(SpinnerType.BallTriangle);
    this.basketItems = await this.basketService.get();
    this.hideSpinner(SpinnerType.BallTriangle);
  }

  async changeQuantity(object: any)
  {
    this.showSpinner(SpinnerType.BallTriangle);
   
    const basketItemId: string = object.target.attributes["id"].value;
    const quantity: number = object.target.value; 

    const basketItem: Update_Basket_Item = new Update_Basket_Item;
    basketItem.basketItemId = basketItemId;
    basketItem.quantity = quantity;

    await this.basketService.updateQuantity(basketItem);
    this.hideSpinner(SpinnerType.BallTriangle);
  }

  async removeBasketItem(basketItemId: string)
  {
    this.showSpinner(SpinnerType.BallTriangle);
    
    await this.basketService.remove(basketItemId);
    $("." + basketItemId).fadeOut(500, () => this.hideSpinner(SpinnerType.BallTriangle));
  }

  async shoppingComplete(){
    this.showSpinner(SpinnerType.JellyBox);
    const order: Create_Order = new Create_Order();
    order.address = "Rize";
    order.description = "Birilierini şiparişi"; 
    await this.orderService.create(order);
    this.hideSpinner(SpinnerType.JellyBox);
    this.toastrService.message("siparişiniz alınmıştır.","Şipariş oluşturuldu.",{
      messageType: ToasterMessageType.Info,
      position: ToasterPosition.TopRight
    });
    this.router.navigate(["/"]);
  }
}