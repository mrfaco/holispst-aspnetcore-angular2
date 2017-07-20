import { Component, Inject, EventEmitter, Output } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from "rxjs/Rx";
import 'rxjs/Rx';

@Component({
    selector: 'creator',
    templateUrl: './creator.component.html'
})

export class CreatorComponent {

    private http: Http;
    public added = false;
    @Output() onUpdated = new EventEmitter<boolean>();

    constructor(http: Http) {
        this.http = http;
    }

    add(formValues) {
        let mat =
            {
                'Id': 0,
                'Name': formValues.name,
                'Price': formValues.price,
                'Stock': formValues.stock,
                'DateModified': Date.now()
            };
        this.postToApi(<Materia>mat)
            .subscribe(
            data => {
                console.log(data);
                this.added = true;
                this.onUpdated.emit(true);
                return true
            },
            error => {
                console.error("error");
                return Observable.throw(error);
            }
            );
    }

    postToApi(mat: Materia) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify([mat]);
        console.log(body);
        return this.http.post('http://holispst.azurewebsites.net/api/materias', body, options)
            .map((res: Response) => res.text());
    }
}

interface Materia {
    Id: number,
    Name: string,
    Price: number,
    Stock: number,
    DateModified: number
}