import { Component, Inject, Input, Output, EventEmitter, } from '@angular/core';
import { Http } from '@angular/http';


@Component({
    selector: 'account',
    templateUrl: './account.component.html',
})


export class AccountComponent {

    public UserName: string;
    public Logged = false;
    @Output() Login = new EventEmitter<string>();

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        let user: any;
        http.get(originUrl+"/Account/User").subscribe(result => {
            user = result.json() as string;
            console.log(user);
            if (user != null) {
                this.UserName = user.userName;
            }
            if (this.UserName != null) {
                this.Logged = true;
            }
            else {
                this.Logged = false;
            }
            console.log(this.Logged)
            this.Login.emit(this.UserName);

        });

    }


}