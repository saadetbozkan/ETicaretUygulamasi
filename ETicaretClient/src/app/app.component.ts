import { ActivatedRoute, Router } from '@angular/router';
import { CustomToastrService, ToasterMessageType, ToasterPosition } from 'src/app/services/ui/custom-toastr.service';
import { AuthService } from './services/common/auth.service';
import { Component } from '@angular/core';
import { Position } from './services/admin/alertify.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public authService: AuthService, 
    private toastrService: CustomToastrService,
    private router: Router) {
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
}
