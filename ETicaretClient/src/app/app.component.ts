import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CustomToastrService, ToasterMessageType, ToasterOptions, ToasterPosition } from './services/ui/custom-toastr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ETicaretClient';
  constructor(private toastrService: CustomToastrService){
    toastrService.message("Merhaba", "Title", {
      messageType: ToasterMessageType.Info,
      position: ToasterPosition.BottomFullWidth
    })

  }
}
