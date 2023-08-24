import { BaseUrl } from 'src/app/contracts/base_url';
import { ProductService } from 'src/app/services/common/models/product.service';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AuthService } from 'src/app/services/common/auth.service';
import { List_Product } from 'src/app/contracts/list_product';
import { FileService } from 'src/app/services/common/models/file.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent implements OnInit {
  products: List_Product[];
  baseUrl: BaseUrl;
  currentCarousel: number = 1;

  constructor(spinner: NgxSpinnerService, public authService: AuthService, private productService: ProductService, private fileService:FileService) {
    super(spinner);
   }

  ngOnInit(): void {
    this.getProduct(this.currentCarousel);
  }

  async getProduct(currentCarousel: number){
    this.baseUrl = await this.fileService.getBaseStorageUrl();
    const data: {totalProductCount: number; products: List_Product[]} = await this.productService.read(currentCarousel, 3,
      () => {

      },
      errormessage => {

      });
      this.products = data.products;

      this.products = this.products.map<List_Product>(p => {
      const listProduct: List_Product = { 
      name: p.name,
      id: p.id,
      price: p.price,
      stock: p.stock,
      createDate: p.createDate,
      updateDate: p.updateDate,
      imagePath : p.productImageFiles.length ? p.productImageFiles.find(p => p.showcase = true).path : "",
      productImageFiles: p.productImageFiles
    };
    return listProduct;
   });
  }
  carouselControl(currentCarousel: number){
    this.currentCarousel = currentCarousel;
    this.getProduct(this.currentCarousel);
  }

}