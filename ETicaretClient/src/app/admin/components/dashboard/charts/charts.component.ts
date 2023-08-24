import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';
import { UserService } from 'src/app/services/common/models/user.service';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.scss']
})
export class ChartsComponent implements OnInit {

  constructor(private userService: UserService) { }

  async ngOnInit() {
    
    const orders = await this.userService.getOrdersToCurrentUser();
    
    const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    let ordersCount = [0,0,0,0,0,0,0,0,0,0,0,0];
    orders.map(item=>{
      ordersCount[ parseInt(item.createdDate.slice(5,7))]++;
   
    });

    
    let productsName = [];
    let productsCount = [];

      orders.map(order=>{
     
      order.basketItems.map(product => {
       if(!productsName.includes(product.name)) {
        productsName.push(product.name);
        productsCount.push(1);
      }else{
        productsCount[productsName.indexOf(product.name)] ++
      }
       
      }
     )
    })

    new Chart("orderChart", {
      type: 'line',
      data: {
        labels: months,
        datasets: [
          {
            label: "Order",
            data: ordersCount,
            pointRadius: 4,
            borderColor: '#ffe5e5',
            backgroundColor: '#fe4042'
          }
        ]
      },
      options: {
        aspectRatio: 2,
      }
    });

    new Chart("productChart", {
      type: 'line', 
      data: {
        labels: productsName,
        datasets: [
          {
            label: "Product",
            data: productsCount,
            pointRadius: 4,
            borderColor: '#ffe5e5',
            backgroundColor: '#fe4042'
          }
        ]
      },
      options: {
        aspectRatio: 2,
      }
    });

  }
}
