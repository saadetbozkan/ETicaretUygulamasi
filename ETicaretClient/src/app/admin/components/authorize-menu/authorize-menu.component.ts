import { DialogService } from './../../../services/common/dialog.service';
import { ApplicationService } from './../../../services/common/models/application.service';
import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener, MatTreeNestedDataSource } from '@angular/material/tree';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent } from 'src/app/base/base.component';
import { Actions, Menu } from 'src/app/contracts/application-configuration/menu';
import { AuthorizeMenuDialogComponent } from 'src/app/dialogs/authorize-menu-dialog/authorize-menu-dialog.component';

interface ITreeMenu {
  name?: string;
  actions?: ITreeMenu[];
  code?: string;
  menuName?: string;
}

interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}
@Component({
  selector: 'app-authorize-menu',
  templateUrl: './authorize-menu.component.html',
  styleUrls: ['./authorize-menu.component.scss']
})
export class AuthorizeMenuComponent extends BaseComponent implements OnInit{
 
  constructor(spinner: NgxSpinnerService, private applicationService: ApplicationService, private dialogService: DialogService){
    super(spinner);
  }
   
   async ngOnInit() {
    this.dataSource.data = (await this.applicationService.getAuthorizeDefinitionEndPoints())
    .map(m =>{

      const treeMenu: ITreeMenu = {
        name: m.name,
        actions: m.actions.map(a=> {
          const _treeMenu: ITreeMenu={
            name: a.definition,
            code: a.code,
            menuName: m.name
          }
          return _treeMenu;
        })
      };
      return treeMenu;
    });  
  }


  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level,
    node => node.expandable,
  );

  
  treeFlattener = new MatTreeFlattener(
    (menu: ITreeMenu, level: number) =>{
      return {
        expandable: !!menu.actions && menu.actions?.length > 0,
        name: menu.name,
        level: level,
        code: menu.code,
        menuName: menu.menuName
      };
    },
    menu => menu.level,
    menu => menu.expandable,
    menu => menu.actions
  );
  
 dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  assignRole(code: string, name: string, menuName: string) {
    this.dialogService.openDialog({
      componentType: AuthorizeMenuDialogComponent,
      data: {code: code, name: name, menuName: menuName},
      options:{
        width :"750px"
      },
      afterClosed: () => {

      }
    })
  }
}