import { FileService } from './../../../../services/common/models/file.service';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/services/common/models/product.service';
import { Component, OnInit } from '@angular/core';
import { List_Product } from 'src/app/contracts/list_product';
import { BaseUrl } from 'src/app/contracts/base_url';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  constructor(private productService: ProductService, private activatedRoute: ActivatedRoute, private fileService: FileService) { }

  products: List_Product[];
  currentPageNo : number;
  totalProductCount: number;
  totalPageCount: number;
  pageSize: number = 10;
  pageList: number[] = [];
  baseUrl:BaseUrl;

  async ngOnInit() {
    this.baseUrl = await this.fileService.getBaseStorageUrl();

    this.activatedRoute.params.subscribe(async params =>{
      this.currentPageNo = parseInt(params["pageNo"] ?? 1);
      const data: {totalProductCount: number; products: List_Product[]} = await this.productService.read(this.currentPageNo - 1, this.pageSize,
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
  
    this.totalProductCount = data.totalProductCount;
    this.totalPageCount = Math.ceil(this.totalProductCount / this.pageSize) ;

    this.pageList = [];

    if (this.currentPageNo -3 <= 0)
      for(let i = 1; i <= 7; i++)
        this.pageList.push(i);
    else if (this.currentPageNo + 3 >= this.totalPageCount)
      for(let i = this.totalPageCount - 6; i <= this.totalPageCount; i++)
        this.pageList.push(i);
    else
      for(let i = this.currentPageNo - 3; i <= this.currentPageNo + 3; i++)
        this.pageList.push(i);
    });   
    
  }
}
