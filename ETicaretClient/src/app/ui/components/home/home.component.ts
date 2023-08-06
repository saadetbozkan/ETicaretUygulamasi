import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AuthService } from 'src/app/services/common/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, public authService: AuthService) {
    super(spinner);
   }

  ngOnInit(): void {
    //this.showSpinner(SpinnerType.BallTriangle);
  }
}