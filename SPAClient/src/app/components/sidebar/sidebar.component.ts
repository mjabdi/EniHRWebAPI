import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from '../../services/authentication.service';

declare const $: any;
export class RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}


@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];
  ROUTES : RouteInfo[];

  constructor(private authenticationService : AuthenticationService) { }

  ngOnInit() {

    if (this.authenticationService.isLogin())
    {
        if (this.authenticationService.getUsername() == 'admin')
        {
            this.ROUTES = [
                { path: '/users', title: 'Users',  icon: 'person', class: '' },
            ];
        }
        else
        {
            this.ROUTES = [
                { path: '/home', title: 'Dashboard',  icon: 'dashboard', class: '' },
                { path: '/employees', title: 'Employees',  icon: 'people', class: '' },
                { path: '/housing', title: 'Housing',  icon: 'account_balance', class: '' },
                { path: '/leave', title: 'Leave',  icon: 'transfer_within_a_station', class: '' },
                // { path: '/importdata', title: 'Import Data',  icon: 'archive', class: '' },
                { path: '/settings', title: 'Settings',  icon: 'build', class: '' }

            ];
        }
    }

    this.menuItems = this.ROUTES.filter(menuItem => menuItem);
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };
}
