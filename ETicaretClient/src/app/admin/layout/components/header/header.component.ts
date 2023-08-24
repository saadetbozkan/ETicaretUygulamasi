import { CurrentUserService } from './../../../../services/common/current-user.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private currentUserService: CurrentUserService) { }

  user: string;

  ngOnInit(): void {
    this.user = this.currentUserService.getUsername();
    }
}
