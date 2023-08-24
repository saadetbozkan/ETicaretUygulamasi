import { Component, OnInit } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { UserService } from 'src/app/services/common/models/user.service';

interface OrderNode {
  name?: string;
  children?: OrderNode[];
  orderCode?: string
  description?: string;
  address?: string;
  basketItems?: any[];
  isComplated?: boolean;
  createdDate?: Date;
  totalPrice?: number;
}
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  constructor(private userService: UserService) { }

  displayedColumns: string[] = ['orderCode', 'description', 'address', 'createdDate', 'totalPrice', 'isComplated'];
  panelOpenState = false;

  async ngOnInit() {
    /*
    this.dataSource.data = [
      {
        name: "Orders",
        children: [
          {
            name: "orderCode1",
            children: [
              {
                orderCode: "orderCode1",
                description: "descrition1",
                address: "address1",
                basketItems: ["basketItem1", "basketItem2", "basketItem3"],
                isComplatedOrder: false
              }
            ]
          },
          {
            name: "orderCode2",
            children: [
              {
                orderCode: "orderCode2",
                description: "descrition2",
                address: "address2",
                basketItems: ["basketItem1", "basketItem2", "basketItem3"],
                isComplatedOrder: false
              }
            ]
          }, {
            name: "orderCode1",
            children: [
              {
                orderCode: "orderCode1",
                description: "descrition1",
                address: "address1",
                basketItems: ["basketItem1", "basketItem2", "basketItem3"],
                isComplatedOrder: true
              }
            ]
          }, {
            name: "orderCode1",
            children: [
              {
                orderCode: "orderCode1",
                description: "descrition1",
                address: "address1",
                basketItems: ["basketItem1", "basketItem2", "basketItem3"],
                isComplatedOrder: true
              }
            ]
          }, {
            name: "orderCode1",
            children: [
              {
                orderCode: "orderCode1",
                description: "descrition1",
                address: "address1",
                basketItems: ["basketItem1", "basketItem2", "basketItem3"],
                isComplatedOrder: true
              }
            ]
          }, {
            name: "orderCode1",
            children: [
              {
                orderCode: "orderCode1",
                description: "descrition1",
                address: "address1",
                basketItems: ["basketItem1", "basketItem2", "basketItem3"],
                isComplatedOrder: true
              }
            ]
          }, {
            name: "orderCode1",
            children: [
              {
                orderCode: "orderCode1",
                description: "descrition1",
                address: "address1",
                basketItems: ["basketItem1", "basketItem2", "basketItem3"],
                isComplatedOrder: true
              }
            ]
          },

        ]
      }
    ];
    */
  
    this.dataSource.data = [
      {
        name: "Orders",
        children:(await this.userService.getOrdersToCurrentUser())
          .map((item) => {
            let totalPrice = 0;
            const orderNode: OrderNode = {
                  name: item.orderCode,
                  children:[{
                    orderCode:item.orderCode,
                    description: item.description,
                    address: item.address,
                    basketItems: item.basketItems,
                    isComplated: item.isComplated,
                    createdDate: item.createdDate,
                    totalPrice: item.basketItems.map(_item=> {
                     return totalPrice += _item.price 
                    }).pop()
                  }]
                };
                
            return orderNode;
          })
      }
    ];
  }
  private _transformer = (node: OrderNode, level: number) => {
    return {
      expandable: !!node.children && node.children?.length > 0,
      name: node.name,
      level: level,
      orderCode: node.orderCode,
      description: node.description,
      address: node.address,
      basketItems: node.basketItems,
      isComplated: node.isComplated,
      createdDate:node.createdDate,
      totalPrice: node.totalPrice
    };
  };

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level,
    node => node.expandable,
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    node => node.level,
    node => node.expandable,
    node => node.children
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;
}
