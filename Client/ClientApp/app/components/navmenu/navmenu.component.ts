import { Component } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    public LoggedUser: string;

    onLogin(event) {
        this.LoggedUser = event;
        console.log(event);
    }
}
