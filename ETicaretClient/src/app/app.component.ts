import { Router } from '@angular/router';
import { CustomToastrService, ToasterMessageType, ToasterPosition } from 'src/app/services/ui/custom-toastr.service';
import { AuthService } from './services/common/auth.service';
import { Component, ViewChild } from '@angular/core';
import { ComponentType, DynamicLoadComponentService } from './services/common/dynamic-load-component.service';
import { DynamicLoadComponentDirective } from './directives/common/dynamic-load-component.directive';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title(title: "ETicaretClient") {
    throw new Error('Method not implemented.');
  }

  @ViewChild(DynamicLoadComponentDirective,{static: true})
  dynamicLoadComponentDirective: DynamicLoadComponentDirective;

  constructor(public authService: AuthService, 
    private toastrService: CustomToastrService,
    private router: Router,
    private dynamicLoadComponentService: DynamicLoadComponentService)  {
    authService.identityCheck();
  }
  signOut(){
    localStorage.removeItem("accessToken");
    this.authService.identityCheck(); 
    
    this.router.navigate([""]);
    
    this.toastrService.message("Oturum kapatılmıştır", "Oturum Kapatıldı.",{
      messageType: ToasterMessageType.Warning,
      position: ToasterPosition.TopRight
    });   
  }
  loadComponent(){
    this.dynamicLoadComponentService.loadComponent(ComponentType.BasketsComponent, this.dynamicLoadComponentDirective.viewContainerRef)
  }
}
